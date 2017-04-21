﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using MedfordToolsUtility;
using MedfordToolsDAL;

namespace Medford_GISAddin.AddLayer
{
    public partial class ucBoundary : UserControl
    {
        public ucBoundary()
        {
            InitializeComponent();
        }

        private void CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            if (chk.Enabled)
            {
                if (chk.Checked)
                    SConst.addLayerToList(chk.Tag.ToString());
                else
                    SConst.removeLayerFromList(chk.Tag.ToString());
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Control ctl in panel1.Controls)
            {
                if (ctl is CheckBox)
                {
                    if (ctl.Tag != null)
                    {
                        //if (CConst.LayerExists(ctl.Tag.ToString()))
                        if (CMedToolsSubs.layerExists(ctl.Tag.ToString() + ".lyr", SConst.LayerLocation))
                            ctl.Enabled = true;
                        else
                            ctl.Enabled = false;
                    }
                    else
                    {
                        ctl.Enabled = false;
                    }
                }
            }
        }

    }
}
