using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.Remoting;
using System.Collections;
using ESRI.ArcGIS;
using System.IO;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using MedfordToolsUtility;
using MedfordToolsDAL;
using System.Data;

namespace Medford_GISAddin.TaxlotSearch
{
    public partial class fmTLSearch : Form
    {

        //const int WM_KEYDOWN = 0x100;
        //const int WM_KEYUP = 0x101;

        #region " M O D U L E   L E V E L  V A R I A B L E S "

        private IApplication m_pApp;
        private int m_iSearchType;

        enum SearchType
        {
            OWNER,
            SITUS,
            MAPLOT,
            ACCOUNT,
            MAPNUMBER
        }

        #endregion

        #region " P R O P E R T I E S "

        public ESRI.ArcGIS.Framework.IApplication App
        {
            get { return this.m_pApp; }
            set { this.m_pApp = value; }
        }

        #endregion

        public fmTLSearch()
        {
            InitializeComponent();
            this.App = (IApplication)ArcMap.ThisApplication;

            if ((SConst.LayerLocation == null) || (SConst.LayerLocation.Length < 1))
                CMedToolsSubs.setConstVals();
        }

        private void fmTLSearch_Load(object sender, EventArgs e)
        {
            //Application.AddMessageFilter(this);
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        //// this grabs the enter key down event and makes it a submit
        //public bool PreFilterMessage(ref Message msg)
        //{
        //    Keys keyCode = (Keys)(int)msg.WParam & Keys.KeyCode;
        //    if (msg.Msg == WM_KEYDOWN && keyCode == Keys.Return)
        //    {
        //        doSearch();
        //        return true;
        //    }
        //    return false;
        //}


        private void btnSearch_Click(object sender, EventArgs e)
        {
            doSearch();
        }

        private void doSearch()
        {

            this.Cursor = Cursors.WaitCursor;

            using (CSpatialSubs oSpatialSubs = new CSpatialSubs())
            {
                // check if taxlots is in the data frame
                if (!oSpatialSubs.doesMapLayerExist(SConst.Taxlots, this.App))
                {
                    //CMedToolsSubs.addFeatureLayerToMap(m_pApp, CConst.Taxlots, false);
                    oSpatialSubs.addSingleLayerToMap(m_pApp, SConst.LayerLocation, SConst.TaxlotLayerName);
                }
            }

            string sWhere = "";

            switch (m_iSearchType)
            {
                case 0: //Int32.Parse(SearchType.OWNER):
                    sWhere = "FEEOWNER LIKE '%" + txtOwner.Text.Replace(",", " ") + "%'";
                    break;
                //case 1: //Int32.Parse(SearchType.SITUS):
                //    sWhere = "SITEADD = '" + txtSitus.Text + "'";
                //    break;
                case 2: //Int32.Parse(SearchType.MAPLOT):
                    sWhere = "MAPLOT = '" + txtMaplot.Text + "'";
                    break;
                case 3: //Int32.Parse(SearchType.ACCOUNT):
                    sWhere = "ACCOUNT = " + txtAccount.Text + "";
                    break;
                case 4: //Int32.Parse(SearchType.MAPNUMBER):
                    sWhere = "MAPNUM = '" + txtMapnum.Text + "'";
                    break;
                default:

                    break;
            }

            if (sWhere.Length > 0)
            {
                getResults(sWhere);
            }

            this.Cursor = Cursors.Default;
        }

        private void setSearch(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            int iSearchType = Int32.Parse(t.Tag.ToString());
            switch (iSearchType)
            {
                case 0: //SearchType.OWNER:
                    this.txtAccount.Text = "";
                    this.txtMaplot.Text = "";
                    this.txtMapnum.Text = "";
                    this.txtSitus.Text = "";

                    break;
                case 1: //SearchType.SITUS:
                //this.txtAccount.Text = "";
                //this.txtMaplot.Text = "";
                //this.txtMapnum.Text = "";
                //this.txtOwner.Text = "";
                //break;
                case 2: //SearchType.MAPLOT:
                    this.txtAccount.Text = "";
                    this.txtOwner.Text = "";
                    this.txtMapnum.Text = "";
                    this.txtSitus.Text = "";
                    break;
                case 3: //SearchType.ACCOUNT:
                    this.txtOwner.Text = "";
                    this.txtMaplot.Text = "";
                    this.txtMapnum.Text = "";
                    this.txtSitus.Text = "";
                    break;
                case 4: //SearchType.MAPNUMBER:
                    this.txtAccount.Text = "";
                    this.txtMaplot.Text = "";
                    this.txtOwner.Text = "";
                    this.txtSitus.Text = "";
                    break;
                default:
                    this.txtAccount.Text = "";
                    this.txtMaplot.Text = "";
                    this.txtMapnum.Text = "";
                    this.txtSitus.Text = "";
                    break;
            }
            m_iSearchType = iSearchType;
        }

        private void searchTaxlots()
        {


        }

        private void getResults(string sWhere)
        {
            CSpatialSubs oSpatialSubs = new CSpatialSubs();
            object oResults = oSpatialSubs.searchTaxlots(sWhere, m_pApp);

            try
            {
                if (oResults is IFeatureCursor)
                {
                    // this means that there were multiple results returned 
                    // from the user search criteria
                    // so ... show the multiple results window
                    this.Hide();

                    // show multiple results
                    fmMultipleResults oMultRes = new fmMultipleResults();
                    oMultRes.SearchResults = (IFeatureCursor)oResults;
                    oMultRes.App = this.m_pApp;
                    oMultRes.Show(new WindowWrapper((System.IntPtr)m_pApp.hWnd));

                    this.Dispose();
                }
                else
                {
                    // this means that there was only one feature returned from 
                    // the user search criteria

                    IFeature pFeat = (IFeature)oResults;
                    if (pFeat != null)
                    {
                        // zoom to table
                        this.Hide();

                        string sAccount = oSpatialSubs.returnValueFromSingleSelection("ACCOUNT", this.App, SConst.Taxlots);
                        string sMaplot = oSpatialSubs.returnValueFromSingleSelection("MAPLOT", this.App, SConst.Taxlots);
                        string sAddNum = oSpatialSubs.returnValueFromSingleSelection("ADDRESSNUM", this.App, SConst.Taxlots);
                        if (sAddNum.IndexOf(" ") > 0)
                            sAddNum = "";
                        string sStreet = oSpatialSubs.returnValueFromSingleSelection("STREETNAME", this.App, SConst.Taxlots);
                        string sObjectID = oSpatialSubs.returnValueFromSingleSelection("OBJECTID", this.App, SConst.Taxlots);
                        object oLocResults = new object();

                        // go get the location id and check for subaccounts

                        //if (CMedToolsSubs.canConnect(CConst.LXConnString))
                        if (SConst.GotDBConn)
                        {
                            using (CData oData = new CData(SConst.LXConnString.ToString()))
                            {
                                string sDir = CMedToolsSubs.returnStreetDirectionFromTaxlotsAddress(sStreet);
                                string sStreetName = CMedToolsSubs.returnStreetNameFromTaxlotsAddress(sStreet, SConst.CountyStreets);
                                string sStreetType = CMedToolsSubs.returnStreetTypeFromTaxlotsAddress(sStreet, SConst.CountyStreets);
                                oLocResults = oData.returnLocID(sAddNum, sDir, sStreetName, sStreetType, sMaplot, sAccount);
                            }
                        }
                        else
                            oLocResults = null;

                        if (oLocResults is Hashtable)
                        {
                            // the hashtable means that there are multiple subaccounts
                            // so ... go to the sub account window
                            fmSubAccount oSubAccount = new fmSubAccount();
                            oSubAccount.App = this.App;
                            oSubAccount.ObjectID = sObjectID;
                            oSubAccount.SubAccounts = oLocResults as Hashtable;
                            oSubAccount.Show(new WindowWrapper((System.IntPtr)m_pApp.hWnd));
                        }
                        else
                        {
                            // there is only one account here ...
                            // so ... go to the results
                            fmSearchResults oSearchResults = new fmSearchResults();
                            oSearchResults.ObjectID = sObjectID;
                            oSearchResults.App = this.App;
                            if (oLocResults != null)
                                oSearchResults.LocationID = (int)oLocResults;
                            else
                                oSearchResults.LocationID = 0;
                            oSearchResults.Show(new WindowWrapper((System.IntPtr)m_pApp.hWnd));
                        }
                        this.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("There were no matches found.", "Taxlot Search: Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // reset the form
                    }
                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                s += "done";
            }
            finally
            {
                oSpatialSubs.Dispose();
            }
        }

        private void handleKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                doSearch();
        }

        private void txtOwner_Enter(object sender, EventArgs e)
        {
            txtSitus.Text = "";
            txtMapnum.Text = "";
            txtMaplot.Text = "";
            txtAccount.Text = "";
        }

        private void txtSitus_Enter(object sender, EventArgs e)
        {
            txtOwner.Text = "";
            txtMapnum.Text = "";
            txtMaplot.Text = "";
            txtAccount.Text = "";
        }

        private void txtMaplot_Enter(object sender, EventArgs e)
        {
            txtSitus.Text = "";
            txtMapnum.Text = "";
            txtOwner.Text = "";
            txtAccount.Text = "";
        }

        private void txtAccount_Enter(object sender, EventArgs e)
        {
            txtSitus.Text = "";
            txtMapnum.Text = "";
            txtMaplot.Text = "";
            txtOwner.Text = "";
        }

        private void txtMapnum_Enter(object sender, EventArgs e)
        {
            txtSitus.Text = "";
            txtOwner.Text = "";
            txtMaplot.Text = "";
            txtAccount.Text = "";
        }
    }
}
