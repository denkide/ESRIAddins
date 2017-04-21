using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Desktop.AddIns;
using System.IO;
using MedfordToolsUtility;
using Medford_GISAddin.AddLayer;

namespace Medford_GISAddin.AddLayer
{
    public partial class fmAddLayers : Form
    {
        public CloseAddLayersForm CloseForm;
        private Control m_CurrentControl;
        private ToolStripButton m_CurrentClickedButton = null;

        private ucBoundary BoundariesPanel;
        private ucCensus CensusPanel;
        private ucEmergency EmergencyPanel;
        private ucEnvironment EnvironmentPanel;
        private ucInfrastructure InfrastructPanel;
        private ucPhoto PhotoPanel;
        private ucServiceDistricts ServiceDistrictsPanel;
        private ucSoilsTopo SoilsTopoPanel;
        private ucStructures StructuresPanel;
        private ucTaxlots TaxlotsPanel;
        private ucTransportation TransportationPanel;
        private ucUtility UtilityPanel;
        private ucWater WaterPanel;
        private ucZoning ZoningPanel;
        private ucEditor EditorPanel;
        private ucMapService MapServicePanel;

        /// <summary>
        /// Host object of the dockable window
        /// </summary>
        private object Hook
        {
            get;
            set;
        }

        public void reloadForm()
        {
            loadControls();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            if (SConst.m_pCheckedLayers != null)
            {
                if (SConst.m_pCheckedLayers.Count > 0)
                {
                    using (CSpatialSubs oSpatialSubs = new CSpatialSubs())
                    {
                        oSpatialSubs.addLayers(ArcMap.Application, "I://GIS//Layers//"); 
                        // SConst.LayerLocation);
                    }
                }
            }

            //SConst.clearLayerList();
            this.Cursor = Cursors.Default;
            closeForm();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            closeForm();
        }

        public fmAddLayers()
        {
            InitializeComponent();
        }

        private void loadControls()
        {
            string sStartUpTab = CMedToolsSubs.readRegKey(Microsoft.Win32.Registry.LocalMachine, "SOFTWARE\\City of Medford", "AddLayersTab");   //SOFTWARE\\City of Medford

            Control ctl = returnNewControl(sStartUpTab);
            m_CurrentControl = ctl;

            ToolStripButton tsb = setToolStripButton(sStartUpTab);
            m_CurrentClickedButton = tsb;
            doButtonChange(tsb);
        }


        private Control returnNewControl(string pCurrentButtonTag)
        {
            Control pRetVal;

            switch (pCurrentButtonTag.ToUpper())
            {
                case "MEDFORDTOOLSEXTENSION.ADDLAYER.UCBOUNDARIES":
                    if (BoundariesPanel == null)
                        BoundariesPanel = new ucBoundary();

                    BoundariesPanel.Tag = pCurrentButtonTag;
                    BoundariesPanel.Dock = DockStyle.Fill;

                    pRetVal = (Control)BoundariesPanel;
                    break;
                case "MEDFORDTOOLSEXTENSION.ADDLAYER.UCCENSUS":
                    if (CensusPanel == null)
                        CensusPanel = new ucCensus();
                    CensusPanel.Tag = pCurrentButtonTag;
                    CensusPanel.Dock = DockStyle.Fill;

                    pRetVal = (Control)CensusPanel;
                    break;
                case "MEDFORDTOOLSEXTENSION.ADDLAYER.UCEMERGENCY":

                    if (EmergencyPanel == null)
                        EmergencyPanel = new ucEmergency();
                    EmergencyPanel.Tag = pCurrentButtonTag;
                    EmergencyPanel.Dock = DockStyle.Fill;

                    pRetVal = (Control)EmergencyPanel;
                    break;
                case "MEDFORDTOOLSEXTENSION.ADDLAYER.UCENVIRONMENT":

                    if (EnvironmentPanel == null)
                        EnvironmentPanel = new ucEnvironment();
                    EnvironmentPanel.Tag = pCurrentButtonTag;
                    EnvironmentPanel.Dock = DockStyle.Fill;

                    pRetVal = (Control)EnvironmentPanel;
                    break;
                case "MEDFORDTOOLSEXTENSION.ADDLAYER.UCINFRASTRUCTURE":

                    if (InfrastructPanel == null)
                        InfrastructPanel = new ucInfrastructure();
                    InfrastructPanel.Tag = pCurrentButtonTag;
                    InfrastructPanel.Dock = DockStyle.Fill;

                    pRetVal = (Control)InfrastructPanel;
                    break;
                case "MEDFORDTOOLSEXTENSION.ADDLAYER.UCPHOTO":

                    if (PhotoPanel == null)
                        PhotoPanel = new ucPhoto();
                    PhotoPanel.Tag = pCurrentButtonTag;
                    PhotoPanel.Dock = DockStyle.Fill;

                    pRetVal = (Control)PhotoPanel;
                    break;
                case "MEDFORDTOOLSEXTENSION.ADDLAYER.UCSERVICEDISTRICTS":

                    if (ServiceDistrictsPanel == null)
                        ServiceDistrictsPanel = new ucServiceDistricts();
                    ServiceDistrictsPanel.Tag = pCurrentButtonTag;
                    ServiceDistrictsPanel.Dock = DockStyle.Fill;

                    pRetVal = (Control)ServiceDistrictsPanel;
                    break;
                case "MEDFORDTOOLSEXTENSION.ADDLAYER.UCSOILSTOPO":

                    if (SoilsTopoPanel == null)
                        SoilsTopoPanel = new ucSoilsTopo();
                    SoilsTopoPanel.Tag = pCurrentButtonTag;
                    SoilsTopoPanel.Dock = DockStyle.Fill;

                    pRetVal = (Control)SoilsTopoPanel;
                    break;
                case "MEDFORDTOOLSEXTENSION.ADDLAYER.UCSTRUCTURES":

                    if (StructuresPanel == null)
                        StructuresPanel = new ucStructures();
                    StructuresPanel.Tag = pCurrentButtonTag;
                    StructuresPanel.Dock = DockStyle.Fill;

                    pRetVal = (Control)StructuresPanel;
                    break;
                case "MEDFORDTOOLSEXTENSION.ADDLAYER.UCTAXLOTS":

                    if (TaxlotsPanel == null)
                        TaxlotsPanel = new ucTaxlots();
                    TaxlotsPanel.Tag = pCurrentButtonTag;
                    TaxlotsPanel.Dock = DockStyle.Fill;

                    pRetVal = (Control)TaxlotsPanel;
                    break;
                case "MEDFORDTOOLSEXTENSION.ADDLAYER.UCTRANSPORTATION":

                    if (TransportationPanel == null)
                        TransportationPanel = new ucTransportation();
                    TransportationPanel.Tag = pCurrentButtonTag;
                    TransportationPanel.Dock = DockStyle.Fill;

                    pRetVal = (Control)TransportationPanel;
                    break;
                case "MEDFORDTOOLSEXTENSION.ADDLAYER.UCWATER":

                    if (WaterPanel == null)
                        WaterPanel = new ucWater();
                    WaterPanel.Tag = pCurrentButtonTag;
                    WaterPanel.Dock = DockStyle.Fill;

                    pRetVal = (Control)WaterPanel;
                    break;
                case "MEDFORDTOOLSEXTENSION.ADDLAYER.UCZONING":

                    if (ZoningPanel == null)
                        ZoningPanel = new ucZoning();
                    ZoningPanel.Tag = pCurrentButtonTag;
                    ZoningPanel.Dock = DockStyle.Fill;

                    pRetVal = (Control)ZoningPanel;
                    break;
                case "MEDFORDTOOLSEXTENSION.ADDLAYER.UCUTILITY":

                    if (UtilityPanel == null)
                        UtilityPanel = new ucUtility();
                    UtilityPanel.Tag = pCurrentButtonTag;
                    UtilityPanel.Dock = DockStyle.Fill;

                    pRetVal = (Control)UtilityPanel;
                    break;
                case "MEDFORDTOOLSEXTENSION.ADDLAYER.UCEDITOR":

                    if (EditorPanel == null)
                        EditorPanel = new ucEditor();

                    EditorPanel.Tag = pCurrentButtonTag;
                    EditorPanel.Dock = DockStyle.Fill;

                    pRetVal = (Control)EditorPanel;
                    break;
                case "MEDFORDTOOLSEXTENSION.ADDLAYER.UCMAPSERVICES":

                    if (MapServicePanel == null)
                        MapServicePanel = new ucMapService();

                    MapServicePanel.Tag = pCurrentButtonTag;
                    MapServicePanel.Dock = DockStyle.Fill;

                    pRetVal = (Control)MapServicePanel;
                    break;
                default:
                    ////ucBoundaries defaultPanel = new ucBoundaries();
                    //ucBoundary defaultPanel = new ucBoundary();

                    //defaultPanel.Tag = pCurrentButtonTag;
                    //defaultPanel.Dock = DockStyle.Fill;
                    if (BoundariesPanel == null)
                        BoundariesPanel = new ucBoundary();

                    BoundariesPanel.Tag = pCurrentButtonTag;
                    BoundariesPanel.Dock = DockStyle.Fill;

                    pRetVal = (Control)BoundariesPanel;
                    break;
            }

            return pRetVal;
        }

        private ToolStripButton setToolStripButton(string sTag)
        {
            switch (sTag.ToUpper())
            {
                case "MEDFORDTOOLSEXTENSION.ADDLAYER.UCBOUNDARIES":
                    return tsbBoundaries;
                case "MEDFORDTOOLSEXTENSION.ADDLAYER.UCCENSUS":
                    return tsbCensus;
                case "MEDFORDTOOLSEXTENSION.ADDLAYER.UCEMERGENCY":
                    return tsbEmergency;
                case "MEDFORDTOOLSEXTENSION.ADDLAYER.UCENVIRONMENT":
                    return tsbEnvironment;
                case "MEDFORDTOOLSEXTENSION.ADDLAYER.UCINFRASTRUCTURE":
                    return tsbInfrastructure;
                case "MEDFORDTOOLSEXTENSION.ADDLAYER.UCPHOTO":
                    return tsbPhoto;
                case "MEDFORDTOOLSEXTENSION.ADDLAYER.UCSERVICEDISTRICTS":
                    return tsbServiceDistricts;
                case "MEDFORDTOOLSEXTENSION.ADDLAYER.UCSOILSTOPO":
                    return tsbSoils;
                case "MEDFORDTOOLSEXTENSION.ADDLAYER.UCSTRUCTURES":
                    return tsbStructures;
                case "MEDFORDTOOLSEXTENSION.ADDLAYER.UCTAXLOTS":
                    return tsbTaxlots;
                case "MEDFORDTOOLSEXTENSION.ADDLAYER.UCTRANSPORTATION":
                    return tsbTransportation;
                case "MEDFORDTOOLSEXTENSION.ADDLAYER.UCWATER":
                    return tsbWater;
                case "MEDFORDTOOLSEXTENSION.ADDLAYER.UCZONING":
                    return tsbZoning;
                case "MEDFORDTOOLSEXTENSION.ADDLAYER.UCUTILITY":
                    return tsbUtility;
                case "MEDFORDTOOLSEXTENSION.ADDLAYER.UCEDITOR":
                    return tsbEditor;
                case "MEDFORDTOOLSEXTENSION.ADDLAYER.UCMAPSERVICES":
                    return tspMapServices;
                default:
                    return tsbBoundaries;
            }
        }

        private void clearCurrentCheckButton()
        {
            // get rid of the current check
            m_CurrentClickedButton.Checked = false;
            return;
        }

        private void setCurrentButton(ToolStripButton tsb)
        {
            // set this new button to be the current button
            m_CurrentClickedButton = tsb;
            m_CurrentClickedButton.Select();
            m_CurrentClickedButton.Checked = true;
            m_CurrentClickedButton.CheckState = CheckState.Checked;
            return;
        }

        private void doButtonChange(ToolStripButton tsbutton)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                // clear it out first
                splitContainer1.Panel2.Controls.Clear();

                Control p_NewControl;
                string p_ControlName = tsbutton.Tag.ToString();

                clearCurrentCheckButton();
                setCurrentButton(tsbutton);

                if (p_ControlName != m_CurrentControl.Name.ToString())
                {
                    p_NewControl = splitContainer1.Panel2.Controls[p_ControlName];
                    if (p_NewControl == null)
                    {
                        p_NewControl = returnNewControl(m_CurrentClickedButton.Tag.ToString());
                        p_NewControl.Name = p_ControlName;
                        p_NewControl.Dock = DockStyle.Fill;
                        splitContainer1.Panel2.Controls.Add(p_NewControl);

                        if (tsbutton.Text.ToUpper() == "EDITORS")
                        {
                            if (SConst.EnableEditorControl)
                            {
                                loadEditForm(p_NewControl);
                            }
                        }
                    }

                    //m_CurrentControl.Visible = false;
                    p_NewControl.Visible = true;
                    m_CurrentControl = p_NewControl;
                }
                Cursor.Current = Cursors.Default;
                splitContainer1.Panel2.Refresh();
            }
            catch (Exception ex)
            {
                //string s = ex.Message;
                //s += " WEEEEE";
            }
        }

        private void loadEditForm(Control ctl)
        {
            ucEditor ucEdit = (ucEditor)ctl;
            ucEdit.App = this.Hook as IApplication;

            ucEdit.CloseForm = new CloseDelegate(this.closeForm);
            //ucEdit.CloseViewerApp = new CloseApp(this.closeApp);
            ucEdit.loadForm();
        }

        private void closeForm()
        {

            if (BoundariesPanel != null)
                BoundariesPanel = null;

            if (CensusPanel != null)
                CensusPanel = null;

            if (EmergencyPanel != null)
                EmergencyPanel = null;

            if (EnvironmentPanel != null)
                EnvironmentPanel = null;

            if (InfrastructPanel != null)
                InfrastructPanel = null;

            if (PhotoPanel != null)
                PhotoPanel = null;

            if (ServiceDistrictsPanel != null)
                ServiceDistrictsPanel = null;

            if (SoilsTopoPanel != null)
                SoilsTopoPanel = null;

            if (StructuresPanel != null)
                StructuresPanel.Dispose();

            if (TaxlotsPanel != null)
                TaxlotsPanel = null;

            if (TransportationPanel != null)
                TransportationPanel = null;

            if (WaterPanel != null)
                WaterPanel = null;

            if (ZoningPanel != null)
                ZoningPanel = null;

            if (UtilityPanel != null)
                UtilityPanel = null;

            if (EditorPanel != null)
                EditorPanel = null;

            if (MapServicePanel != null)
                MapServicePanel = null;

            string sTag = m_CurrentControl.Tag.ToString();
            try
            {
                CMedToolsSubs.writeRegKey(Microsoft.Win32.Registry.LocalMachine, "SOFTWARE\\City of Medford", "AddLayersTab", sTag);
            }
            catch(Exception ex)
            {
                // just move on  ... don't throw an error 
            }

            //CAddLayers oAddLayers = new CAddLayers();
            //oAddLayers.closeForm();

            this.Close();
            this.Dispose();

            //reloadForm();
        }

        private void buttonClick(object sender, EventArgs e)
        {
            try
            {
                ToolStripItem tsItem = (ToolStripItem)sender;

                if (m_CurrentClickedButton != (ToolStripButton)tsItem)
                    doButtonChange((ToolStripButton)tsItem);
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
        }

        private void fmAddLayers_Paint(object sender, PaintEventArgs e)
        {
            if (splitContainer1.Panel2.Controls.Count < 1)
                loadControls(); //MessageBox.Show("Error: Panel could not be found.");
        }

        private void fmAddLayers_Load(object sender, EventArgs e)
        {
            //if (!SConst.EnableEditorControl)
            //{
            //    tsbEditor.Visible = false;
            //}

            if (SConst.LayerLocation == null)
                CMedToolsSubs.setConstVals();
            else if (SConst.LayerLocation.Length < 1)
                CMedToolsSubs.setConstVals();

            lblInfo.Text = SConst.LayerLocation;

            try
            {
                tsbEditor.Visible = true;
                loadControls();
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
        }

        private void tsbBoundaries_DoubleClick(object sender, EventArgs e)
        {
            // do nothing
            string s = "weee -";
            s += " haw!";
        }


    }
}
