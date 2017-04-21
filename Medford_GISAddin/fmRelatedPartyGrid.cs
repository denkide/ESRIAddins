using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Medford_GISAddin
{
    public partial class fmRelatedPartyGrid : Form
    {
        public fmRelatedPartyGrid()
        {
            InitializeComponent();
        }

        public fmRelatedPartyGrid(DataTable source)
        {
            InitializeComponent();
            this.dataGridView1.DataSource = source;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                try
                {
                    // Add the selection to the clipboard.
                    Clipboard.SetDataObject(this.dataGridView1.GetClipboardContent());

                    MessageBox.Show("The selected rows have been copied to your clipboard.");
                }
                catch (System.Runtime.InteropServices.ExternalException)
                {
                    MessageBox.Show("The Clipboard could not be accessed. Please try again.");
                }
            }
            else
                MessageBox.Show("There are no rows selected.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.dataGridView1.SelectAll();
        }

    }
}