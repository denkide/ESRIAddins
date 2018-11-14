using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.ObjectModel;
using System.Security.Principal;
using System.Data.SqlClient;
using System.Data;
using System.DirectoryServices;
using System.Windows.Forms;
using System.Configuration;
using Microsoft.Win32;
using System.Xml;
using System.Xml.XPath;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;

namespace MedfordToolsUtility
{
    public static class CMedToolsSubs
    {
        #region " L O C A L   V A R I A B L E S "

            private static bool m_bResetEditor;

        #endregion

        #region " P U B L I C   P R O P E R T I E S "

            public static bool ResetAsEditor
            {
                get { return m_bResetEditor; }
                set { m_bResetEditor = value; }
            }

            //public static ESRI.ArcGIS.Framework.IApplication App
            //{
            //    get
            //    {
            //        return m_pApp;
            //    }
            //    set
            //    {
            //        m_pApp = value;
            //    }
            //}


        #endregion

        /// <summary>
        ///     Name:
        ///         getObjectProperty
        /// 
        ///     Description:
        ///         Gets a given property value for a given ActiveDirectory object
        /// </summary>
        /// <param name="pstrObject">The ActiveDirectory object to query</param>
        /// <param name="pstrProperty">The property to evaluate</param>
        /// <returns>Property value (string)</returns>
        /// -----------------------------------------------------------------
        /// notes/rev:
        /// 
        public static string getObjectProperty(string pstrObject, string pstrProperty)
        {
            DirectorySearcher objSearch = new DirectorySearcher();
            SearchResult objResult = null;

            try
            {
                objSearch.Filter = String.Format("(SAMAccountName={0})", pstrObject);
                objSearch.PropertiesToLoad.Add(pstrProperty);
                objResult = objSearch.FindOne();
                return objResult.Properties[pstrProperty][0].ToString();
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                return String.Empty;
            }
            catch (System.NullReferenceException)
            {
                return String.Empty;
            }
            catch (System.ArgumentOutOfRangeException)
            {
                return String.Empty;
            }
            catch (Exception)
            {
                //throw;
                return String.Empty;
            }
            finally
            {
                if (objResult != null)
                {
                    objResult = null;
                }
                if (objSearch != null)
                {
                    objSearch.Dispose();
                    objSearch = null;
                }
            }
        }


        /// <summary>
        ///     Name:
        ///         writeEditor
        /// 
        ///     Description:
        ///         This sets the values for the ESRI registry keys to an Editor license.
        /// </summary>
        /// <returns>bool (success or failure)</returns>
        /// -----------------------------------------------------------------
        /// notes/rev:
        /// 
        public static bool writeEditor()
        {
            try
            {
                RegistryKey rk = Registry.LocalMachine;
                RegistryKey sk1 = rk.OpenSubKey("SOFTWARE\\ESRI\\License",true);
                sk1.SetValue("SOFTWARE_CLASS", "Editor");
                sk1.SetValue("SEAT_PREFERENCE", "Float");
                sk1.Close();
                return true;
            }
            catch(Exception ex)
            {
                string err = ex.Message;
                err += "\r\n" + ex.InnerException.Message;
                return false;
            }
        }

        public static string readRegKey(RegistryKey rkKey, string sSubKey, string sValue)
        {
            string sRetVal = "";
            try
            {
                RegistryKey sk = rkKey.OpenSubKey(sSubKey, false);
                sRetVal = sk.GetValue(sValue).ToString();
                sk.Close();
            }
            catch { sRetVal = "";}
 
            return sRetVal;
        }

        public static bool writeRegKey(RegistryKey rkKey, string sSubKey, string sValue, string sWriteThis)
        {
            bool bRetVal = true;
            try
            {
                RegistryKey rk = Registry.LocalMachine.CreateSubKey(sSubKey);
                //RegistryKey sk = rk.OpenSubKey(sSubKey, true);

                //if (sk == null)
                //{
                //    sk = rk.OpenSubKey(sSubKey, true);
                //}
                
                rk.SetValue(sValue, sWriteThis);
                rk.Close();
            }
            catch
            {   
                bRetVal = false;
            }
            return bRetVal;
        }


        /// <summary>
        ///     Name:
        ///         returnStreetTypeFromTaxlotsAddress
        /// 
        ///     Description:
        ///         Parses out a street type from the Jackson County taxlots feature class
        ///         
        /// </summary>
        /// <param name="sStreet">The street attribute</param>
        /// <returns>string</returns>
        /// -----------------------------------------------------------------
        /// notes/rev:
        /// 
        public static string returnStreetTypeFromTaxlotsAddress(string sStreet, Collection<string> sStreetsCollection)
        {
            //return sStreet.Substring(sStreet.LastIndexOf(" ")+1);
            string sRetVal = "";

            if (sStreet.IndexOf(" ") > 0)
            {
                sRetVal = sStreet.Substring(sStreet.LastIndexOf(" ") + 1);      
            }
            else
            {
                sRetVal = "";
            }
            return sRetVal;
        }

        /// <summary>
        ///     Name:
        ///         returnStreetDirectionFromTaxlotsAddress
        /// 
        ///     Description:
        ///         Determines street direction from Jackson County taxlots feature class street
        ///         
        /// </summary>
        /// <param name="sStreet">The street attribute</param>
        /// <returns>string</returns>
        /// -----------------------------------------------------------------
        /// notes/rev:
        /// 
        public static string returnStreetDirectionFromTaxlotsAddress(string sStreet)
        {
            string sRetVal = "";

            if (sStreet.Length > 0)
            {
                if (sStreet.Substring(0, 2).ToUpper() == "N ")
                    sRetVal = "N";
                else if (sStreet.Substring(0, 2).ToUpper() == "S ")
                    sRetVal = "S";
                else if (sStreet.Substring(0, 2).ToUpper() == "E ")
                    sRetVal = "E";
                else if (sStreet.Substring(0, 2).ToUpper() == "W ")
                    sRetVal = "W";
            }
            else
            {
                sRetVal = " ";
            }
            return sRetVal;
        }

        /// <summary>
        ///     Name:
        ///         returnStreetNameFromTaxlotsAddress
        /// 
        ///     Description:
        ///         Parses out a street name from the Jackson County taxlots feature class
        ///         
        /// </summary>
        /// <param name="sStreet">The street attribute</param>
        /// <returns>string</returns>
        /// -----------------------------------------------------------------
        /// notes/rev:
        /// 
        public static string returnStreetNameFromTaxlotsAddress(string sStreet, Collection<string> sStreetsCollection)
        {
            string sRetVal = "";

            try
            {
                if (sStreet.Length > 0)
                {
                    // count the spaces
                    int iSpaces = sStreet.Split(' ').Length - 1;

                    if (iSpaces == 1) //(sStreetsCollection.Contains(sStreet.Substring(sStreet.LastIndexOf(" ") + 1)))
                        sRetVal = sStreet.Substring(0, sStreet.LastIndexOf(" "));
                    else
                    {
                        // there is some wierdness at the end
                        //string s = sStreet.Substring(0, sStreet.LastIndexOf(" ") - 1);
                        sRetVal = sStreet.Substring(0, sStreet.LastIndexOf(" "));  //s.Substring(0, s.LastIndexOf(" ")).Trim();
                    }

                    if (sRetVal.Substring(0, 2).ToUpper() == "N ")
                        sRetVal = sRetVal.Substring(1);
                    else if (sRetVal.Substring(0, 2).ToUpper() == "S ")
                        sRetVal = sRetVal.Substring(1);
                    else if (sRetVal.Substring(0, 2).ToUpper() == "E ")
                        sRetVal = sRetVal.Substring(1);
                    else if (sRetVal.Substring(0, 2).ToUpper() == "W ")
                        sRetVal = sRetVal.Substring(1, sRetVal.LastIndexOf(" "));
                }
                else
                {
                    sRetVal = "";
                }
            }
            catch
            {
                sRetVal = sStreet;
            }
            return sRetVal;
        }

        /// <summary>
        ///     Name:
        ///         layerExists
        /// 
        ///     Description:
        ///         Checks to see if a layer file exists in the layer location
        ///         
        /// </summary>
        /// <param name="sLayer">The layer file to look for</param>
        /// <param name="sLayerLocation">The folder where the layer files are found</param>
        /// <returns></returns>
        /// -----------------------------------------------------------------
        /// notes/rev:
        /// 
        public static bool layerExists(string sLayer, string sLayerLocation)
        {
            if (System.IO.File.Exists(sLayerLocation + sLayer))
                return true;
            else
                return false;
        }


        /// <summary>
        ///     Name:
        ///         returnCityFromTaxCode
        /// 
        ///     Description:
        ///         Returns the city designation of a given taxcode.
        ///         If no taxcode match is found, the location is "Rural"
        ///         
        /// </summary>
        /// <param name="sTaxCodeKey">The taxcode for to search</param>
        /// <returns>string</returns>
        /// -----------------------------------------------------------------
        /// notes/rev:
        ///     
        public static string returnCityFromTaxCode(string sTaxCodeKey, Dictionary<string, string> dctTaxcodeCollection)
        {
            try
            {
                if (dctTaxcodeCollection.ContainsKey(sTaxCodeKey))
                {
                    char[] seperator = { ',' };
                    string[] aTaxCodeInfo = dctTaxcodeCollection[sTaxCodeKey].ToString().Split(seperator);
                    return aTaxCodeInfo[0].ToString();
                }
                else
                    return sTaxCodeKey;
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        ///     Name:
        ///         returnTaxCodeInfo
        /// 
        ///     Description:
        ///         Returns the tax code info for a given taxcode
        ///         
        /// </summary>
        /// <param name="sTaxCodeKey">The taxcode for to search</param>
        /// <returns>string</returns>
        /// -----------------------------------------------------------------
        /// notes/rev:
        ///     
        public static string[] returnTaxCodeInfo(string sTaxCodeKey, Dictionary<string, string> dctTaxcodeCollection)
        {
            try
            {
                if (dctTaxcodeCollection.ContainsKey(sTaxCodeKey))
                {
                    char[] seperator = { ',' };
                    return dctTaxcodeCollection[sTaxCodeKey].ToString().Split(seperator);
                }
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        ///     Name:
        ///         returnPropClassDescription
        /// 
        ///     Description:
        ///         Returns the tax code info for a given taxcode
        ///         
        /// </summary>
        /// <param name="sPropClassKey">The prop class for to search</param>
        /// <returns>string</returns>
        /// -----------------------------------------------------------------
        /// notes/rev:
        ///     
        public static string returnPropClassDescription(string sPropClassKey, Dictionary<string , string> dctPropClassCollection)
        {
            try
            {
                if (dctPropClassCollection.ContainsKey(sPropClassKey))
                {
                    return dctPropClassCollection[sPropClassKey].ToString();
                }
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        public static string returnTaxCodeFormatted(string sTaxCode)
        {
            string sRetVal = "";
            try
            {
                if (sTaxCode.Length < 4)
                    sRetVal = sTaxCode.Substring(0, 1) + "-" + sTaxCode.Substring(1);
                else
                    sRetVal = sTaxCode.Substring(0, 2) + "-" + sTaxCode.Substring(2);
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            return sRetVal;
        }

        public static bool canConnect(string sConnString)
        {
            SqlConnection oConn = new SqlConnection(sConnString);
            try
            {
                oConn.Open();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                oConn.Dispose();
            }
        }

        public static string returnSettingValue(string sSetting, string sSettingsFile)
        {
            XmlTextReader xReader = new XmlTextReader(sSettingsFile);
            xReader.Read();

            string sRetVal = "";

            try
            {
                while (xReader.Read())
                {
                    xReader.MoveToElement();
                    if (xReader.Name == "Setting")
                    {
                        xReader.MoveToFirstAttribute();
                        if (xReader.Value.ToString().ToUpper() == sSetting.ToUpper())
                        {
                            xReader.MoveToNextAttribute();
                            sRetVal = xReader.Value.ToString();
               
                            break;
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                throw new Exception("Exceptions occurred: " + ex.Message); 
            }
            finally
            {
                xReader = null;
            }
            return sRetVal;
        }

        public static Dictionary<string, string> returnConfigNode(string sNode, string sFilter, string sSettingsLoc)
        {
            Dictionary<string, string> dctRetVal = new Dictionary<string, string>();
            
            // sFilter = [@type='Planning']
            // path = "/" + sParentNode + "/" + sNode + sFilter
            // eg: "/Layers/Layer[@type='Planning']"

            try
            {
                string sPath = "//" + sNode;
                if (sFilter.Length > 0)
                    sPath += "[" + sFilter + "]";

                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(sSettingsLoc);

                XmlNodeList nodes = xDoc.SelectNodes(sPath);

                foreach (XmlNode node in nodes)
                {
                    dctRetVal.Add(node.FirstChild.InnerText.ToString(), node.LastChild.InnerText.ToString());
                }
            }
            catch (Exception ex)
            {
                dctRetVal = null;
            }
            return dctRetVal;
        }

        public static Dictionary<string, string> returnSettingsNodes(string sSettingsLoc, string sPath)
        {
            Dictionary<string, string> dctRetVal = new Dictionary<string, string>();
            try
            {
                //string sPath = "//Setting";
                
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(sSettingsLoc);

                XmlNodeList nodes = xDoc.SelectNodes(sPath);

                foreach (XmlNode node in nodes)
                {
                    dctRetVal.Add(node.Attributes[0].Value.ToString(), node.Attributes[1].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                dctRetVal = null;
            }
            return dctRetVal;
        }

        public static bool ensureSettingsFile()
        {
            bool bRetVal = true;
            try
            {
                string sPath = @"I:\ArcGISAddin\ConfigFiles\MedfordTools"; //System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);

                string sDataSettingsLoc = "DataSettings.xml"; // readRegKey(Registry.LocalMachine, "SOFTWARE\\xxxx", "DataSettingsLocation");
                string sPrinterSettingsLoc = "PrintSettings.xml"; // readRegKey(Registry.LocalMachine, "SOFTWARE\\xxxx", "PrinterSettingsLocation");
                string sLayerSettingsLoc = "LayerSettings.xml"; // readRegKey(Registry.LocalMachine, "SOFTWARE\\xxxx", "LayerSettingsLocation");

                if (File.Exists(sPath + "\\" + sDataSettingsLoc))
                {
                    SConst.DataSettingsLocation = sPath + "\\" + sDataSettingsLoc;
                    if (File.Exists(sPath + "\\" + sLayerSettingsLoc))
                    {
                        SConst.LayerSettingsLocation = sPath + "\\" + sLayerSettingsLoc;
                        if (File.Exists(sPath + "\\" + sPrinterSettingsLoc))
                        {
                            SConst.PrintSettingsLocation = sPath + "\\" + sPrinterSettingsLoc;
                        }
                        else { bRetVal = false; }
                    }
                    else {bRetVal = false;}
                }
                else {bRetVal = false;}
            }
            catch(Exception ex)
            {
                string err = ex.Message;
                err += "\r\n" + ex.InnerException.Message;

                System.Windows.Forms.MessageBox.Show(ex.Message);

                return false;
            }
            return bRetVal;
        }

        // --------------------------
        //  4-24-2008
        //  DJR
        //      -- added so that we can use a settings.xml file for config
        // --------------------------
        public static bool checkRequiredSettings()
        {   
            bool bRetVal = false;
            // test for the various settings
            Dictionary<string, string> dctSettings = returnSettingsNodes(SConst.DataSettingsLocation, "//Setting");
            
            if (dctSettings.ContainsKey("MedfordToolsExtension_WSMEDLX_WSMEDLX"))
            {
                if (dctSettings.ContainsKey("MEDSDE_1_PROD_GIS"))
                {
                    if (dctSettings.ContainsKey("Editable_Server"))
                    {
                        if (dctSettings.ContainsKey("Editable_Instance"))
                        {
                            if (dctSettings.ContainsKey("Editable_Database"))
                            {
                                if (dctSettings.ContainsKey("Editable_Version"))
                                {
                                    if (dctSettings.ContainsKey("Editable_AuthMode"))
                                    {
                                        if (dctSettings.ContainsKey("Enable_MedfordContent"))
                                        {
                                            if (dctSettings.ContainsKey("GISConnString"))
                                            {
                                                if (dctSettings.ContainsKey("LXConnString"))
                                                {
                                                    if (dctSettings.ContainsKey("Taxlots_FullName"))
                                                    {
                                                        if (dctSettings.ContainsKey("Taxlots_LayerName"))
                                                        {
                                                            if (dctSettings.ContainsKey("County_Street_Types"))
                                                            {
                                                                if (dctSettings.ContainsKey("Default_Layer_Location"))
                                                                {
                                                                    if (dctSettings.ContainsKey("Enable_EditorControl"))
                                                                    {
                                                                        if (dctSettings.ContainsKey("Organization_Name"))
                                                                        {
                                                                            bRetVal = true;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return bRetVal;
        }

        public static string ensurePad(string val)
        {
            if (val.Length < 5)
            {
                string sTemp = val.PadLeft(5);
                return sTemp.Replace(" ", "0");
            }
            else
                return val;
        }

        public static Dictionary<string, string> getTaxCodes()
        {
            MedfordToolsDAL.CData oData = new MedfordToolsDAL.CData(SConst.GISConnString);
            System.Data.DataSet ds = oData.returnTaxCode();

            Dictionary<string, string> dct = new Dictionary<string, string>(ds.Tables[0].Rows.Count);
            string commaSeperatedVals = "";
            try
            {
                foreach (System.Data.DataRow dr in ds.Tables[0].Rows)
                {
                    commaSeperatedVals = dr["City"].ToString() + ", " + dr["schoolDist"].ToString() + "," + dr["waterDist"].ToString() + "," + dr["urbanRenewalDist"].ToString() + "," + dr["fireDist"].ToString();
                    dct.Add(dr["taxcode"].ToString(), commaSeperatedVals);
                }
            }
            catch
            {
                return null;
            }
            return dct;
        }

        public static Dictionary<string, string> getPropClass()
        {
            MedfordToolsDAL.CData oData = new MedfordToolsDAL.CData(SConst.GISConnString);
            System.Data.DataSet ds = oData.returnPropClass();

            Dictionary<string, string> dct = new Dictionary<string, string>(ds.Tables[0].Rows.Count);

            try
            {
                foreach (System.Data.DataRow dr in ds.Tables[0].Rows)
                {
                    dct.Add(dr["CLASS"].ToString(), dr["CLASS_DESC"].ToString());
                }
            }
            catch
            {
                return null;
            }
           
            return dct;  //CMedToolsSubs.returnConfigNode("PropClass", "", SettingsLocation);
        }


        public static Collection<string> getCountyStreets()
        {
            MedfordToolsDAL.CData oData = new MedfordToolsDAL.CData(SConst.GISConnString);
            System.Data.DataSet ds = oData.returnTaxCode();

            Collection<string> col = new Collection<string>();
            return col;  //CMedToolsSubs.returnConfigNode("PropClass", "", SettingsLocation);
        }

        public static void Encrypt(XmlDocument Doc, string ElementName, SymmetricAlgorithm Key)
        {
            // Check the arguments.  
            if (Doc == null)
                throw new ArgumentNullException("Doc");
            if (ElementName == null)
                throw new ArgumentNullException("ElementToEncrypt");
            if (Key == null)
                throw new ArgumentNullException("Alg");

            ////////////////////////////////////////////////
            // Find the specified element in the XmlDocument
            // object and create a new XmlElemnt object.
            ////////////////////////////////////////////////
            XmlElement elementToEncrypt = Doc.GetElementsByTagName(ElementName)[0] as XmlElement;
            // Throw an XmlException if the element was not found.
            if (elementToEncrypt == null)
            {
                throw new XmlException("The specified element was not found");

            }

            //////////////////////////////////////////////////
            // Create a new instance of the EncryptedXml class 
            // and use it to encrypt the XmlElement with the 
            // symmetric key.
            //////////////////////////////////////////////////

            EncryptedXml eXml = new EncryptedXml();

            byte[] encryptedElement = eXml.EncryptData(elementToEncrypt, Key, false);
            ////////////////////////////////////////////////
            // Construct an EncryptedData object and populate
            // it with the desired encryption information.
            ////////////////////////////////////////////////

            EncryptedData edElement = new EncryptedData();
            edElement.Type = EncryptedXml.XmlEncElementUrl;

            // Create an EncryptionMethod element so that the 
            // receiver knows which algorithm to use for decryption.
            // Determine what kind of algorithm is being used and
            // supply the appropriate URL to the EncryptionMethod element.

            string encryptionMethod = null;

            if (Key is TripleDES)
            {
                encryptionMethod = EncryptedXml.XmlEncTripleDESUrl;
            }
            else if (Key is DES)
            {
                encryptionMethod = EncryptedXml.XmlEncDESUrl;
            }
            if (Key is Rijndael)
            {
                switch (Key.KeySize)
                {
                    case 128:
                        encryptionMethod = EncryptedXml.XmlEncAES128Url;
                        break;
                    case 192:
                        encryptionMethod = EncryptedXml.XmlEncAES192Url;
                        break;
                    case 256:
                        encryptionMethod = EncryptedXml.XmlEncAES256Url;
                        break;
                }
            }
            else
            {
                // Throw an exception if the transform is not in the previous categories
                throw new CryptographicException("The specified algorithm is not supported for XML Encryption.");
            }

            edElement.EncryptionMethod = new EncryptionMethod(encryptionMethod);

            // Add the encrypted element data to the 
            // EncryptedData object.
            edElement.CipherData.CipherValue = encryptedElement;

            ////////////////////////////////////////////////////
            // Replace the element from the original XmlDocument
            // object with the EncryptedData element.
            ////////////////////////////////////////////////////
            EncryptedXml.ReplaceElement(elementToEncrypt, edElement, false);
        }

        public static void Decrypt(XmlDocument Doc, SymmetricAlgorithm Alg)
        {
            // Check the arguments.  
            if (Doc == null)
                throw new ArgumentNullException("Doc");
            if (Alg == null)
                throw new ArgumentNullException("Alg");

            // Find the EncryptedData element in the XmlDocument.
            XmlElement encryptedElement = Doc.GetElementsByTagName("EncryptedData")[0] as XmlElement;

            // If the EncryptedData element was not found, throw an exception.
            if (encryptedElement == null)
            {
                throw new XmlException("The EncryptedData element was not found.");
            }


            // Create an EncryptedData object and populate it.
            EncryptedData edElement = new EncryptedData();
            edElement.LoadXml(encryptedElement);

            // Create a new EncryptedXml object.
            EncryptedXml exml = new EncryptedXml();


            // Decrypt the element using the symmetric key.
            byte[] rgbOutput = exml.DecryptData(edElement, Alg);

            // Replace the encryptedData element with the plaintext XML element.
            exml.ReplaceData(encryptedElement, rgbOutput);

        }

        public static void setConstVals()
        {
            if (ensureSettingsFile())
            {
                if ((SConst.LayerLocation == null) || (SConst.LayerLocation.Length < 1))
                    SConst.LayerLocation = @"I:\GIS\Layers\";

                // set the location for the default layer location
                SConst.LayerLocation = CMedToolsSubs.returnSettingValue("Default_Layer_Location", SConst.DataSettingsLocation);

                // set the flag that allows for City of Medford Specific content
                SConst.EnableMedfordContent = Convert.ToBoolean(CMedToolsSubs.returnSettingValue("Enable_MedfordContent", SConst.DataSettingsLocation).ToString());

                // set the flag that allows for City of Medford Specific content
                SConst.EnableEditorControl = Convert.ToBoolean(CMedToolsSubs.returnSettingValue("Enable_EditorControl", SConst.DataSettingsLocation).ToString());

                // set the organization name
                SConst.OrganizationName = CMedToolsSubs.returnSettingValue("Organization_Name", SConst.DataSettingsLocation).ToString();

                Dictionary<string, string> dctMapLayers = new Dictionary<string, string>();
                dctMapLayers = SConst.PlanningLayers;

                if (CMedToolsSubs.canConnect(SConst.LXConnString))
                    SConst.GotDBConn = true;
                else
                    SConst.GotDBConn = false;
            }
        }
    }
}
