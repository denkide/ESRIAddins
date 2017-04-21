using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ESRI.ArcGIS.Desktop.AddIns;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;

namespace Medford_GISAddin
{
    public class CConvertMapToLL : ESRI.ArcGIS.Desktop.AddIns.Tool
    {
        fmCoords oCoords; 

        public CConvertMapToLL()
        {
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }

        protected override void OnMouseDown(ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs arg)
        {
            if (oCoords != null)
            {
                try
                {
                    oCoords.Close();
                    oCoords.Dispose();
                }
                catch
                {
                    string s = "continue on";
                    s += " -- ok, thanks";
                }
            }

            if ((arg.X > 0) && (arg.Y > 0))
            {
                oCoords = new fmCoords();
                oCoords.LoadValues(arg.X, arg.Y);
            }
        }
    }
}
