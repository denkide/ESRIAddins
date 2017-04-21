namespace Medford_GISAddin.TaxlotSearch
{
    partial class fmTLSearch
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
            this.gpbSearch = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSitus = new System.Windows.Forms.TextBox();
            this.txtMaplot = new System.Windows.Forms.TextBox();
            this.txtAccount = new System.Windows.Forms.TextBox();
            this.txtMapnum = new System.Windows.Forms.TextBox();
            this.txtOwner = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gpbSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpbSearch
            // 
            this.gpbSearch.Controls.Add(this.label9);
            this.gpbSearch.Controls.Add(this.label8);
            this.gpbSearch.Controls.Add(this.txtSitus);
            this.gpbSearch.Controls.Add(this.txtMaplot);
            this.gpbSearch.Controls.Add(this.txtAccount);
            this.gpbSearch.Controls.Add(this.txtMapnum);
            this.gpbSearch.Controls.Add(this.txtOwner);
            this.gpbSearch.Controls.Add(this.label7);
            this.gpbSearch.Controls.Add(this.label6);
            this.gpbSearch.Controls.Add(this.label5);
            this.gpbSearch.Controls.Add(this.label4);
            this.gpbSearch.Controls.Add(this.label3);
            this.gpbSearch.Controls.Add(this.label2);
            this.gpbSearch.Controls.Add(this.label1);
            this.gpbSearch.Location = new System.Drawing.Point(12, 21);
            this.gpbSearch.Name = "gpbSearch";
            this.gpbSearch.Size = new System.Drawing.Size(405, 331);
            this.gpbSearch.TabIndex = 0;
            this.gpbSearch.TabStop = false;
            this.gpbSearch.Text = "Select one search field";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(142, 233);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "(e.g. 10373164)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(142, 174);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "(e.g. 372W25DA8600)";
            // 
            // txtSitus
            // 
            this.txtSitus.Enabled = false;
            this.txtSitus.Location = new System.Drawing.Point(145, 99);
            this.txtSitus.Name = "txtSitus";
            this.txtSitus.Size = new System.Drawing.Size(216, 20);
            this.txtSitus.TabIndex = 2;
            this.txtSitus.Tag = "1";
            this.txtSitus.Click += new System.EventHandler(this.setSearch);
            this.txtSitus.Enter += new System.EventHandler(this.txtSitus_Enter);
            this.txtSitus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.handleKeyDown);
            // 
            // txtMaplot
            // 
            this.txtMaplot.Location = new System.Drawing.Point(145, 144);
            this.txtMaplot.Name = "txtMaplot";
            this.txtMaplot.Size = new System.Drawing.Size(216, 20);
            this.txtMaplot.TabIndex = 3;
            this.txtMaplot.Tag = "2";
            this.txtMaplot.Click += new System.EventHandler(this.setSearch);
            this.txtMaplot.Enter += new System.EventHandler(this.txtMaplot_Enter);
            this.txtMaplot.KeyDown += new System.Windows.Forms.KeyEventHandler(this.handleKeyDown);
            // 
            // txtAccount
            // 
            this.txtAccount.Location = new System.Drawing.Point(145, 204);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.Size = new System.Drawing.Size(216, 20);
            this.txtAccount.TabIndex = 4;
            this.txtAccount.Tag = "3";
            this.txtAccount.Click += new System.EventHandler(this.setSearch);
            this.txtAccount.Enter += new System.EventHandler(this.txtAccount_Enter);
            this.txtAccount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.handleKeyDown);
            // 
            // txtMapnum
            // 
            this.txtMapnum.Location = new System.Drawing.Point(145, 266);
            this.txtMapnum.Name = "txtMapnum";
            this.txtMapnum.Size = new System.Drawing.Size(216, 20);
            this.txtMapnum.TabIndex = 5;
            this.txtMapnum.Tag = "4";
            this.txtMapnum.Click += new System.EventHandler(this.setSearch);
            this.txtMapnum.Enter += new System.EventHandler(this.txtMapnum_Enter);
            this.txtMapnum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.handleKeyDown);
            // 
            // txtOwner
            // 
            this.txtOwner.Location = new System.Drawing.Point(145, 41);
            this.txtOwner.Name = "txtOwner";
            this.txtOwner.Size = new System.Drawing.Size(216, 20);
            this.txtOwner.TabIndex = 1;
            this.txtOwner.Tag = "0";
            this.txtOwner.Click += new System.EventHandler(this.setSearch);
            this.txtOwner.Enter += new System.EventHandler(this.txtOwner_Enter);
            this.txtOwner.KeyDown += new System.Windows.Forms.KeyEventHandler(this.handleKeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(142, 299);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "(e.g. 372W25)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(21, 271);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 15);
            this.label6.TabIndex = 5;
            this.label6.Text = "Map Number:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 204);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Account Number:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(21, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Maplot Number:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(21, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Situs Address:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(142, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "(e.g. MEDFORD CITY OF )";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Owner Name:";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(119, 376);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(224, 376);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // fmTLSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 426);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.gpbSearch);
            this.Name = "fmTLSearch";
            this.Text = "Search Taxlots";
            this.gpbSearch.ResumeLayout(false);
            this.gpbSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpbSearch;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOwner;
        private System.Windows.Forms.TextBox txtSitus;
        private System.Windows.Forms.TextBox txtMaplot;
        private System.Windows.Forms.TextBox txtAccount;
        private System.Windows.Forms.TextBox txtMapnum;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
    }
}