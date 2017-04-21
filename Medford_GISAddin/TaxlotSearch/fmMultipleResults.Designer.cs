namespace Medford_GISAddin.TaxlotSearch
{
    partial class fmMultipleResults
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.OBJECTID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FEEOWNER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SITEADD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SitusCity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAPLOT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ACCOUNT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CITY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ADDRESSNUM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STREETNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnZoom = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnViewDetails = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.AllowUserToOrderColumns = true;
            this.dgvResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OBJECTID,
            this.FEEOWNER,
            this.SITEADD,
            this.SitusCity,
            this.MAPLOT,
            this.ACCOUNT,
            this.CITY,
            this.ADDRESSNUM,
            this.STREETNAME});
            this.dgvResults.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvResults.Location = new System.Drawing.Point(12, 12);
            this.dgvResults.MaximumSize = new System.Drawing.Size(860, 267);
            this.dgvResults.MinimumSize = new System.Drawing.Size(860, 267);
            this.dgvResults.MultiSelect = false;
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.ReadOnly = true;
            this.dgvResults.Size = new System.Drawing.Size(860, 267);
            this.dgvResults.TabIndex = 0;
            this.dgvResults.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvResults_RowHeaderMouseClick);
            this.dgvResults.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dgvResults_RowStateChanged);
            // 
            // OBJECTID
            // 
            this.OBJECTID.HeaderText = "OBJECTID";
            this.OBJECTID.Name = "OBJECTID";
            this.OBJECTID.ReadOnly = true;
            this.OBJECTID.Visible = false;
            // 
            // FEEOWNER
            // 
            this.FEEOWNER.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.FEEOWNER.DefaultCellStyle = dataGridViewCellStyle1;
            this.FEEOWNER.FillWeight = 200F;
            this.FEEOWNER.HeaderText = "Fee Owner";
            this.FEEOWNER.Name = "FEEOWNER";
            this.FEEOWNER.ReadOnly = true;
            this.FEEOWNER.Width = 84;
            // 
            // SITEADD
            // 
            this.SITEADD.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SITEADD.DefaultCellStyle = dataGridViewCellStyle2;
            this.SITEADD.FillWeight = 200F;
            this.SITEADD.HeaderText = "Site Address";
            this.SITEADD.Name = "SITEADD";
            this.SITEADD.ReadOnly = true;
            this.SITEADD.Width = 91;
            // 
            // SitusCity
            // 
            this.SitusCity.HeaderText = "City";
            this.SitusCity.Name = "SitusCity";
            this.SitusCity.ReadOnly = true;
            this.SitusCity.Visible = false;
            // 
            // MAPLOT
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MAPLOT.DefaultCellStyle = dataGridViewCellStyle3;
            this.MAPLOT.HeaderText = "Maplot";
            this.MAPLOT.Name = "MAPLOT";
            this.MAPLOT.ReadOnly = true;
            // 
            // ACCOUNT
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ACCOUNT.DefaultCellStyle = dataGridViewCellStyle4;
            this.ACCOUNT.HeaderText = "Account";
            this.ACCOUNT.Name = "ACCOUNT";
            this.ACCOUNT.ReadOnly = true;
            // 
            // CITY
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CITY.DefaultCellStyle = dataGridViewCellStyle5;
            this.CITY.HeaderText = "Owner City";
            this.CITY.Name = "CITY";
            this.CITY.ReadOnly = true;
            // 
            // ADDRESSNUM
            // 
            this.ADDRESSNUM.HeaderText = "Address Number";
            this.ADDRESSNUM.Name = "ADDRESSNUM";
            this.ADDRESSNUM.ReadOnly = true;
            this.ADDRESSNUM.Visible = false;
            // 
            // STREETNAME
            // 
            this.STREETNAME.HeaderText = "Street Name";
            this.STREETNAME.Name = "STREETNAME";
            this.STREETNAME.ReadOnly = true;
            this.STREETNAME.Visible = false;
            // 
            // btnZoom
            // 
            this.btnZoom.Location = new System.Drawing.Point(603, 295);
            this.btnZoom.Name = "btnZoom";
            this.btnZoom.Size = new System.Drawing.Size(78, 23);
            this.btnZoom.TabIndex = 1;
            this.btnZoom.Text = "Zoom to all";
            this.btnZoom.UseVisualStyleBackColor = true;
            this.btnZoom.Click += new System.EventHandler(this.btnZoom_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(797, 295);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close ";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnViewDetails
            // 
            this.btnViewDetails.Location = new System.Drawing.Point(702, 295);
            this.btnViewDetails.Name = "btnViewDetails";
            this.btnViewDetails.Size = new System.Drawing.Size(75, 23);
            this.btnViewDetails.TabIndex = 3;
            this.btnViewDetails.Text = "View Details";
            this.btnViewDetails.UseVisualStyleBackColor = true;
            this.btnViewDetails.Click += new System.EventHandler(this.btnViewDetails_Click);
            // 
            // fmMultipleResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 340);
            this.Controls.Add(this.btnViewDetails);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnZoom);
            this.Controls.Add(this.dgvResults);
            this.Name = "fmMultipleResults";
            this.Text = "GIS Tools: Multiple Search Results";
            this.Load += new System.EventHandler(this.fmMultipleResults_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.fmMultipleResults_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.Button btnZoom;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnViewDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn OBJECTID;
        private System.Windows.Forms.DataGridViewTextBoxColumn FEEOWNER;
        private System.Windows.Forms.DataGridViewTextBoxColumn SITEADD;
        private System.Windows.Forms.DataGridViewTextBoxColumn SitusCity;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAPLOT;
        private System.Windows.Forms.DataGridViewTextBoxColumn ACCOUNT;
        private System.Windows.Forms.DataGridViewTextBoxColumn CITY;
        private System.Windows.Forms.DataGridViewTextBoxColumn ADDRESSNUM;
        private System.Windows.Forms.DataGridViewTextBoxColumn STREETNAME;
    }
}