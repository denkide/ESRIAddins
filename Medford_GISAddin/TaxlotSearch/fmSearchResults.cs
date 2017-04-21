using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using MedfordToolsUtility;
using MedfordToolsDAL;
using System.Data;
namespace Medford_GISAddin.TaxlotSearch
{
    public partial class fmSearchResults : Form
    {
        private string m_sObjectID;
        private IApplication m_pApp;
        private int m_iLocID;
        private bool m_bShowHTE;

        public string ObjectID
        {
            get { return m_sObjectID; }
            set { m_sObjectID = value; }
        }

        public IApplication App
        {
            get { return m_pApp; }
            set { m_pApp = value; }
        }

        public int LocationID
        {
            get { return m_iLocID; }
            set { m_iLocID = value; }
        }

        public fmSearchResults()
        {
            InitializeComponent();
            this.App = (IApplication)ArcMap.ThisApplication;
        }

        private void fmSearchResults_Load(object sender, EventArgs e)
        {
            m_bShowHTE = false;
            this.setHTEVisibility();

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void getLXInfo(string sAccount, string sMapNum, string sTaxlot)
        {
            int iLocID = 0;
            using (CData oData = new CData(SConst.LXConnString.ToString()))
            {
                iLocID = oData.returnLocID(sAccount, sMapNum, sTaxlot);
            }
            lblLocID.Text = iLocID.ToString();
        }

        private void loadBP()
        {
            this.Cursor = Cursors.WaitCursor;

            tvwBP.Nodes.Clear();
            string commandString = "SELECT ATT_Yr, ATT_Key from LXMaster.dbo.vw_PermitsAndLicense Where Appl_ID = 'BP' AND ATT_Loc_ID = " + this.LocationID + " ORDER BY ATT_yr,ATT_Key ";

            DataSet ds;
            using (CData oData = new CData(SConst.LXConnString.ToString()))
            {
                ds = oData.returnHTEData(commandString);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        TreeNode tn = new TreeNode(dr["ATT_yr"].ToString() + "-" + dr["ATT_Key"].ToString());
                        tvwBP.Nodes.Add(tn);
                        char[] delimiter = { '|' };

                        string[] aDetails = oData.getBPDetails(dr["ATT_yr"].ToString(), dr["ATT_Key"].ToString()).Split(delimiter);

                        for (int i = 0; i < aDetails.Length; i++)
                        {
                            tn.Nodes.Add(aDetails[i].ToString());
                        }
                    }
                }
                else
                {
                    tvwBP.Nodes.Add("There are no Building Permit items");
                }
            }

            this.Cursor = Cursors.Default;
        }

        private void loadCE()
        {
            this.Cursor = Cursors.WaitCursor;

            tvwCE.Nodes.Clear();

            string commandString = "SELECT ATT_Yr, ATT_Key from LXMaster.dbo.vw_PermitsAndLicense Where Appl_ID = 'CE' AND ATT_Loc_ID = " + this.LocationID + " ORDER BY ATT_yr,ATT_Key ";
            DataSet ds;
            using (CData oData = new CData(SConst.LXConnString.ToString()))
            {
                ds = oData.returnHTEData(commandString);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        TreeNode tn = new TreeNode(dr["ATT_yr"].ToString() + "-" + dr["ATT_Key"].ToString());
                        tvwCE.Nodes.Add(tn);
                        char[] delimiter = { '|' };

                        string[] aDetails = oData.getCEDetails(dr["ATT_yr"].ToString(), dr["ATT_Key"].ToString()).Split(delimiter);

                        for (int i = 0; i < aDetails.Length; i++)
                        {
                            tn.Nodes.Add(aDetails[i].ToString());
                        }
                    }
                }
                else
                {
                    tvwCE.Nodes.Add("There are no Code Enforcement items");
                }
            }
            this.Cursor = Cursors.Default;
        }

        private void loadOL()
        {
            this.Cursor = Cursors.WaitCursor;

            tvwOL.Nodes.Clear();

            string commandString = "SELECT ATT_Yr, ATT_Key from LXMaster.dbo.vw_PermitsAndLicense Where Appl_ID = 'OL' AND ATT_Loc_ID = " + this.LocationID + " ORDER BY ATT_yr,ATT_Key ";
            DataSet ds;

            using (CData oData = new CData(SConst.LXConnString.ToString()))
            {
                ds = oData.returnHTEData(commandString);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        TreeNode tn = new TreeNode(dr["ATT_yr"].ToString() + "-" + dr["ATT_Key"].ToString());
                        tvwOL.Nodes.Add(tn);
                        char[] delimiter = { '|' };

                        DataSet ds0 = oData.getOLDetails(this.LocationID, Int32.Parse(dr["ATT_Key"].ToString()));
                        if (ds0.Tables.Count > 0)
                        {
                            foreach (DataRow dr0 in ds0.Tables[0].Rows)
                            {
                                //tn.Nodes.Add("License Year: 200" + dr0["OLYEAR"].ToString());

                                TreeNode tn0 = new TreeNode("License Year: 200" + dr0["OLYEAR"].ToString());
                                tn.Nodes.Add(tn0);

                                tn0.Nodes.Add("Business Name: " + dr0["OLBSNM"].ToString());
                                tn0.Nodes.Add("Business Addr: " + dr0["OLMAD1"].ToString().Trim() + " " + dr0["OLMAD2"].ToString().Trim() + " " + dr0["OLMAD3"].ToString().Trim() + " " + dr0["OLMZIP"].ToString().Trim());
                                tn0.Nodes.Add("Date Opened: " + oData.returnHTEDate(dr0["OLBDAT"].ToString()));
                                tn0.Nodes.Add("Owner Type: " + dr0["OLTOWN"].ToString());
                                tn0.Nodes.Add("License Number: " + dr0["OLLIC"].ToString());
                                tn0.Nodes.Add("Classification Code: " + dr0["OLCLAS"].ToString());
                                tn0.Nodes.Add("Application Date: " + oData.returnHTEDate(dr0["OLAPDT"].ToString()));
                                tn0.Nodes.Add("Issue Date: " + oData.returnHTEDate(dr0["OLISDT"].ToString()));
                                tn0.Nodes.Add("License Status: " + dr0["OLLSTA"].ToString());
                            }
                        }
                    }
                }
                else
                {
                    tvwOL.Nodes.Add("There are no Occupation License items");
                }
            }
            this.Cursor = Cursors.Default;
        }

        private void loadPZ()
        {
            this.Cursor = Cursors.WaitCursor;

            tvwPZ.Nodes.Clear();

            string commandString = "SELECT ATT_Yr, ATT_Key from LXMaster.dbo.vw_PermitsAndLicense Where Appl_ID = 'PZ' AND ATT_Loc_ID = " + this.LocationID + " ORDER BY ATT_yr,ATT_Key ";
            DataSet ds;

            using (CData oData = new CData(SConst.LXConnString.ToString()))
            {
                ds = oData.returnHTEData(commandString);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        TreeNode tn = new TreeNode(dr["ATT_yr"].ToString() + "-" + dr["ATT_Key"].ToString());
                        tvwPZ.Nodes.Add(tn);
                        char[] delimiter = { '|' };

                        DataSet ds0 = oData.getPZDetails(Int32.Parse(dr["ATT_yr"].ToString()), Int32.Parse(dr["ATT_Key"].ToString()));
                        if (ds0.Tables.Count > 0)
                        {
                            foreach (DataRow dr0 in ds0.Tables[0].Rows)
                            {
                                //tn.Nodes.Add("License Year: 200" + dr0["OLYEAR"].ToString());

                                tn.Nodes.Add("Description: " + dr0["PZPDEE"].ToString());
                                tn.Nodes.Add("Application Date: " + oData.returnHTEDate(dr0["PZADAT"].ToString().Trim()));
                                tn.Nodes.Add("Project Type: " + dr0["PZPTYP"].ToString());
                                tn.Nodes.Add("Status Code: " + dr0["PZPSTS"].ToString());
                                tn.Nodes.Add("Alternate ID: " + dr0["PZPAL1"].ToString());
                                tn.Nodes.Add("Project Planner: " + dr0["PZPPLR"].ToString());
                                tn.Nodes.Add("Total Sq Ft: " + dr0["PZPSQF"].ToString());
                            }
                        }
                    }
                }
                else
                {
                    tvwPZ.Nodes.Add("There are no Planning and Zoning items");
                }
            }

            this.Cursor = Cursors.Default;

            this.Cursor = Cursors.Default;
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {


        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SConst.GotDBConn)
            {
                switch (tabControl1.SelectedTab.Text)
                {
                    case "BP":
                        loadBP();
                        break;
                    case "CE":
                        loadCE();
                        break;
                    case "OL":
                        loadOL();
                        break;
                    case "PZ":
                        loadPZ();
                        break;
                    default:
                        loadBP();
                        break;
                }
            }
        }

        private void fmSearchResults_Paint(object sender, PaintEventArgs e)
        {
            //try
            //{
            //    this.Cursor = Cursors.WaitCursor;

            //    //Application.DoEvents();

            //    ESRI.ArcGIS.ArcMapUI.IMxDocument pMxDoc = (ESRI.ArcGIS.ArcMapUI.IMxDocument)this.App.Document;
            //    IMap pMap = (IMap)pMxDoc.FocusMap;
            //    IFeatureLayer pLayer;

            //    using (CSpatialSubs oSpatialSubs = new CSpatialSubs())
            //    {
            //        pLayer = (IFeatureLayer)oSpatialSubs.returnFeatureLayer(pMap, SConst.Taxlots);
            //    }

            //    ICursor pCurs;

            //    IFeatureSelection pFSel = (IFeatureSelection)pLayer;
            //    pFSel.SelectionSet.Search(null, true, out pCurs);

            //    IFeatureCursor pFCurs = (IFeatureCursor)pCurs;
            //    IFeature pFeat = pFCurs.NextFeature();

            //    //Application.DoEvents();

            //    decimal dImpVal = decimal.Parse(pFeat.get_Value(pFeat.Fields.FindField("IMPVALUE")).ToString());
            //    decimal dLandVal = decimal.Parse(pFeat.get_Value(pFeat.Fields.FindField("LANDVALUE")).ToString());

            //    lblAcctNumber.Text = pFeat.get_Value(pFeat.Fields.FindField("ACCOUNT")).ToString();
            //    lblFeeOwner.Text = pFeat.get_Value(pFeat.Fields.FindField("FEEOWNER")).ToString();
            //    lblImpValue.Text = (dImpVal).ToString("C");
            //    lblLandValue.Text = (dLandVal).ToString("C");
            //    lblMailingAddr.Text = pFeat.get_Value(pFeat.Fields.FindField("ADDRESS1")).ToString();
            //    lblMailingAddr2.Text = pFeat.get_Value(pFeat.Fields.FindField("CITY")).ToString() + "," + pFeat.get_Value(pFeat.Fields.FindField("STATE")).ToString() + " " + pFeat.get_Value(pFeat.Fields.FindField("ZIPCODE")).ToString();
            //    lblMaplotNumber.Text = pFeat.get_Value(pFeat.Fields.FindField("MAPLOT")).ToString();
            //    //lblSitusAddr.Text = pFeat.get_Value(pFeat.Fields.FindField("ADDRESSNUM")).ToString() + " " + pFeat.get_Value(pFeat.Fields.FindField("STREETNAME")).ToString();
            //    lblLocID.Text = this.LocationID.ToString();

            //    lblPropClass.Text = pFeat.get_Value(pFeat.Fields.FindField("PROPCLASS")).ToString();
            //    lblPropClassDesc.Text = CMedToolsSubs.returnPropClassDescription(pFeat.get_Value(pFeat.Fields.FindField("PROPCLASS")).ToString(), CMedToolsSubs.getPropClass());

            //    string sFormattedTaxCode = CMedToolsSubs.returnTaxCodeFormatted(pFeat.get_Value(pFeat.Fields.FindField("TAXCODE")).ToString());

            //    lblTaxcode.Text = sFormattedTaxCode;
            //    string[] aTaxCodeInfo = CMedToolsSubs.returnTaxCodeInfo(sFormattedTaxCode, CMedToolsSubs.getTaxCodes());

            //    if (aTaxCodeInfo != null)
            //    {
            //        lblTaxCodeCity.Text = aTaxCodeInfo[0].ToString();
            //        lblTaxCodeSchool.Text = aTaxCodeInfo[1].ToString();
            //        lblTaxCodeWater.Text = aTaxCodeInfo[2].ToString();
            //        lblTaxCodeUrbanRenew.Text = aTaxCodeInfo[3].ToString();
            //        lblTaxCodeFireDist.Text = aTaxCodeInfo[4].ToString();
            //    }
            //    else
            //    {
            //        lblTaxCodeCity.Text = "";
            //        lblTaxCodeSchool.Text = "";
            //        lblTaxCodeWater.Text = "";
            //        lblTaxCodeUrbanRenew.Text = "";
            //        lblTaxCodeFireDist.Text = "";
            //    }

            //    //getLXInfo(pFeat.get_Value(pFeat.Fields.FindField("ACCOUNT")).ToString(), pFeat.get_Value(pFeat.Fields.FindField("MAPNUM")).ToString(), pFeat.get_Value(pFeat.Fields.FindField("TAXLOT")).ToString());

            //    //Application.DoEvents();

            //    if (this.LocationID < 1)
            //    {
            //        if (SConst.GotDBConn)
            //        {
            //            // this should be a Medford Address
            //            using (CData oData = new CData(SConst.LXConnString.ToString()))
            //            {
            //                lblSitusAddr.Text = oData.returnSitusAddressByLocID(this.LocationID);
            //            }
            //        }
            //        //else
            //        //tabControl1.Enabled = false;
            //    }
            //    else
            //    {
            //        // this should be a county address
            //        lblSitusAddr.Text = pFeat.get_Value(pFeat.Fields.FindField("ADDRESSNUM")).ToString() + " " + pFeat.get_Value(pFeat.Fields.FindField("STREETNAME")).ToString();
            //        //tabControl1.Enabled = false;
            //    }

            //    this.Cursor = Cursors.Default;
            //    //Application.DoEvents();
            //}
            //catch (Exception ex)
            //{
            //    string s = ex.Message;
            //    s += "- --- that was it";
            //}
        }

        private void lblTaxcode_Click(object sender, EventArgs e)
        {

        }

        private void setHTEVisibility()
        {
            this.Cursor = Cursors.WaitCursor;
            if (this.m_bShowHTE)
            {
                if (this.LocationID > 0)
                {
                    // go get the default window for the HTE stuff ... BP
                    loadBP();
                    tabControl1.SelectedTab = tabControl1.TabPages[0];
                }

                this.Height = this.Height + tabControl1.Height;
                this.btnOK.Top = this.btnOK.Location.Y + tabControl1.Height;

                pctMinus.Visible = true;
                pctPlus.Visible = false;

                gpbxHTE.Height = gpbxHTE.Height + tabControl1.Height;
                tabControl1.Visible = true;

                //else
                //{
                // this should be a county address
                //lblSitusAddr.Text = pFeat.get_Value(pFeat.Fields.FindField("ADDRESSNUM")).ToString() + " " + pFeat.get_Value(pFeat.Fields.FindField("STREETNAME")).ToString();
                //}
            }
            else
            {
                tabControl1.Visible = false;

                this.Height = this.Height - tabControl1.Height;
                gpbxHTE.Height = gpbxHTE.Height - tabControl1.Height;

                this.btnOK.Top = this.btnOK.Location.Y - tabControl1.Height;

                pctMinus.Visible = false;
                pctPlus.Visible = true;
            }
            this.Cursor = Cursors.Default;
        }

        private void showHTEClick(object sender, EventArgs e)
        {
            if (this.m_bShowHTE)
                this.m_bShowHTE = false;
            else
                this.m_bShowHTE = true;

            this.setHTEVisibility();
        }

        private void gpbxHTE_Enter(object sender, EventArgs e)
        {

        }

        private void fmSearchResults_Shown(object sender, EventArgs e)
        {
            try
            {
                m_bShowHTE = false;
                this.setHTEVisibility();

                this.Cursor = Cursors.WaitCursor;

                //Application.DoEvents();

                ESRI.ArcGIS.ArcMapUI.IMxDocument pMxDoc = (ESRI.ArcGIS.ArcMapUI.IMxDocument)this.App.Document;
                IMap pMap = (IMap)pMxDoc.FocusMap;
                IFeatureLayer pLayer;

                using (CSpatialSubs oSpatialSubs = new CSpatialSubs())
                {
                    pLayer = (IFeatureLayer)oSpatialSubs.returnFeatureLayer(pMap, SConst.Taxlots);
                }

                ICursor pCurs;

                IFeatureSelection pFSel = (IFeatureSelection)pLayer;
                pFSel.SelectionSet.Search(null, true, out pCurs);

                IFeatureCursor pFCurs = (IFeatureCursor)pCurs;
                IFeature pFeat = pFCurs.NextFeature();

                //Application.DoEvents();

                decimal dImpVal = decimal.Parse(pFeat.get_Value(pFeat.Fields.FindField("IMPVALUE")).ToString());
                decimal dLandVal = decimal.Parse(pFeat.get_Value(pFeat.Fields.FindField("LANDVALUE")).ToString());

                lblAcctNumber.Text = pFeat.get_Value(pFeat.Fields.FindField("ACCOUNT")).ToString();
                lblFeeOwner.Text = pFeat.get_Value(pFeat.Fields.FindField("FEEOWNER")).ToString();
                lblImpValue.Text = (dImpVal).ToString("C");
                lblLandValue.Text = (dLandVal).ToString("C");
                lblMailingAddr.Text = pFeat.get_Value(pFeat.Fields.FindField("ADDRESS1")).ToString();
                lblMailingAddr2.Text = pFeat.get_Value(pFeat.Fields.FindField("CITY")).ToString() + "," + pFeat.get_Value(pFeat.Fields.FindField("STATE")).ToString() + " " + pFeat.get_Value(pFeat.Fields.FindField("ZIPCODE")).ToString();
                lblMaplotNumber.Text = pFeat.get_Value(pFeat.Fields.FindField("MAPLOT")).ToString();
                //lblSitusAddr.Text = pFeat.get_Value(pFeat.Fields.FindField("ADDRESSNUM")).ToString() + " " + pFeat.get_Value(pFeat.Fields.FindField("STREETNAME")).ToString();
                lblLocID.Text = this.LocationID.ToString();

                lblPropClass.Text = pFeat.get_Value(pFeat.Fields.FindField("PROPCLASS")).ToString();
                lblPropClassDesc.Text = CMedToolsSubs.returnPropClassDescription(pFeat.get_Value(pFeat.Fields.FindField("PROPCLASS")).ToString(), CMedToolsSubs.getPropClass());

                string sFormattedTaxCode = CMedToolsSubs.returnTaxCodeFormatted(pFeat.get_Value(pFeat.Fields.FindField("TAXCODE")).ToString());

                lblTaxcode.Text = sFormattedTaxCode;
                string[] aTaxCodeInfo = CMedToolsSubs.returnTaxCodeInfo(sFormattedTaxCode, CMedToolsSubs.getTaxCodes());

                if (aTaxCodeInfo != null)
                {
                    lblTaxCodeCity.Text = aTaxCodeInfo[0].ToString();
                    lblTaxCodeSchool.Text = aTaxCodeInfo[1].ToString();
                    lblTaxCodeWater.Text = aTaxCodeInfo[2].ToString();
                    lblTaxCodeUrbanRenew.Text = aTaxCodeInfo[3].ToString();
                    lblTaxCodeFireDist.Text = aTaxCodeInfo[4].ToString();
                }
                else
                {
                    lblTaxCodeCity.Text = "";
                    lblTaxCodeSchool.Text = "";
                    lblTaxCodeWater.Text = "";
                    lblTaxCodeUrbanRenew.Text = "";
                    lblTaxCodeFireDist.Text = "";
                }

                //getLXInfo(pFeat.get_Value(pFeat.Fields.FindField("ACCOUNT")).ToString(), pFeat.get_Value(pFeat.Fields.FindField("MAPNUM")).ToString(), pFeat.get_Value(pFeat.Fields.FindField("TAXLOT")).ToString());

                //Application.DoEvents();

                if (this.LocationID < 1)
                {
                    if (SConst.GotDBConn)
                    {
                        // this should be a Medford Address
                        using (CData oData = new CData(SConst.LXConnString.ToString()))
                        {
                            lblSitusAddr.Text = oData.returnSitusAddressByLocID(this.LocationID);
                        }
                    }
                    //else
                    //tabControl1.Enabled = false;
                }
                else
                {
                    // this should be a county address
                    lblSitusAddr.Text = pFeat.get_Value(pFeat.Fields.FindField("ADDRESSNUM")).ToString() + " " + pFeat.get_Value(pFeat.Fields.FindField("STREETNAME")).ToString();
                    //tabControl1.Enabled = false;
                }

                this.Cursor = Cursors.Default;
                //Application.DoEvents();
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                s += "- --- that was it";
            }
        }

        private void lblShowHTE_Click(object sender, EventArgs e)
        {
            if (this.m_bShowHTE)
                this.m_bShowHTE = false;
            else
                this.m_bShowHTE = true;

            this.setHTEVisibility();
        }

        //private int returnLocationID(string sAccount, string sMapNum, string sTaxlot)
        //{
        //    int iLocID = 0;
        //    SqlConnection oConn = new SqlConnection(CConst.LXConnString.ToString());
        //    SqlDataReader reader;
        //    try
        //    {
        //        oConn.Open();

        //        string commandString = "SELECT LO_Location_ID FROM LXMaster.dbo.Location WHERE Account_Num = '" + sAccount + "' AND Mapid = '" + sMapNum + "' AND Taxlot = '" + ensurePad(sTaxlot) + "'";

        //        SqlCommand cmd = new SqlCommand(commandString, oConn);
        //        reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            iLocID = Int32.Parse(reader["LO_Location_ID"].ToString());
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        oConn.Close();
        //    }

        //    return iLocID;

        //}
    }
}
