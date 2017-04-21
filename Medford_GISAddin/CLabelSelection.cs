using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Desktop.AddIns;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Editor;
using System.Data.SqlClient;
using System.Data;
using MedfordToolsUtility;


namespace Medford_GISAddin
{
    public class CLabelSelection : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        fmRelatedPartyGrid results;

        public CLabelSelection()
        {
        }

        protected override void OnClick()
        {
            if (results != null)
            {
                try
                {
                    results.Close();
                    results = null;
                }
                catch
                {
                    // just move on.
                    int i = 1;
                }
            }

            using (CSpatialSubs oSpatialSubs = new CSpatialSubs())
            {
                // check if taxlots is in the data frame
                //if (!oSpatialSubs.doesMapLayerExist("MEDSDE.DBO.TAXLOTS", ArcMap.Application))

                if (!oSpatialSubs.doesMapLayerExist("MEDSDE.DBO.TAXLOTS", ArcMap.Application))
                {
                    System.Windows.Forms.MessageBox.Show("Taxlots layer does not exist." + Environment.NewLine + Environment.NewLine + "Please ensure that you have taxlots loaded into your project and " + Environment.NewLine + "that have your target taxlots selected.", "Related Party Query: Oops!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    return;
                }

                IFeatureLayer pFLayer = (IFeatureLayer)oSpatialSubs.returnFeatureLayer(ArcMap.Document.FocusMap, "Tax Lots");
                IFeatureSelection pFSel = pFLayer as IFeatureSelection;
                ISelectionSet2 pSelSet = pFSel.SelectionSet as ISelectionSet2;

                if (pSelSet.Count < 1)
                {
                    System.Windows.Forms.MessageBox.Show("There were no selected features found." + Environment.NewLine + Environment.NewLine + "Please select taxlots before using this tool.", "Related Party Query: Oops!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    return;
                }

                ICursor pCursor;
                pSelSet.Search(null, false, out pCursor);

                doQuery(returnSearchCriteria(pCursor as IFeatureCursor));

            }
        }

        private void doQuery(string sSearchCriteria)
        {
            if (sSearchCriteria.Length < 1)
            {
                System.Windows.Forms.MessageBox.Show("Search criteria is blank.", "Related Party Query: Oops!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                return;
            }

            SqlConnection oConn = new SqlConnection(@"Server=MEDSDE-1\MEDGISPROD;Database=MEDWARE;User ID=webUser;Password=tabl3t!;");
            SqlDataAdapter daData = new SqlDataAdapter();
            DataSet dsDataSet = new DataSet();

            oConn.Open();
            SqlCommand cmdAddr = new SqlCommand();

            cmdAddr.Connection = oConn;
            cmdAddr.CommandType = CommandType.Text;
            cmdAddr.CommandText = "SELECT * FROM vw_AddressWithRelatedParty where Maplot IN (" + sSearchCriteria + ")";

            daData.SelectCommand = cmdAddr;
            daData.Fill(dsDataSet);

            showResults(dsDataSet.Tables[0]);
        }

        private void showResults(DataTable dt)
        {
            if (dt.Rows.Count < 1)
            {
                System.Windows.Forms.MessageBox.Show("No records were returned from the search.", "Related Party Query: Oops!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                return;
            }

            results = new fmRelatedPartyGrid(dt);
            results.Show();
        }
        private string returnSearchCriteria(IFeatureCursor pFeatCursor)
        {
            IFeature pFeat = pFeatCursor.NextFeature();

            string sQueryConstraints = "";
            string sTaxlot = "";
            while (pFeat != null)
            {
                sTaxlot = pFeat.get_Value(2).ToString();

                switch (sTaxlot.Length)
                {
                    case 1:
                        sTaxlot = "0000" + sTaxlot;
                        break;
                    case 2:
                        sTaxlot = "000" + sTaxlot;
                        break;
                    case 3:
                        sTaxlot = "00" + sTaxlot;
                        break;
                    case 4:
                        sTaxlot = "0" + sTaxlot;
                        break;
                    default: break;
                }

                if (sQueryConstraints.Length > 1)
                    sQueryConstraints += ",";

                sQueryConstraints += "'" + pFeat.get_Value(1).ToString() + sTaxlot + "'";


                pFeat = pFeatCursor.NextFeature();
            }

            return sQueryConstraints;
        }


        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
