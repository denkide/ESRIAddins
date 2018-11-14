using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.Remoting;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using MedfordToolsUtility;
using MedfordToolsDAL;
using Medford_GISAddin.AddLayer;

namespace Medford_GISAddin
{
    public delegate void RestartEditorApp();
    public delegate void CloseDelegate();
    public delegate void CloseAddLayersForm();

    public class CAddLayers : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        //private static IDockableWindow m_dockWindow;

        private fmAddLayers m_addLayersForm; 
        private static bool m_bIsFormOpened = false;

        public CAddLayers()
        {
            CloseAddLayersForm closeFormDelegate = new CloseAddLayersForm(closeForm);
            
        }

        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;

            if (m_addLayersForm != null)
                m_addLayersForm = null;

            try
            {
                m_addLayersForm = new fmAddLayers();
                m_addLayersForm.Show();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message); 
            }

        }


        public void closeForm()
        {
            m_addLayersForm.Close();
            m_addLayersForm.Dispose();
            m_addLayersForm = null;

            m_bIsFormOpened = false;
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;

            if (!m_bIsFormOpened)
                this.Dispose();
        }

        //internal static IDockableWindow GetAddLayerstWindow()
        //{
        //    //System.Windows.Forms.MessageBox.Show("Get Window");
        //    if (CMedToolsSubs.ensureSettingsFile())
        //    {
        //        //System.Windows.Forms.MessageBox.Show("Got file");
        //        CMedToolsSubs.setConstVals();

        //        // Only get/create the dockable window if they ask for it
        //        if (m_dockWindow == null)
        //        {
        //            // Use GetDockableWindow directly intead of FromID as we want the client IDockableWindow not the internal class
        //            UID dockWinID = new UIDClass();
        //            dockWinID.Value = ThisAddIn.IDs.AddLayersDockWin;

        //            //System.Windows.Forms.MessageBox.Show("ID = " + dockWinID.Value.ToString());

        //            m_dockWindow = ArcMap.DockableWindowManager.GetDockableWindow(dockWinID);

        //            //System.Windows.Forms.MessageBox.Show("Added!"); 

        //            //s_extension.UpdateSelCountDockWin();

        //            m_bIsFormOpened = true;
        //        }

        //        return m_dockWindow;
        //    }
        //    else
        //    {
        //        System.Windows.Forms.MessageBox.Show("Cannot find the settings files!");
        //        return null;
        //    }
        //}

        public void clearCheckBoxes(UserControl userCtl)
        {
            foreach (Control ctl in userCtl.Controls)
            {
                if (ctl is CheckBox)
                {
                    CheckBox chk = ctl as CheckBox;
                    //chk.Enabled = true;
                    chk.Checked = false;
                }
            }
        }

        private void clearChecks()
        {
            ucBoundary BoundariesPanel = new ucBoundary();
            clearCheckBoxes(BoundariesPanel);
            BoundariesPanel = null;

            ucCensus CensusPanel = new ucCensus();
            clearCheckBoxes(CensusPanel);
            CensusPanel = null;

            ucEmergency EmergencyPanel = new ucEmergency();
            clearCheckBoxes(EmergencyPanel);
            EmergencyPanel = null;

            ucEnvironment EnvironmentPanel = new ucEnvironment();
            clearCheckBoxes(EnvironmentPanel);
            EnvironmentPanel = null;

            ucInfrastructure InfrastructPanel = new ucInfrastructure();
            clearCheckBoxes(InfrastructPanel);
            InfrastructPanel = null;

            ucPhoto PhotoPanel = new ucPhoto();
            clearCheckBoxes(PhotoPanel);
            PhotoPanel = null;

            ucServiceDistricts ServiceDistrictsPanel = new ucServiceDistricts();
            clearCheckBoxes(ServiceDistrictsPanel);
            ServiceDistrictsPanel = null;

            ucSoilsTopo SoilsTopoPanel = new ucSoilsTopo();
            clearCheckBoxes(SoilsTopoPanel);
            SoilsTopoPanel = null;

            ucStructures StructuresPanel = new ucStructures();
            clearCheckBoxes(StructuresPanel);
            StructuresPanel = null;

            ucTaxlots TaxlotsPanel = new ucTaxlots();
            clearCheckBoxes(TaxlotsPanel);
            TaxlotsPanel = null;
            
            ucTransportation TransportationPanel = new ucTransportation();
            clearCheckBoxes(TransportationPanel);
            TransportationPanel = null;
            
            ucWater WaterPanel = new ucWater();
            clearCheckBoxes(WaterPanel);
            WaterPanel = null;
            
            ucZoning ZoningPanel = new ucZoning();
            clearCheckBoxes(ZoningPanel);
            ZoningPanel = null;
            
            ucUtility UtilityPanel = new ucUtility();
            clearCheckBoxes(UtilityPanel);
            UtilityPanel = null;
            
            ucEditor EditorPanel = new ucEditor();
            clearCheckBoxes(EditorPanel);
            EditorPanel = null;

            ucMapService MapServicePanel = new ucMapService();
            clearCheckBoxes(MapServicePanel);
            MapServicePanel = null;
        }
    }

}
