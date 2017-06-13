namespace FastOMS.UI
{
    partial class WatchListForm
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
            this.buttonAddSymbol = new DevExpress.XtraEditors.SimpleButton();
            this.watchListGrid1 = new FastOMS.UI.WatchListGrid();
            this.SuspendLayout();
            // 
            // buttonAddSymbol
            // 
            this.buttonAddSymbol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddSymbol.Location = new System.Drawing.Point(445, 261);
            this.buttonAddSymbol.Name = "buttonAddSymbol";
            this.buttonAddSymbol.Size = new System.Drawing.Size(75, 23);
            this.buttonAddSymbol.TabIndex = 1;
            this.buttonAddSymbol.Text = "Add Symbol";
            this.buttonAddSymbol.Click += new System.EventHandler(this.buttonAddSymbol_Click);
            // 
            // watchListGrid1
            // 
            this.watchListGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.watchListGrid1.Location = new System.Drawing.Point(0, 0);
            this.watchListGrid1.Name = "watchListGrid1";
            this.watchListGrid1.Size = new System.Drawing.Size(532, 255);
            this.watchListGrid1.TabIndex = 0;
            // 
            // WatchListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 296);
            this.Controls.Add(this.buttonAddSymbol);
            this.Controls.Add(this.watchListGrid1);
            this.Name = "WatchListForm";
            this.Text = "WatchListForm";
            this.ResumeLayout(false);

        }

        #endregion

        private WatchListGrid watchListGrid1;
        private DevExpress.XtraEditors.SimpleButton buttonAddSymbol;
    }
}