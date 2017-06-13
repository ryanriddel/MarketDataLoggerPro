namespace FastOMS.UI
{
    partial class SpreadForm
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
            this.spreadLevel2PanelControl = new FastOMS.UI.Level2PanelControl();
            this.barManager = new DevExpress.XtraBars.BarManager();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this._dockManager = new DevExpress.XtraBars.Docking.DockManager();
            this.spreadLevel2Container = new DevExpress.XtraEditors.PanelControl();
            this.spreadDockPanel1 = new FastOMS.UI.Controls.SpreadDockPanel();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dockManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spreadLevel2Container)).BeginInit();
            this.spreadLevel2Container.SuspendLayout();
            this.SuspendLayout();
            // 
            // spreadLevel2PanelControl
            // 
            this.spreadLevel2PanelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spreadLevel2PanelControl.Location = new System.Drawing.Point(2, 2);
            this.spreadLevel2PanelControl.Name = "spreadLevel2PanelControl";
            this.spreadLevel2PanelControl.Size = new System.Drawing.Size(512, 440);
            this.spreadLevel2PanelControl.TabIndex = 0;
            // 
            // barManager
            // 
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.MaxItemId = 0;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1546, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 443);
            this.barDockControlBottom.Size = new System.Drawing.Size(1546, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 443);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1546, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 443);
            // 
            // _dockManager
            // 
            this._dockManager.Form = this;
            this._dockManager.MenuManager = this.barManager;
            this._dockManager.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane"});
            // 
            // spreadLevel2Container
            // 
            this.spreadLevel2Container.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spreadLevel2Container.Controls.Add(this.spreadLevel2PanelControl);
            this.spreadLevel2Container.Location = new System.Drawing.Point(0, 0);
            this.spreadLevel2Container.Name = "spreadLevel2Container";
            this.spreadLevel2Container.Size = new System.Drawing.Size(516, 444);
            this.spreadLevel2Container.TabIndex = 16;
            // 
            // spreadDockPanel1
            // 
            this.spreadDockPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spreadDockPanel1.Location = new System.Drawing.Point(520, 2);
            this.spreadDockPanel1.Name = "spreadDockPanel1";
            this.spreadDockPanel1.Size = new System.Drawing.Size(1023, 440);
            this.spreadDockPanel1.TabIndex = 26;
            // 
            // SpreadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1546, 443);
            this.Controls.Add(this.spreadDockPanel1);
            this.Controls.Add(this.spreadLevel2Container);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.LookAndFeel.SkinName = "DevExpress Dark Style";
            this.Name = "SpreadForm";
            this.Text = "SpreadForm";
            this.Load += new System.EventHandler(this.SpreadForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dockManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spreadLevel2Container)).EndInit();
            this.spreadLevel2Container.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.Docking.DockManager _dockManager;
        private Level2PanelControl spreadLevel2PanelControl;
        private DevExpress.XtraEditors.PanelControl spreadLevel2Container;
        private Controls.SpreadDockPanel spreadDockPanel1;
    }
}