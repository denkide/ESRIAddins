using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using MedfordToolsUtility;
using MedfordToolsDAL;
using System.Data;
namespace Medford_GISAddin.TaxlotSearch
{
    public partial class fmSubAccount : Form
    {
        private string m_sObjectID;
        private IApplication m_pApp;
        private Hashtable m_htSubAccounts;

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

        public Hashtable SubAccounts
        {
            get { return m_htSubAccounts; }
            set { m_htSubAccounts = value; }
        }

        public fmSubAccount()
        {
            InitializeComponent();
            this.App = (IApplication)ArcMap.ThisApplication;
        }

        private void fmSubAccount_Load(object sender, EventArgs e)
        {
            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dgvSubAccounts.SelectedRows.Count == 1)
            {
                this.Hide();

                fmSearchResults oSearchResults = new fmSearchResults();
                oSearchResults.ObjectID = dgvSubAccounts.SelectedRows[0].Cells["OBJECTID"].Value.ToString();
                oSearchResults.LocationID = Convert.ToInt32(dgvSubAccounts.SelectedRows[0].Cells["LocationID"].Value.ToString());
                oSearchResults.App = this.App;
                oSearchResults.Show(new WindowWrapper((System.IntPtr)m_pApp.hWnd));

                this.Dispose();
            }
            else
            {
                MessageBox.Show("Please select one row in the grid.", "Taxlot Search: Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void fmSubAccount_Paint(object sender, PaintEventArgs e)
        {
            IDictionaryEnumerator en = this.SubAccounts.GetEnumerator();
            while (en.MoveNext())
            {
                if (SConst.GotDBConn)
                {
                    DataSet ds;
                    using (CData oData = new CData(SConst.LXConnString.ToString()))
                    {
                        ds = oData.returnOwnerInfo(Int32.Parse(en.Key.ToString()));
                    }
                    DataTable dt = ds.Tables[0];

                    foreach (DataRow r in dt.Rows)
                    {
                        string[] row = { this.ObjectID, en.Key.ToString(), r["RP_Name"].ToString(), r["Situs_Addr"].ToString(), r["MAPLOT"].ToString(), r["Account_Num"].ToString() };
                        dgvSubAccounts.Rows.Add(row);
                    }
                }
            }
            dgvSubAccounts.ClearSelection();
            //dgvResults.SelectAll();
        }


        //private DataSet returnOwnerInfo(int iLocID)
        //{
        //    SqlConnection oConn = new SqlConnection(CConst.LXConnString.ToString());
        //    DataSet dsDataSet = new DataSet();
        //    SqlDataAdapter daData = new SqlDataAdapter();

        //    try
        //    {
        //        oConn.Open();

        //        string commandString = "SELECT RP_Name, Situs_Addr, Maplot,Account_Num  FROM LXMaster.dbo.vw_OwnerParcels WHERE LO_Location_ID = " + iLocID;

        //        SqlCommand cmd = new SqlCommand(commandString, oConn);
        //        daData.SelectCommand = cmd;
        //        daData.Fill(dsDataSet);
        //        return dsDataSet;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        oConn.Close();
        //    }
        //    return null;
        //}
    }
}
