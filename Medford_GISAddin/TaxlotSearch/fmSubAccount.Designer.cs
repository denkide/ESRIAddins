namespace Medford_GISAddin.TaxlotSearch
{
    partial class fmSubAccount
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

            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvSubAccounts = new System.Windows.Forms.DataGridView();
            this.OBJECTID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FEEOWNER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SITEADD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAPLOT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ACCOUNT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubAccounts)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSubAccounts
            // 
            this.dgvSubAccounts.AllowUserToAddRows = false;
            this.dgvSubAccounts.AllowUserToDeleteRows = false;
            this.dgvSubAccounts.AllowUserToOrderColumns = true;
            this.dgvSubAccounts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSubAccounts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OBJECTID,
            this.LocationID,
            this.FEEOWNER,
            this.SITEADD,
            this.MAPLOT,
            this.ACCOUNT});
            this.dgvSubAccounts.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvSubAccounts.Location = new System.Drawing.Point(12, 82);
            this.dgvSubAccounts.MaximumSize = new System.Drawing.Size(860, 267);
            this.dgvSubAccounts.MinimumSize = new System.Drawing.Size(860, 267);
            this.dgvSubAccounts.MultiSelect = false;
            this.dgvSubAccounts.Name = "dgvSubAccounts";
            this.dgvSubAccounts.ReadOnly = true;
            this.dgvSubAccounts.Size = new System.Drawing.Size(860, 267);
            this.dgvSubAccounts.TabIndex = 1;
            // 
            // OBJECTID
            // 
            this.OBJECTID.HeaderText = "OBJECTID";
            this.OBJECTID.Name = "OBJECTID";
            this.OBJECTID.ReadOnly = true;
            this.OBJECTID.Visible = false;
            // 
            // LocationID
            // 
            this.LocationID.HeaderText = "LocationID";
            this.LocationID.Name = "LocationID";
            this.LocationID.ReadOnly = true;
            // 
            // FEEOWNER
            // 
            this.FEEOWNER.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.FEEOWNER.DefaultCellStyle = dataGridViewCellStyle5;
            this.FEEOWNER.FillWeight = 200F;
            this.FEEOWNER.HeaderText = "Fee Owner";
            this.FEEOWNER.Name = "FEEOWNER";
            this.FEEOWNER.ReadOnly = true;
            this.FEEOWNER.Width = 84;
            // 
            // SITEADD
            // 
            this.SITEADD.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SITEADD.DefaultCellStyle = dataGridViewCellStyle6;
            this.SITEADD.FillWeight = 200F;
            this.SITEADD.HeaderText = "Site Address";
            this.SITEADD.Name = "SITEADD";
            this.SITEADD.ReadOnly = true;
            this.SITEADD.Width = 91;
            // 
            // MAPLOT
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MAPLOT.DefaultCellStyle = dataGridViewCellStyle7;
            this.MAPLOT.HeaderText = "Maplot";
            this.MAPLOT.Name = "MAPLOT";
            this.MAPLOT.ReadOnly = true;
            // 
            // ACCOUNT
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ACCOUNT.DefaultCellStyle = dataGridViewCellStyle8;
            this.ACCOUNT.HeaderText = "Account";
            this.ACCOUNT.Name = "ACCOUNT";
            this.ACCOUNT.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(772, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "The account that you selected has sub accounts associated with it. This is usuall" +
                "y a result of an apartment complex or condominium complex.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(231, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Please select the unit that you want the info for.";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(770, 367);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(102, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "Show Details";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // fmSubAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 413);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvSubAccounts);
            this.MaximumSize = new System.Drawing.Size(900, 447);
            this.MinimumSize = new System.Drawing.Size(900, 447);
            this.Name = "fmSubAccount";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "GIS Tools: Sub Accounts";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.fmSubAccount_Paint);
            this.Load += new System.EventHandler(this.fmSubAccount_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubAccounts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSubAccounts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.DataGridViewTextBoxColumn OBJECTID;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationID;
        private System.Windows.Forms.DataGridViewTextBoxColumn FEEOWNER;
        private System.Windows.Forms.DataGridViewTextBoxColumn SITEADD;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAPLOT;
        private System.Windows.Forms.DataGridViewTextBoxColumn ACCOUNT;
    }
}