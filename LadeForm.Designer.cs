namespace LabelDruck
{
    partial class LadeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LadeForm));
            this.lbLoad = new System.Windows.Forms.Label();
            this.pgrbLoad = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // lbLoad
            // 
            this.lbLoad.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLoad.Location = new System.Drawing.Point(12, 9);
            this.lbLoad.Name = "lbLoad";
            this.lbLoad.Size = new System.Drawing.Size(372, 94);
            this.lbLoad.TabIndex = 0;
            this.lbLoad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pgrbLoad
            // 
            this.pgrbLoad.Location = new System.Drawing.Point(12, 106);
            this.pgrbLoad.Name = "pgrbLoad";
            this.pgrbLoad.Size = new System.Drawing.Size(372, 23);
            this.pgrbLoad.TabIndex = 1;
            // 
            // LadeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 141);
            this.Controls.Add(this.pgrbLoad);
            this.Controls.Add(this.lbLoad);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LadeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "L A D E N - Programmstart";
            this.Load += new System.EventHandler(this.LadeForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbLoad;
        private System.Windows.Forms.ProgressBar pgrbLoad;
    }
}