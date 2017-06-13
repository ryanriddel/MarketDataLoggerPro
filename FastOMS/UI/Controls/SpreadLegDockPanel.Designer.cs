namespace FastOMS.UI.Controls
{
    partial class SpreadLegDockPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this._leftSpreadDock = new FastOMS.UI.Controls.SpreadDock();
            this._rightSpreadDock = new FastOMS.UI.Controls.SpreadDock();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.splitContainerControl1.IsSplitterFixed = true;
            this.splitContainerControl1.Location = new System.Drawing.Point(2, 20);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this._leftSpreadDock);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this._rightSpreadDock);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1046, 491);
            this.splitContainerControl1.SplitterPosition = 522;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl1.CaptionLocation = DevExpress.Utils.Locations.Top;
            this.groupControl1.Controls.Add(this.splitContainerControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1050, 513);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Spread Information";
            // 
            // _leftSpreadDock
            // 
            this._leftSpreadDock.Appearance.BackColor = System.Drawing.Color.LightBlue;
            this._leftSpreadDock.Appearance.Options.UseBackColor = true;
            this._leftSpreadDock.Dock = System.Windows.Forms.DockStyle.Fill;
            this._leftSpreadDock.Location = new System.Drawing.Point(0, 0);
            this._leftSpreadDock.Name = "_leftSpreadDock";
            this._leftSpreadDock.Size = new System.Drawing.Size(522, 491);
            this._leftSpreadDock.TabIndex = 0;
            // 
            // _rightSpreadDock
            // 
            this._rightSpreadDock.Appearance.BackColor = System.Drawing.Color.LightBlue;
            this._rightSpreadDock.Appearance.Options.UseBackColor = true;
            this._rightSpreadDock.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rightSpreadDock.Location = new System.Drawing.Point(0, 0);
            this._rightSpreadDock.Name = "_rightSpreadDock";
            this._rightSpreadDock.Size = new System.Drawing.Size(519, 491);
            this._rightSpreadDock.TabIndex = 0;
            // 
            // SpreadLegDockPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Name = "SpreadLegDockPanel";
            this.Size = new System.Drawing.Size(1050, 513);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private SpreadDock _leftSpreadDock;
        private SpreadDock _rightSpreadDock;
        private DevExpress.XtraEditors.GroupControl groupControl1;
    }
}
