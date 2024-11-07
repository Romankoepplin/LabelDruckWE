namespace LabelDruck
{
    partial class BestandsListeKleber
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lbROOM = new System.Windows.Forms.Label();
            this.lbBestKl = new System.Windows.Forms.Label();
            this.cbBestKl = new System.Windows.Forms.ComboBox();
            this.rbtnTeilBez = new System.Windows.Forms.RadioButton();
            this.rBtnTeilNr = new System.Windows.Forms.RadioButton();
            this.btnFilterAus = new System.Windows.Forms.Button();
            this.btnBestKlPrint = new System.Windows.Forms.Button();
            this.btnAchtung = new System.Windows.Forms.Button();
            this.dtGvBestKl = new System.Windows.Forms.DataGridView();
            this.lbBestKlOK = new DevComponents.DotNetBar.LabelX();
            this.llBestKlVerf = new DevComponents.DotNetBar.LabelX();
            this.lbBestKlVerfUeb = new DevComponents.DotNetBar.LabelX();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGvBestKl)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.lbROOM);
            this.flowLayoutPanel1.Controls.Add(this.lbBestKl);
            this.flowLayoutPanel1.Controls.Add(this.cbBestKl);
            this.flowLayoutPanel1.Controls.Add(this.rbtnTeilBez);
            this.flowLayoutPanel1.Controls.Add(this.rBtnTeilNr);
            this.flowLayoutPanel1.Controls.Add(this.btnFilterAus);
            this.flowLayoutPanel1.Controls.Add(this.btnBestKlPrint);
            this.flowLayoutPanel1.Controls.Add(this.btnAchtung);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(920, 49);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // lbROOM
            // 
            this.lbROOM.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbROOM.AutoSize = true;
            this.lbROOM.Location = new System.Drawing.Point(4, 11);
            this.lbROOM.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbROOM.Name = "lbROOM";
            this.lbROOM.Size = new System.Drawing.Size(16, 17);
            this.lbROOM.TabIndex = 6;
            this.lbROOM.Text = "  ";
            // 
            // lbBestKl
            // 
            this.lbBestKl.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbBestKl.AutoSize = true;
            this.lbBestKl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBestKl.Location = new System.Drawing.Point(28, 10);
            this.lbBestKl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbBestKl.Name = "lbBestKl";
            this.lbBestKl.Size = new System.Drawing.Size(148, 18);
            this.lbBestKl.TabIndex = 0;
            this.lbBestKl.Text = "Anzeige Filtern nach :";
            // 
            // cbBestKl
            // 
            this.cbBestKl.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbBestKl.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbBestKl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBestKl.FormattingEnabled = true;
            this.cbBestKl.Location = new System.Drawing.Point(184, 6);
            this.cbBestKl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbBestKl.Name = "cbBestKl";
            this.cbBestKl.Size = new System.Drawing.Size(199, 26);
            this.cbBestKl.TabIndex = 1;
            this.cbBestKl.SelectedIndexChanged += new System.EventHandler(this.filterNach);
            this.cbBestKl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyPressEvent);
            // 
            // rbtnTeilBez
            // 
            this.rbtnTeilBez.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rbtnTeilBez.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnTeilBez.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnTeilBez.Location = new System.Drawing.Point(391, 4);
            this.rbtnTeilBez.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbtnTeilBez.Name = "rbtnTeilBez";
            this.rbtnTeilBez.Size = new System.Drawing.Size(160, 31);
            this.rbtnTeilBez.TabIndex = 7;
            this.rbtnTeilBez.TabStop = true;
            this.rbtnTeilBez.Text = "Teilebezeichnung";
            this.rbtnTeilBez.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbtnTeilBez.UseVisualStyleBackColor = true;
            this.rbtnTeilBez.Click += new System.EventHandler(this.filterNach);
            // 
            // rBtnTeilNr
            // 
            this.rBtnTeilNr.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rBtnTeilNr.Appearance = System.Windows.Forms.Appearance.Button;
            this.rBtnTeilNr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rBtnTeilNr.Location = new System.Drawing.Point(559, 4);
            this.rBtnTeilNr.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rBtnTeilNr.Name = "rBtnTeilNr";
            this.rBtnTeilNr.Size = new System.Drawing.Size(160, 31);
            this.rBtnTeilNr.TabIndex = 8;
            this.rBtnTeilNr.TabStop = true;
            this.rBtnTeilNr.Text = "Teilenummer";
            this.rBtnTeilNr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rBtnTeilNr.UseVisualStyleBackColor = true;
            this.rBtnTeilNr.Click += new System.EventHandler(this.filterNach);
            // 
            // btnFilterAus
            // 
            this.btnFilterAus.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnFilterAus.Image = global::LabelDruck.Properties.Resources.icon_packet_filter_disabled;
            this.btnFilterAus.Location = new System.Drawing.Point(727, 5);
            this.btnFilterAus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFilterAus.Name = "btnFilterAus";
            this.btnFilterAus.Size = new System.Drawing.Size(36, 28);
            this.btnFilterAus.TabIndex = 4;
            this.btnFilterAus.UseVisualStyleBackColor = true;
            this.btnFilterAus.Click += new System.EventHandler(this.filterAus);
            // 
            // btnBestKlPrint
            // 
            this.btnBestKlPrint.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnBestKlPrint.Image = global::LabelDruck.Properties.Resources.print_icon;
            this.btnBestKlPrint.Location = new System.Drawing.Point(771, 5);
            this.btnBestKlPrint.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBestKlPrint.Name = "btnBestKlPrint";
            this.btnBestKlPrint.Size = new System.Drawing.Size(80, 28);
            this.btnBestKlPrint.TabIndex = 5;
            this.btnBestKlPrint.UseVisualStyleBackColor = true;
            this.btnBestKlPrint.Click += new System.EventHandler(this.printToExcel);
            // 
            // btnAchtung
            // 
            this.btnAchtung.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnAchtung.Image = global::LabelDruck.Properties.Resources.exclamation1;
            this.btnAchtung.Location = new System.Drawing.Point(856, 5);
            this.btnAchtung.Margin = new System.Windows.Forms.Padding(1);
            this.btnAchtung.Name = "btnAchtung";
            this.btnAchtung.Size = new System.Drawing.Size(40, 28);
            this.btnAchtung.TabIndex = 9;
            this.btnAchtung.UseVisualStyleBackColor = true;
            this.btnAchtung.Visible = false;
            this.btnAchtung.Click += new System.EventHandler(this.alteKleber);
            // 
            // dtGvBestKl
            // 
            this.dtGvBestKl.AllowUserToAddRows = false;
            this.dtGvBestKl.AllowUserToDeleteRows = false;
            this.dtGvBestKl.AllowUserToResizeRows = false;
            this.dtGvBestKl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtGvBestKl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGvBestKl.Location = new System.Drawing.Point(0, 57);
            this.dtGvBestKl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtGvBestKl.Name = "dtGvBestKl";
            this.dtGvBestKl.ReadOnly = true;
            this.dtGvBestKl.Size = new System.Drawing.Size(920, 423);
            this.dtGvBestKl.TabIndex = 1;
            this.dtGvBestKl.Paint += new System.Windows.Forms.PaintEventHandler(this.colourChange);
            // 
            // lbBestKlOK
            // 
            this.lbBestKlOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbBestKlOK.BackColor = System.Drawing.Color.LimeGreen;
            // 
            // 
            // 
            this.lbBestKlOK.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbBestKlOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBestKlOK.Location = new System.Drawing.Point(28, 545);
            this.lbBestKlOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lbBestKlOK.Name = "lbBestKlOK";
            this.lbBestKlOK.Size = new System.Drawing.Size(293, 31);
            this.lbBestKlOK.TabIndex = 4;
            this.lbBestKlOK.Text = "OK";
            this.lbBestKlOK.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // llBestKlVerf
            // 
            this.llBestKlVerf.BackColor = System.Drawing.Color.Yellow;
            // 
            // 
            // 
            this.llBestKlVerf.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.llBestKlVerf.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llBestKlVerf.Location = new System.Drawing.Point(312, 545);
            this.llBestKlVerf.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.llBestKlVerf.Name = "llBestKlVerf";
            this.llBestKlVerf.Size = new System.Drawing.Size(293, 31);
            this.llBestKlVerf.TabIndex = 5;
            this.llBestKlVerf.Text = "Verfallsdatum innerhalb eines Monats";
            this.llBestKlVerf.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // lbBestKlVerfUeb
            // 
            this.lbBestKlVerfUeb.BackColor = System.Drawing.Color.Red;
            // 
            // 
            // 
            this.lbBestKlVerfUeb.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbBestKlVerfUeb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBestKlVerfUeb.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbBestKlVerfUeb.Location = new System.Drawing.Point(604, 545);
            this.lbBestKlVerfUeb.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lbBestKlVerfUeb.Name = "lbBestKlVerfUeb";
            this.lbBestKlVerfUeb.Size = new System.Drawing.Size(293, 31);
            this.lbBestKlVerfUeb.TabIndex = 6;
            this.lbBestKlVerfUeb.Text = "Verfallsdatum überschritten";
            this.lbBestKlVerfUeb.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // BestandsListeKleber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(920, 576);
            this.Controls.Add(this.lbBestKlVerfUeb);
            this.Controls.Add(this.llBestKlVerf);
            this.Controls.Add(this.lbBestKlOK);
            this.Controls.Add(this.dtGvBestKl);
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "BestandsListeKleber";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BestandsListeKleber";
            this.Load += new System.EventHandler(this.BestandsListeKleber_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGvBestKl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label lbROOM;
        private System.Windows.Forms.Label lbBestKl;
        private System.Windows.Forms.ComboBox cbBestKl;
        private System.Windows.Forms.Button btnFilterAus;
        private System.Windows.Forms.Button btnBestKlPrint;
        private System.Windows.Forms.DataGridView dtGvBestKl;
        private DevComponents.DotNetBar.LabelX lbBestKlOK;
        private DevComponents.DotNetBar.LabelX lbBestKlVerfUeb;
        private DevComponents.DotNetBar.LabelX llBestKlVerf;
        private System.Windows.Forms.RadioButton rbtnTeilBez;
        private System.Windows.Forms.RadioButton rBtnTeilNr;
        private System.Windows.Forms.Button btnAchtung;

    }
}