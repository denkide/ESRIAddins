using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Medford_GISAddin.TaxlotSearch;

namespace Medford_GISAddin
{
    public class CTaxlotSearch : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public CTaxlotSearch()
        {
        }

        protected override void OnClick()
        {
            //System.Windows.Forms.MessageBox.Show("Printing shall commence");

            fmTLSearch oTLSearchForm = new fmTLSearch();
            oTLSearchForm.ShowDialog();
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
