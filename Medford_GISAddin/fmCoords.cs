using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;


namespace Medford_GISAddin
{
    public partial class fmCoords : Form
    {
        private IActiveView m_focusMap;

        public int _iMapX;
        public int _iMapY;

        public int MapX
        {
            get { return _iMapX; }
            set { this._iMapX = value; }
        }

        public int MapY
        {
            get { return _iMapY; }
            set { this._iMapY = value; }
        }


        public fmCoords()
        {
            InitializeComponent();
            
            this._iMapX = 0;
            this._iMapY = 0;
        }

        public void LoadValues(int? mapX, int? mapY)
        {
            double lat = 0;
            double lon = 0;

            if (mapX.HasValue)
                this._iMapX = mapX.Value;
            else
            {
                System.Windows.Forms.MessageBox.Show("Invalid Map X coordinate!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (mapY.HasValue)
                this._iMapY = mapY.Value;
            else
            {
                System.Windows.Forms.MessageBox.Show("Invalid Map Y coordinate!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            IMxDocument mxDoc = ArcMap.Document;
            m_focusMap = mxDoc.FocusMap as IActiveView;

            IPoint point = m_focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(this.MapX, this.MapY) as IPoint;

            projectCoords(out lat, out lon, point);

            this.txtLatDD.Text = lat.ToString();
            this.txtLonDD.Text = lon.ToString();

            this.txtLatDMS.Text = convertDDToDMS(lat);
            this.txtLonDMS.Text = convertDDToDMS(lon);

            this.Show();
        }

        private string convertDDToDMS(double inDD)
        {
            string sOutDMS = "";

            double dblDeg = Math.Truncate(inDD);
            double dblMin = 0;
            double dblSec = 0;
            double dblTemp = (Math.Abs(inDD) - Math.Abs(dblDeg)) * 60;
            dblMin = Math.Truncate(dblTemp);
            dblSec = Math.Truncate((Math.Abs(dblTemp) - Math.Abs(dblMin)) * 60);

            sOutDMS = dblDeg.ToString() + " " + dblMin + " " + dblSec;

            return sOutDMS;
        }

        private void projectCoords(out double lat, out double lon, IPoint pPoint)
        {
            ISpatialReference pSR;
            SpatialReferenceEnvironment pSREnv = new SpatialReferenceEnvironmentClass();

            // using the WKID 2270 == Oregon State Plane South
            // =-======================================================
            IProjectedCoordinateSystem pPCS = pSREnv.CreateProjectedCoordinateSystem(2270);
            pSR = pPCS;

            IPoint pLocalPoint = new PointClass();
            pLocalPoint.SpatialReference = pSR;
            pLocalPoint.PutCoords(pPoint.X, pPoint.Y);

            ISpatialReference pWGSSR;
            SpatialReferenceEnvironment pSREnv2 = new SpatialReferenceEnvironmentClass();

            // this uses the esri enum instead of the 4326 wkid
            // again, either is fine
            IGeographicCoordinateSystem pGCS = pSREnv2.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_WGS1984);
            pWGSSR = pGCS;
            pWGSSR.SetFalseOriginAndUnits(-180, -90, 1000000);

            pLocalPoint.Project(pWGSSR);

            lat = pLocalPoint.Y;
            lon = pLocalPoint.X;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
