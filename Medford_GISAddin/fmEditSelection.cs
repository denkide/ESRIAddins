using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ADF.COMSupport;
using ESRI.ArcGIS.ADF.Connection.AGS;
using ESRI.ArcGIS.ADF.Connection.Local;
using ESRI.ArcGIS.ADF.Serialization;
using ESRI.ArcGIS.Desktop.AddIns;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Editor;
using ESRI.ArcGIS.Geodatabase;

namespace Medford_GISAddin
{
    public partial class fmEditSelection : Form
    {
        IEditor m_pEditor;
        public fmEditSelection(ref IEditor pEditor)
        {
            InitializeComponent();

            m_pEditor = pEditor;
        }

        private void fmEditSelection_Load(object sender, EventArgs e)
        {
            IMap pMap = (IMap)ArcMap.Document.ActiveView.FocusMap;
            IEnumLayer pEnumLayers = pMap.get_Layers(null, false);

            try
            {
                ILayer pLayer = pEnumLayers.Next();
                while (pLayer != null)
                {
                    if (pLayer.Visible)
                    {
                        this.lbLayers.Items.Add(pLayer.Name);
                    }
                    pLayer = pEnumLayers.Next();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                //pMxDoc = null;
                pMap = null;
                pEnumLayers = null;
            }
        }

        private void lbLayers_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            CSpatialSubs oSubs = new CSpatialSubs();

            IFeatureLayer pFLayer = oSubs.returnFLayerByName(lbLayers.SelectedItem.ToString(), true);
            IEditLayers pEditLayers = this.m_pEditor as IEditLayers;
            pEditLayers.SetCurrentLayer(pFLayer,0);
            this.Close();
        }

    }
}
