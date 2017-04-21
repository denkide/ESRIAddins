using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Web.Services;
using System.Xml;
using System.Data;
using System.Data.SqlClient;



namespace MedfordToolsDAL
{
    public class CData : IDisposable
    {
        #region " M O D U L E   L E V E L   V A R I A B L E S "

        private string m_sConnStr;
        private SqlConnection m_oConn;
        private bool m_bDisposed = false;

        #endregion

        #region " P R O P E R T I E S"

        public string ConnectionString
        {
            get
            {
                return m_sConnStr;
            }
            set
            {
                this.m_sConnStr = value;
            }
        }

        public SqlConnection DataConnection
        {
            get
            {
                return m_oConn;
            }
            set
            {
                this.m_oConn = value;
            }
        }

        #endregion

        #region " C O N S T R U C T O R S"

        public CData(string sConnStr)
        {
            m_sConnStr = sConnStr;
            this.OpenConnection();
        }

        #endregion

        #region " D E S T R U C T O R S "

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposeManagedResources)
        {
            if (!this.m_bDisposed)
            {
                if (disposeManagedResources)
                {
                    //this.DestroyMEDLXServer("MEDLX");

                    m_sConnStr = null;
                    m_oConn.Dispose();
                    m_oConn = null;
                    m_bDisposed = true;
                }
            }
        }

        #endregion


        public bool OpenConnection()
        {
            if (ConnectionString.Length < 1) { return false; }
            try
            {
                if (this.DataConnection != null)
                {
                    this.DataConnection.Dispose();
                }

                this.DataConnection = new SqlConnection(ConnectionString);
                this.DataConnection.Open();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public ArrayList getUserLayers(string sFirstName, string sLastName)
        {
            ArrayList userLayers = new ArrayList();
            SqlDataReader reader;
            try
            {
                string commandString = "SELECT FCName FROM vw_FCEditors WHERE FirstName = '" + sFirstName + "' AND LastName = '" + sLastName + "'";

                SqlCommand cmd = new SqlCommand(commandString, this.DataConnection);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    userLayers.Add(reader["FCName"].ToString());
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
            }

            return userLayers;

        }


        /// <summary>
        ///     Name:
        ///         returnLocID
        /// 
        ///     Description:
        ///         This returns an HTE LocationID from the input parameters.
        /// </summary>
        /// <param name="sAddrNum">The street address number</param>
        /// <param name="sStreetDir">The street direction (N,S,E,W)</param>
        /// <param name="sStreetName">The street name</param>
        /// <param name="sMaplot">The maplot of the location</param>
        /// <param name="sAccount">The account of the location</param>
        /// <returns>bool</returns>
        /// -----------------------------------------------------------------
        /// notes/rev:
        /// 
        public object returnLocID(string sAddrNum, string sStreetDir, string sStreetName, string sStreetType, string sMaplot, string sAccount)
        {
            DataSet dsDataSet = new DataSet();
            SqlDataAdapter daData = new SqlDataAdapter();

            try
            {
                string commandString = "SELECT LO_Location_ID, Situs_Addr FROM LXMaster.dbo.Location WHERE LEFT(Account_Num,8) = '" + sAccount.Trim() + "'";
                if (sMaplot.Trim().Length > 0)
                    commandString += " AND MAPLOT = '" + sMaplot.Trim() + "'";
                if (sAddrNum.Trim().Length > 0)
                    commandString += " AND Street_Num = " + Int32.Parse(sAddrNum.Trim());
                if (sStreetDir.Trim().Length > 0)    
                    commandString += " AND Street_Dir = '" + sStreetDir.Trim() + "'"; 
                if (sStreetName.Trim().Length > 0)
                    commandString += " AND Street_Name = '" + sStreetName.Trim() + "'";
                //if (sStreetType.Trim().Length > 0)
                //    commandString += " AND Street_Sfx = '" + sStreetType.Trim() + "'";

                SqlCommand cmd = new SqlCommand(commandString, this.DataConnection);
                daData.SelectCommand = cmd;
                daData.Fill(dsDataSet);

                DataTable dt = dsDataSet.Tables[0];

                if (dt.Rows.Count > 1)
                {
                    Hashtable htRetVal = new Hashtable();
                    foreach (DataRow dr in dt.Rows)
                    {
                        htRetVal.Add(Int32.Parse(dr["LO_Location_ID"].ToString()), dr["Situs_Addr"].ToString());
                    }
                    return htRetVal;
                }
                else
                {
                    int iRetVal = 0;
                    if (dt.Rows.Count > 0)
                        iRetVal = Int32.Parse(dt.Rows[0].ItemArray.GetValue(0).ToString());

                    return iRetVal;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {

            }
            return null;
        }


        public int returnLocID(string sAccount, string sMapNum, string sTaxlot)
        {
            int iLocID = 0;
            SqlDataReader reader;
            try
            {
                string commandString = "SELECT LO_Location_ID FROM LXMaster.dbo.Location WHERE Account_Num = '" + sAccount + "' AND Mapid = '" + sMapNum + "' AND Taxlot = '" + ensurePad(sTaxlot) + "'";

                SqlCommand cmd = new SqlCommand(commandString, this.DataConnection);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    iLocID = Int32.Parse(reader["LO_Location_ID"].ToString());
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {

            }

            return iLocID;

        }

        /// <summary>
        ///     Name:
        ///         returnLocID
        /// 
        ///     Description:
        ///         This returns a situs address from LXMaster based on a location id.
        /// </summary>
        /// <param name="LocID">The HTE LocationID</param>
        /// <returns>bool</returns>
        /// -----------------------------------------------------------------
        /// notes/rev:
        /// 
        public string returnSitusAddressByLocID(int LocID)
        {
            string sRetVal = "";

            try
            {
                string commandString = "SELECT Situs_Addr FROM LXMaster.dbo.Location WHERE LO_Location_ID = " + LocID;

                SqlCommand cmd = new SqlCommand(commandString, this.DataConnection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    sRetVal = reader["Situs_Addr"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
 
            }
            return sRetVal;
        }


        /// <summary>
        ///     Name:
        ///         getBPDetails
        /// 
        ///     Description:
        ///         This returns the building permit info with the year / permit number
        ///         from the WSMEDLX web service.
        /// </summary>
        /// <param name="sYear">The BP year</param>
        /// <param name="sPermit">The BP permit ID</param>
        /// <returns>string (a pipe delimited string of name / value pairs formatted for display)</returns>
        /// -----------------------------------------------------------------
        /// notes/rev:
        /// 
        public string getBPDetails(string sYear, string sPermit)
        {
            WSMEDLX.WSMEDLX oData = new WSMEDLX.WSMEDLX();
            string sRetVal = "";

            try
            {
                XmlDocument xDoc = new XmlDocument();

                XmlNode xNode = (XmlNode)oData.ReturnBPPermits(Int32.Parse(sYear), Int32.Parse(sPermit));
                xDoc.AppendChild(xDoc.ImportNode(xNode, true));
                XmlNodeReader xReader = new XmlNodeReader(xNode);

                DataSet ds = new DataSet();
                ds.ReadXml(xReader);

                foreach(DataRow dr in ds.Tables[0].Rows)
                {
                    sRetVal = "Type: " + dr["BPPTCD"].ToString() + "|Status Date: " +  this.returnHTEDate(dr["BPPSTD"].ToString()) + "|Expiration Date: " + this.returnHTEDate(dr["BPPEXD"].ToString());
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                oData.Dispose();
            }
            return sRetVal;
        }


        /// <summary>
        ///     Name:
        ///         getCEDetails
        /// 
        ///     Description:
        ///         This returns the code enforcement info with the year / permit number
        ///         from the WSMEDLX web service.
        /// </summary>
        /// <param name="sYear">The CE year</param>
        /// <param name="sPermit">The CE permit ID</param>
        /// <returns>string (a pipe delimited string of name / value pairs formatted for display)</returns>
        /// -----------------------------------------------------------------
        /// notes/rev:
        /// 
        public string getCEDetails(string sYear, string sPermit)
        {
            WSMEDLX.WSMEDLX oData = new WSMEDLX.WSMEDLX();
            string sRetVal = "";

            try
            {
                XmlDocument xDoc = new XmlDocument();

                XmlNode xNode = (XmlNode)oData.ReturnCEInfo(Int32.Parse(sYear), Int32.Parse(sPermit));
                xDoc.AppendChild(xDoc.ImportNode(xNode, true));
                XmlNodeReader xReader = new XmlNodeReader(xNode);

                DataSet ds = new DataSet();
                ds.ReadXml(xReader);

                foreach(DataRow dr in ds.Tables[0].Rows)
                {
                    sRetVal = "Case Status: " + dr["CECSTS"].ToString() + "|Date Case Opened: " + this.returnHTEDate(dr["CECSDT"].ToString()) + "|Case Code: " + dr["CECTCD"].ToString().Substring(1, 2) + "|Staff: " + dr["CEDFID"].ToString();
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                oData.Dispose();
            }
            return sRetVal;
        }


        /// <summary>
        ///     Name:
        ///         getOLDetails
        /// 
        ///     Description:
        ///         This returns the code enforcement info with the year / permit number
        ///         from the WSMEDLX web service.
        /// </summary>
        /// <param name="iLocationID">The HTE LocationID year</param>
        /// <param name="iLicenseID">The OL License ID</param>
        /// <returns>dataset</returns>
        /// -----------------------------------------------------------------
        /// notes/rev:
        /// 
        public DataSet getOLDetails(int iLocationID, int iLicenseID)
        {
            WSMEDLX.WSMEDLX oData = new WSMEDLX.WSMEDLX();
            string sRetVal = "";
            DataSet ds = new DataSet();

            try
            {
                XmlDocument xDoc = new XmlDocument();

                XmlNode xNode = (XmlNode)oData.ReturnOLByLocationAndLicense(iLocationID,iLicenseID);
                xDoc.AppendChild(xDoc.ImportNode(xNode, true));
                XmlNodeReader xReader = new XmlNodeReader(xNode);

                ds.ReadXml(xReader);

                return ds;

            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                oData.Dispose();
            }
            return null;
        }


        /// <summary>
        ///     Name:
        ///         getPZDetails
        /// 
        ///     Description:
        ///         This returns the code enforcement info with the year / permit number
        ///         from the WSMEDLX web service.
        /// </summary>
        /// <param name="iYear">The PZ year</param>
        /// <param name="iPermit">The PZ permit ID</param>
        /// <returns>dataset</returns>
        /// -----------------------------------------------------------------
        /// notes/rev:
        /// 
        public DataSet getPZDetails(int iYear, int iPermit)
        {
            WSMEDLX.WSMEDLX oData = new WSMEDLX.WSMEDLX();
            string sRetVal = "";

            try
            {
                XmlDocument xDoc = new XmlDocument();

                XmlNode xNode = (XmlNode)oData.ReturnPlanProject(iYear, iPermit);
                xDoc.AppendChild(xDoc.ImportNode(xNode, true));
                XmlNodeReader xReader = new XmlNodeReader(xNode);

                DataSet ds = new DataSet();
                ds.ReadXml(xReader);
                return ds;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                oData.Dispose();
            }
            return null;
        }


        /// <summary>
        ///     Name:
        ///         returnHTEData
        /// 
        ///     Description:
        ///         This is a general HTE query function.
        ///         It accepts a sql statement and executes it on the server (LXMaster data connection)
        /// </summary>
        /// <param name="sWhere">The sql statement</param>
        /// <returns>dataset</returns>
        /// -----------------------------------------------------------------
        /// notes/rev:
        /// 
        public DataSet returnHTEData(string sWhere)
        {
            DataSet dsDataSet = new DataSet();
            SqlDataAdapter daData = new SqlDataAdapter();

            try
            {
                string commandString = sWhere;

                SqlCommand cmd = new SqlCommand(commandString, this.DataConnection);
                daData.SelectCommand = cmd;
                daData.Fill(dsDataSet);
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return dsDataSet;
        }


        /// <summary>
        ///     Name:
        ///         returnHTEDate
        /// 
        ///     Description:
        ///         This function formats the dates found in HTE to 
        ///         a normal format.
        /// </summary>
        /// <param name="sHTEDate">The hte date</param>
        /// <returns>string</returns>
        /// -----------------------------------------------------------------
        /// notes/rev:
        /// 
        public string returnHTEDate(string sHTEDate)
        {
            if (sHTEDate.Length > 3)
                return sHTEDate.Substring(1, 2) + "/" + sHTEDate.Substring(3) + "/0" + sHTEDate.Substring(0, 1);
            else
                return sHTEDate;
        }

        public DataSet returnOwnerInfo(int iLocID)
        {
            DataSet dsDataSet = new DataSet();
            SqlDataAdapter daData = new SqlDataAdapter();

            try
            {
                string commandString = "SELECT RP_Name, Situs_Addr, Maplot,Account_Num  FROM LXMaster.dbo.vw_OwnerParcels WHERE LO_Location_ID = " + iLocID;

                SqlCommand cmd = new SqlCommand(commandString, this.DataConnection);
                daData.SelectCommand = cmd;
                daData.Fill(dsDataSet);
                return dsDataSet;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {

            }
            return null;
        }

        public bool isGISDept(string sFirstName, string sLastName)
        {
            bool bRetVal = false;

            DataSet dsDataSet = new DataSet();
            DataTable dt;
            SqlDataAdapter daData = new SqlDataAdapter();

            try
            {
                string commandString = "SELECT DeptPersonnelID FROM GIS.dbo.DeptPersonnel WHERE FirstName = '" + sFirstName + "' AND LastName = '" + sLastName + "' AND IsGIS = 1";

                SqlCommand cmd = new SqlCommand(commandString, this.DataConnection);
                daData.SelectCommand = cmd;
                daData.Fill(dsDataSet);
                dt = dsDataSet.Tables[0];
                if (dt.Rows.Count > 0)
                    bRetVal = true;

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return bRetVal;
        }

        public bool isValidUserLayerCombination(string sFirstName, string sLastName, string sLayer)
        {
            bool bRetVal = false;

            DataSet dsDataSet = new DataSet();
            DataTable dt;
            SqlDataAdapter daData = new SqlDataAdapter();

            try
            {
                string commandString = "SELECT DeptPersonnelID FROM GIS.dbo.vw_FCEditors WHERE FirstName = '" + sFirstName + "' AND LastName = '" + sLastName + "' AND IsEditor=1 AND FCName = '" + sLayer + "'";

                SqlCommand cmd = new SqlCommand(commandString, this.DataConnection);
                daData.SelectCommand = cmd;
                daData.Fill(dsDataSet);
                dt = dsDataSet.Tables[0];
                if (dt.Rows.Count > 0)
                    bRetVal = true;

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return bRetVal;
        }

        public DataSet returnTaxCode()
        {
            DataSet dsDataSet = new DataSet();
            SqlDataAdapter daData = new SqlDataAdapter();

            try
            {
                string commandString = "SELECT taxcode,city,schoolDist,waterDist,urbanRenewalDist,fireDist FROM GIS.dbo.TaxCode";

                SqlCommand cmd = new SqlCommand(commandString, this.DataConnection);
                daData.SelectCommand = cmd;
                daData.Fill(dsDataSet);
                return dsDataSet;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {

            }
            return null;
        }

        public DataSet returnPropClass()
        {
            DataSet dsDataSet = new DataSet();
            SqlDataAdapter daData = new SqlDataAdapter();

            try
            {
                string commandString = "SELECT CLASS, CLASS_DESC FROM GIS.dbo.PropClass";

                SqlCommand cmd = new SqlCommand(commandString, this.DataConnection);
                daData.SelectCommand = cmd;
                daData.Fill(dsDataSet);
                return dsDataSet;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {

            }
            return null;
        }

        private static string ensurePad(string val)
        {
            if (val.Length < 5)
            {
                string sTemp = val.PadLeft(5);
                return sTemp.Replace(" ", "0");
            }
            else
                return val;
        }
    }
}
