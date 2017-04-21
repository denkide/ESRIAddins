namespace Medford_GISAddin
{
    partial class fmCoords
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
            this.lblDDTitle = new System.Windows.Forms.Label();
            this.lblDMSTitle = new System.Windows.Forms.Label();
            this.txtLatDD = new System.Windows.Forms.TextBox();
            this.txtLonDD = new System.Windows.Forms.TextBox();
            this.txtLonDMS = new System.Windows.Forms.TextBox();
            this.txtLatDMS = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblDDTitle
            // 
            this.lblDDTitle.AutoSize = true;
            this.lblDDTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDDTitle.Location = new System.Drawing.Point(23, 22);
            this.lblDDTitle.Name = "lblDDTitle";
            this.lblDDTitle.Size = new System.Drawing.Size(114, 16);
            this.lblDDTitle.TabIndex = 4;
            this.lblDDTitle.Text = "Decimal Degrees";
            // 
            // lblDMSTitle
            // 
            this.lblDMSTitle.AutoSize = true;
            this.lblDMSTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDMSTitle.Location = new System.Drawing.Point(23, 101);
            this.lblDMSTitle.Name = "lblDMSTitle";
            this.lblDMSTitle.Size = new System.Drawing.Size(167, 16);
            this.lblDMSTitle.TabIndex = 5;
            this.lblDMSTitle.Text = "Degrees Minutes Seconds";
            // 
            // txtLatDD
            // 
            this.txtLatDD.Location = new System.Drawing.Point(78, 41);
            this.txtLatDD.Name = "txtLatDD";
            this.txtLatDD.Size = new System.Drawing.Size(194, 20);
            this.txtLatDD.TabIndex = 6;
            this.txtLatDD.Text = "Lat:";
            // 
            // txtLonDD
            // 
            this.txtLonDD.Location = new System.Drawing.Point(78, 67);
            this.txtLonDD.Name = "txtLonDD";
            this.txtLonDD.Size = new System.Drawing.Size(194, 20);
            this.txtLonDD.TabIndex = 7;
            this.txtLonDD.Text = "Lon:";
            // 
            // txtLonDMS
            // 
            this.txtLonDMS.Location = new System.Drawing.Point(78, 149);
            this.txtLonDMS.Name = "txtLonDMS";
            this.txtLonDMS.Size = new System.Drawing.Size(194, 20);
            this.txtLonDMS.TabIndex = 9;
            this.txtLonDMS.Text = "Lon:";
            // 
            // txtLatDMS
            // 
            this.txtLatDMS.Location = new System.Drawing.Point(78, 123);
            this.txtLatDMS.Name = "txtLatDMS";
            this.txtLatDMS.Size = new System.Drawing.Size(194, 20);
            this.txtLatDMS.TabIndex = 8;
            this.txtLatDMS.Text = "Lat:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Lat:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Lon:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Lon:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Lat:";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(197, 202);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // fmCoords
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button1;
            this.ClientSize = new System.Drawing.Size(296, 237);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtLonDMS);
            this.Controls.Add(this.txtLatDMS);
            this.Controls.Add(this.txtLonDD);
            this.Controls.Add(this.txtLatDD);
            this.Controls.Add(this.lblDMSTitle);
            this.Controls.Add(this.lblDDTitle);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmCoords";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Convert Map Units ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDDTitle;
        private System.Windows.Forms.Label lblDMSTitle;
        private System.Windows.Forms.TextBox txtLatDD;
        private System.Windows.Forms.TextBox txtLonDD;
        private System.Windows.Forms.TextBox txtLonDMS;
        private System.Windows.Forms.TextBox txtLatDMS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
    }
}