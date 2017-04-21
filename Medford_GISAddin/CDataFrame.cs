using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Medford_GISAddin.DataFrame;

namespace Medford_GISAddin
{
    public class CDataFrame : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public CDataFrame()
        {
        }

        protected override void OnClick()
        {
            fmAddDataFrame oDataFrame = new fmAddDataFrame();
            oDataFrame.ShowDialog();
        }

        protected override void OnUpdate()
        {
        }
    }
}
