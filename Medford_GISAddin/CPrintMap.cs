using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Medford_GISAddin.Print;


namespace Medford_GISAddin
{
    public class CPrintMap : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public CPrintMap()
        {
        }

        protected override void OnClick()
        {
            //System.Windows.Forms.MessageBox.Show("Printing shall commence");

            fmPrintMap oPrintMapForm = new fmPrintMap();
            oPrintMapForm.ShowDialog();
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
