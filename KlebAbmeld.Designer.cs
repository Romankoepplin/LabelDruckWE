namespace LabelDruck
{
    partial class KlebAbmeld
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
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanelKlebAb = new DevComponents.DotNetBar.TabControlPanel();
            this.btnKlebAbm = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbKlebAbm = new System.Windows.Forms.Label();
            this.tbKlebAbm = new System.Windows.Forms.TextBox();
            this.tabItem1 = new DevComponents.DotNetBar.TabItem(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabControlPanelKlebAb.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.CanReorderTabs = true;
            this.tabControl1.Controls.Add(this.tabControlPanelKlebAb);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedTabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.tabControl1.SelectedTabIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(619, 219);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabControl1.Tabs.Add(this.tabItem1);
            this.tabControl1.Text = "tabControl1";
            // 
            // tabControlPanelKlebAb
            // 
            this.tabControlPanelKlebAb.Controls.Add(this.btnKlebAbm);
            this.tabControlPanelKlebAb.Controls.Add(this.tableLayoutPanel1);
            this.tabControlPanelKlebAb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanelKlebAb.Location = new System.Drawing.Point(0, 26);
            this.tabControlPanelKlebAb.Name = "tabControlPanelKlebAb";
            this.tabControlPanelKlebAb.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanelKlebAb.Size = new System.Drawing.Size(619, 193);
            this.tabControlPanelKlebAb.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.tabControlPanelKlebAb.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.tabControlPanelKlebAb.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanelKlebAb.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.tabControlPanelKlebAb.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanelKlebAb.Style.GradientAngle = 90;
            this.tabControlPanelKlebAb.TabIndex = 1;
            this.tabControlPanelKlebAb.TabItem = this.tabItem1;
            // 
            // btnKlebAbm
            // 
            this.btnKlebAbm.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnKlebAbm.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKlebAbm.Location = new System.Drawing.Point(213, 133);
            this.btnKlebAbm.Name = "btnKlebAbm";
            this.btnKlebAbm.Size = new System.Drawing.Size(200, 40);
            this.btnKlebAbm.TabIndex = 1;
            this.btnKlebAbm.Text = "Abmelden";
            this.btnKlebAbm.UseVisualStyleBackColor = true;
            this.btnKlebAbm.Click += new System.EventHandler(this.kleberAbmelden);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lbKlebAbm, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbKlebAbm, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(617, 100);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lbKlebAbm
            // 
            this.lbKlebAbm.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lbKlebAbm.AutoSize = true;
            this.lbKlebAbm.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbKlebAbm.Location = new System.Drawing.Point(229, 32);
            this.lbKlebAbm.Name = "lbKlebAbm";
            this.lbKlebAbm.Size = new System.Drawing.Size(159, 18);
            this.lbKlebAbm.TabIndex = 0;
            this.lbKlebAbm.Text = "Kleber ID eingeben :";
            // 
            // tbKlebAbm
            // 
            this.tbKlebAbm.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tbKlebAbm.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbKlebAbm.Location = new System.Drawing.Point(233, 53);
            this.tbKlebAbm.Name = "tbKlebAbm";
            this.tbKlebAbm.Size = new System.Drawing.Size(150, 26);
            this.tbKlebAbm.TabIndex = 1;
            this.tbKlebAbm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tabItem1
            // 
            this.tabItem1.AttachedControl = this.tabControlPanelKlebAb;
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.Text = "Klebstoff abmelden";
            // 
            // KlebAbmeld
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(619, 219);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "KlebAbmeld";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Klebstoff abmelden";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabControlPanelKlebAb.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.TabControl tabControl1;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanelKlebAb;
        private DevComponents.DotNetBar.TabItem tabItem1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbKlebAbm;
        private System.Windows.Forms.Button btnKlebAbm;
        private System.Windows.Forms.TextBox tbKlebAbm;
    }
}