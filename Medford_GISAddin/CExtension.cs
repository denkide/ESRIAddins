using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Desktop.AddIns;
using ESRI.ArcGIS.Editor;
using ESRI.ArcGIS.Geodatabase;

using MedfordToolsUtility;
using MedfordToolsDAL;

namespace Medford_GISAddin
{
    public class CExtension : ESRI.ArcGIS.Desktop.AddIns.Extension
    {
        private IMap m_map;
        private static CExtension s_extension;
        //private static IDockableWindow s_dockWindow;
        //private IApplication m_pApplication;
        //private ExtensionState m_pExtState;

        // Event member variables
        //private IDocumentEvents_Event m_docEvents;

        private IEditor m_editor;

        private IEditEvents_Event Events
        {
            get { return ArcMap.Editor as IEditEvents_Event; }
        }

        public CExtension()
        {
        }

        protected override void OnStartup()
        {
            s_extension = this;
            Initialize();

            UID editorUID = new UID();
            editorUID.Value = "esriEditor.Editor";
            
            this.m_editor = ArcMap.Application.FindExtensionByCLSID(editorUID) as IEditor;

            Events.OnStartEditing += new IEditEvents_OnStartEditingEventHandler(Events_OnStartEditing);
            Events.OnStopEditing += new IEditEvents_OnStopEditingEventHandler(Events_OnStopEditing);

            m_map = ArcMap.Document.FocusMap;

            try
            {
                if (CMedToolsSubs.ensureSettingsFile())
                {
                    setConstVals();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(SConst.OrganizationName + " GIS Error: settings not found");
                    //    //this.State = ExtensionState.Disabled;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "STARTUP ERROR!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        protected override void OnShutdown()
        {
            Uninitialize();

            m_map = null;
            s_extension = null;

            base.OnShutdown();
        }

        protected override bool OnSetState(ExtensionState state)
        {
            // Optionally check for a license here
            this.State = ExtensionState.Enabled; // state;

            //if (state == ExtensionState.Enabled)
            //    Initialize();
            //else
            //    Uninitialize();

            return base.OnSetState(state);
        }

        protected override ExtensionState OnGetState()
        {
            return ExtensionState.Enabled;  //this.State;
        }

        // Privates
        private void Initialize()
        {
            // If the extension hasn't been started yet or it's been turned off, bail
            //if (s_extension == null || this.State != ExtensionState.Enabled)
            //    return;

            // Update the UI
            m_map = ArcMap.Document.FocusMap;
        }

        private void Uninitialize()
        {
            if (s_extension == null)
                return;
        }

        internal static bool IsExtensionEnabled()
        {
            if (s_extension == null)
                GetExtension();

            if (s_extension == null)
                return false;

            return s_extension.State == ExtensionState.Enabled;
        }

        private static CExtension GetExtension()
        {
            if (s_extension == null)
            {
                // Call FindExtension to load this just-in-time extension.
                UID id = new UIDClass();
                id.Value = ThisAddIn.IDs.CExtension;
                ArcMap.Application.FindExtensionByCLSID(id);
            }

            return s_extension;
        }

        //Invoked at the start of an editor session.
        private void Events_OnStartEditing()
        {
            //if (!checkSelectedLayer())
            //{
            //    //m_editor.UndoOperation();
            //    System.Windows.Forms.MessageBox.Show("Please ensure that you have selected the edited feature class in the editor window.\r\n", "City of Medford GIS Tools: Update Data", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            //    return;
            //}

            //System.Windows.Forms.MessageBox.Show("Start Editing");
            Events.OnChangeFeature += new IEditEvents_OnChangeFeatureEventHandler(Events_OnChangeFeature);
            Events.OnCreateFeature += new IEditEvents_OnCreateFeatureEventHandler(Events_OnCreateFeature);
            Events.OnDeleteFeature += new IEditEvents_OnDeleteFeatureEventHandler(Events_OnDeleteFeature);
            //Events.AfterDrawSketch += new IEditEvents_AfterDrawSketchEventHandler(Events_AfterDrawSketch);
        }

        void Events_AfterDrawSketch(ESRI.ArcGIS.Display.IDisplay pDpy)
        {
            //if (!checkSelectedLayer())
            //{
            //    //m_editor.UndoOperation();
            //    System.Windows.Forms.MessageBox.Show("Please ensure that you have selected the edited feature class in the editor window.\r\n", "City of Medford GIS Tools: Update Data", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            //    return;
            //}
        }

        void Events_OnDeleteFeature(IObject obj)
        {
            //setEditableLayer((IFeature)obj);
        }

        void Events_OnCreateFeature(IObject obj)
        {
            //setEditableLayer((IFeature)obj);
        }

        void Events_OnChangeFeature(IObject obj)
        {
            //setEditableLayer((IFeature)obj);
        }

        private bool checkSelectedLayer()
        {
            bool bRetVal = false;

            IEditLayers pEditLayers = this.m_editor as IEditLayers;
            IFeatureLayer pFLayer = pEditLayers.CurrentLayer as IFeatureLayer;

            if (pFLayer != null)
                bRetVal = true;

            return bRetVal;
        }


        //Invoked at the end of an editor session (Editor->Stop Editing).
        private void Events_OnStopEditing(bool Save)
        {
            CSpatialSubs oSubs = new CSpatialSubs();

            IEditLayers pEditLayers = this.m_editor as IEditLayers;
            IFeatureLayer pFLayer = pEditLayers.CurrentLayer as IFeatureLayer;

            if (pFLayer == null)
            {
                //System.Windows.Forms.MessageBox.Show("Please ensure that you have selected the edited feature class in the editor window.\r\n", "City of Medford GIS Tools: Update Data", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                //returnEditLayer();
                //return;
            }
            else
            {
                if (Save) saveEdits(pFLayer);
            }
        }

        private void saveEdits(IFeatureLayer pFLayer)
        {
            CSpatialSubs oSubs = new CSpatialSubs();

            IFeatureClass pFC = pFLayer.FeatureClass;
            IWorkspace pWkSpc = oSubs.returnWorkspace(pFC);

            if (pWkSpc.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
            {
                if (SConst.EnableMedfordContent == true)
                {
                    if (System.Windows.Forms.MessageBox.Show("Would you like to update all the production and publication databases?", SConst.OrganizationName + " GIS Tools: Update Data", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        try
                        {
                            using (CSpatialSubs oSpatialSubs = new CSpatialSubs())
                            {
                                 oSpatialSubs.updateAllSDEDatabases(pFC);
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Windows.Forms.MessageBox.Show("The update of the publication and production databases did not succeed.\r\n" + ex.Message, SConst.OrganizationName + " GIS Tools: Update Data", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void setEditableLayer(IFeature pFeat)
        {
            CSpatialSubs oSubs = new CSpatialSubs();

            IEditLayers pEditLayers = this.m_editor as IEditLayers;
            IFeatureLayer pFLayer = oSubs.returnFLayerByName(pFeat.Class.AliasName, true);
            pEditLayers.SetCurrentLayer(pFLayer, 0);
        }

        private void returnEditLayer()
        {
            fmEditSelection oEditSel = new fmEditSelection(ref m_editor);
            oEditSel.FormClosed += new System.Windows.Forms.FormClosedEventHandler(oEditSel_FormClosed);
            oEditSel.Show();
        }

        private void oEditSel_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            IEditLayers pEditLayers = this.m_editor as IEditLayers;
            IFeatureLayer pFLayer = pEditLayers.CurrentLayer as IFeatureLayer;

            if (pFLayer != null)
            {
                //m_editor.StopEditing(true);
                saveEdits(pFLayer);
            }
            
        }

        private void setConstVals()
        {
            // set the location for the default layer location
            SConst.LayerLocation = CMedToolsSubs.returnSettingValue("Default_Layer_Location", SConst.DataSettingsLocation);

            //if ((SConst.LayerLocation == null) || (SConst.LayerLocation.Length < 1))
            //    SConst.LayerLocation = @"I:\GIS\Layers\"; 

            // set the flag that allows for City of Medford Specific content
            SConst.EnableMedfordContent = Convert.ToBoolean(CMedToolsSubs.returnSettingValue("Enable_MedfordContent", SConst.DataSettingsLocation).ToString());

            // set the flag that allows for City of Medford Specific content
            SConst.EnableEditorControl = Convert.ToBoolean(CMedToolsSubs.returnSettingValue("Enable_EditorControl", SConst.DataSettingsLocation).ToString());

            // set the organization name
            SConst.OrganizationName = CMedToolsSubs.returnSettingValue("Organization_Name", SConst.DataSettingsLocation).ToString();
        }
    }

}
