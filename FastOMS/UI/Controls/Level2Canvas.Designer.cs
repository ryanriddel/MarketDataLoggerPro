namespace FastOMS.UI
{
    partial class Level2Canvas
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
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.level2GridBid = new Level2Grid(Level2GridType.Bid);
            this.level2GridAsk = new Level2Grid(Level2GridType.Ask);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.SuspendLayout();

            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.splitContainerControl1.IsSplitterFixed = true;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.level2GridBid);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.level2GridAsk);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(700, 330);
            this.splitContainerControl1.SplitterPosition = 350;
            this.splitContainerControl1.TabIndex = 2;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // level2GridBid
            // 
            this.level2GridBid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.level2GridBid.Location = new System.Drawing.Point(0, 0);
            this.level2GridBid.Name = "level2GridBid";
            this.level2GridBid.Size = new System.Drawing.Size(350, 330);
            this.level2GridBid.TabIndex = 0;
            // 
            // level2GridAsk
            // 
            this.level2GridAsk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.level2GridAsk.Location = new System.Drawing.Point(0, 0);
            this.level2GridAsk.Name = "level2GridAsk";
            this.level2GridAsk.Size = new System.Drawing.Size(345, 330);
            this.level2GridAsk.TabIndex = 0;
            // 
            // Level2Canvas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "Level2Canvas";
            this.Size = new System.Drawing.Size(700, 330);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        
        #endregion
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;

        public Level2Grid level2GridBid;
        public Level2Grid level2GridAsk;
    }
}
