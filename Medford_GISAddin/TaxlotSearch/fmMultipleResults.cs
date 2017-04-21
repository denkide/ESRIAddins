using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Framework;
using MedfordToolsUtility;
using MedfordToolsDAL;
using System.Data;

namespace Medford_GISAddin.TaxlotSearch
{
    public partial class fmMultipleResults : Form
    {
        private IFeatureCursor m_pFeatCurs;
        private IApplication m_pApp;
        private string m_sWhere;

        public IFeatureCursor SearchResults
        {
            get { return m_pFeatCurs; }
            set { m_pFeatCurs = value; }
        }

        public ESRI.ArcGIS.Framework.IApplication App
        {
            get { return this.m_pApp; }
            set { this.m_pApp = value; }
        }

        public fmMultipleResults()
        {
            InitializeComponent();
            m_sWhere = "";
            this.App = (IApplication)ArcMap.ThisApplication;
        }

        private void fmMultipleResults_Load(object sender, EventArgs e)
        {
            
        }

        private void dgvResults_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                string sWhere = "OBJECTID = " + this.dgvResults.Rows[e.RowIndex].Cells[0].Value.ToString();
                zoomFromDataGrid(sWhere);
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                s += ex.InnerException.Message;

                s += "that was it";
            }

        }

        private void zoomFromDataGrid(string sWhere)
        {
            //Set the query filter. use the arguements passed into the function
            IQueryFilter pQueryFilter = new QueryFilterClass();
            pQueryFilter.WhereClause = sWhere;

            ESRI.ArcGIS.ArcMapUI.IMxDocument pMxDoc = (ESRI.ArcGIS.ArcMapUI.IMxDocument)this.App.Document;
            IMap pMap = (IMap)pMxDoc.FocusMap;

            IFeatureLayer pTaxlotLayer;
            using (CSpatialSubs oSpatialSubs = new CSpatialSubs())
            {
                pTaxlotLayer = (IFeatureLayer)oSpatialSubs.returnFeatureLayer(pMap, SConst.Taxlots);
                oSpatialSubs.selectFeatures(pQueryFilter, this.App, pTaxlotLayer, true);
            }
        }

        private string getAllObjects()
        {
            string sRetVal = "";
            for (int i = 0; i < dgvResults.Rows.Count; i++)
            {
                if (i == 0)
                    sRetVal += this.dgvResults.Rows[i].Cells[0].Value.ToString();
                else
                    sRetVal += "," + this.dgvResults.Rows[i].Cells[0].Value.ToString();
            }
            return sRetVal;
        }

        private void btnZoom_Click(object sender, EventArgs e)
        {
            string sWhere = "OBJECTID IN (" + getAllObjects() + ")";
            //dgvResults.SelectAll();
            dgvResults.ClearSelection();
            zoomFromDataGrid(sWhere);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvResults.SelectedRows.Count == 1)
                {
                    this.Hide();

                    string sAddNum = dgvResults.SelectedRows[0].Cells["ADDRESSNUM"].Value.ToString();
                    if (sAddNum.IndexOf(" ") > 0)
                        sAddNum = sAddNum.Substring(0, sAddNum.IndexOf(" "));
                    string sStreet = dgvResults.SelectedRows[0].Cells["STREETNAME"].Value.ToString();
                    object oLocResults = new object();

                    //if (CMedToolsSubs.canConnect(CConst.LXConnString))
                    if (SConst.GotDBConn)
                    {
                        using (CData oData = new CData(SConst.LXConnString.ToString()))
                        {
                            oLocResults = oData.returnLocID(sAddNum, CMedToolsSubs.returnStreetDirectionFromTaxlotsAddress(sStreet), CMedToolsSubs.returnStreetNameFromTaxlotsAddress(sStreet, SConst.CountyStreets), CMedToolsSubs.returnStreetTypeFromTaxlotsAddress(sStreet, SConst.CountyStreets), dgvResults.SelectedRows[0].Cells["MAPLOT"].Value.ToString(), dgvResults.SelectedRows[0].Cells["ACCOUNT"].Value.ToString());
                        }
                    }
                    else
                        oLocResults = null;

                    if (oLocResults is Hashtable)
                    {
                        fmSubAccount oSubAccount = new fmSubAccount();
                        oSubAccount.App = this.App;
                        oSubAccount.ObjectID = dgvResults.SelectedRows[0].Cells["OBJECTID"].Value.ToString();
                        oSubAccount.SubAccounts = oLocResults as Hashtable;
                        oSubAccount.Show(new WindowWrapper((System.IntPtr)m_pApp.hWnd));
                    }
                    else
                    {
                        fmSearchResults oSearchResults = new fmSearchResults();
                        oSearchResults.ObjectID = dgvResults.SelectedRows[0].Cells["OBJECTID"].Value.ToString();
                        oSearchResults.App = this.App;
                        if (oLocResults != null)
                            oSearchResults.LocationID = (int)oLocResults;
                        else
                            oSearchResults.LocationID = 0;
                        oSearchResults.Show(new WindowWrapper((System.IntPtr)m_pApp.hWnd));
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please select one row in the grid.", "Taxlot Search: Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errors occurred: \r\n\r\n" + ex.Message);
                this.Dispose();
            }
        }

        //private void dgvResults_SelectionChanged(object sender, EventArgs e)
        //{
        //    if (dgvResults.SelectedRows.Count == dgvResults.Rows.Count)
        //        m_sWhere = "OBJECTID IN (" + getAllObjects()+ ")";

        //    zoomFromDataGrid(m_sWhere);
        //}


        private void dgvResults_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    //Set the query filter. use the arguements passed into the function
            //    IQueryFilter pQueryFilter = new QueryFilterClass();
            //    pQueryFilter.WhereClause = "OBJECTID = " + this.dgvResults.Rows[e.RowIndex].Cells[Int32.Parse(dgvResults.Rows[e.RowIndex].Cells["OBJECTID"].Value.ToString())].Value.ToString();

            //    ESRI.ArcGIS.ArcMapUI.IMxDocument pMxDoc = (ESRI.ArcGIS.ArcMapUI.IMxDocument)this.App.Document;
            //    IMap pMap = (IMap)pMxDoc.FocusMap;

            //    IFeatureLayer pTaxlotLayer = (IFeatureLayer)CMedToolsSubs.returnFeatureLayer(pMap, CConst.Taxlots);
            //    CMedToolsSubs.selectFeatures(pQueryFilter, this.App, pTaxlotLayer, false);
            //}
            //catch (Exception ex)
            //{
            //    string s = ex.Message;
            //    s += ex.InnerException.Message;

            //    s += "that was it";
            //}
        }

        private void fmMultipleResults_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                IFeature pFeat = this.SearchResults.NextFeature();

                int iRowCount = 0;
                int iOID = this.SearchResults.Fields.FindField("OBJECTID");
                int iFeeOwner = this.SearchResults.Fields.FindField("FEEOWNER");
                int iSiteAdd = this.SearchResults.Fields.FindField("SITEADD");
                int iTaxCode = this.SearchResults.Fields.FindField("TAXCODE");
                int iMaplot = this.SearchResults.Fields.FindField("MAPLOT");
                int iAcct = this.SearchResults.Fields.FindField("ACCOUNT");
                int iCity = this.SearchResults.Fields.FindField("CITY");
                int iAddressNum = this.SearchResults.Fields.FindField("ADDRESSNUM");
                int iStreet = this.SearchResults.Fields.FindField("STREETNAME");

                while (pFeat != null)
                {
                    string[] row = { 
                        pFeat.get_Value(iOID).ToString(),
                        pFeat.get_Value(iFeeOwner).ToString(),
                        pFeat.get_Value(iSiteAdd).ToString(),
                        "---", ///CMedToolsSubs.returnCityFromTaxCode(CMedToolsSubs.returnTaxCodeFormatted(pFeat.get_Value(iTaxCode).ToString()), CMedToolsSubs.getTaxCodes()),
                        pFeat.get_Value(iMaplot).ToString(),
                        pFeat.get_Value(iAcct).ToString(),
                        pFeat.get_Value(iCity).ToString(),
                        pFeat.get_Value(iAddressNum).ToString(),
                        pFeat.get_Value(iStreet).ToString()
                    };
                    dgvResults.Rows.Add(row);   //.Columns["FID"] = pFeat.Fields.FindField("FID").ToString();

                    pFeat = this.SearchResults.NextFeature();
                    iRowCount += 1;
                }

                //dgvResults.ClearSelection();
                //dgvResults.SelectAll();
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                s += "\r\n\r\n" + ex.InnerException.Message;
                s += "\r\n\r\nThat was it";
            }
        }

        private void dgvResults_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {

        }
    }
}
