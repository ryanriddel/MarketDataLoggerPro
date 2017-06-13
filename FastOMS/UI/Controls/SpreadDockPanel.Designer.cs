namespace FastOMS.UI.Controls
{
    partial class SpreadDockPanel
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraBars.Docking2010.Views.Tabbed.DockingContainer dockingContainer1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.DockingContainer();
            DevExpress.XtraBars.Docking2010.Views.Tabbed.DockingContainer dockingContainer2 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.DockingContainer();
            this._leftDocumentGroup = new DevExpress.XtraBars.Docking2010.Views.Tabbed.DocumentGroup(this.components);
            this._rightDocumentGroup = new DevExpress.XtraBars.Docking2010.Views.Tabbed.DocumentGroup(this.components);
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.tabbedView1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(this.components);
            this.documentManager1 = new DevExpress.XtraBars.Docking2010.DocumentManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this._leftDocumentGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._rightDocumentGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // _leftDocumentGroup
            // 
            this._leftDocumentGroup.Properties.DestroyOnRemovingChildren = DevExpress.Utils.DefaultBoolean.False;
            this._leftDocumentGroup.Properties.HeaderButtons = DevExpress.XtraTab.TabButtons.None;
            this._leftDocumentGroup.Properties.HeaderButtonsShowMode = DevExpress.XtraTab.TabButtonShowMode.WhenNeeded;
            this._leftDocumentGroup.Properties.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Top;
            this._leftDocumentGroup.Properties.HeaderOrientation = DevExpress.XtraTab.TabOrientation.Horizontal;
            this._leftDocumentGroup.Properties.ShowDocumentSelectorButton = DevExpress.Utils.DefaultBoolean.True;
            this._leftDocumentGroup.Properties.ShowTabHeader = DevExpress.Utils.DefaultBoolean.True;
            this._leftDocumentGroup.Properties.TabWidth = 160;
            // 
            // _rightDocumentGroup
            // 
            this._rightDocumentGroup.Properties.DestroyOnRemovingChildren = DevExpress.Utils.DefaultBoolean.False;
            this._rightDocumentGroup.Properties.HeaderButtons = DevExpress.XtraTab.TabButtons.None;
            this._rightDocumentGroup.Properties.HeaderButtonsShowMode = DevExpress.XtraTab.TabButtonShowMode.WhenNeeded;
            this._rightDocumentGroup.Properties.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Top;
            this._rightDocumentGroup.Properties.HeaderOrientation = DevExpress.XtraTab.TabOrientation.Horizontal;
            this._rightDocumentGroup.Properties.ShowDocumentSelectorButton = DevExpress.Utils.DefaultBoolean.True;
            this._rightDocumentGroup.Properties.ShowTabHeader = DevExpress.Utils.DefaultBoolean.True;
            this._rightDocumentGroup.Properties.TabWidth = 160;
            // 
            // dockManager1
            // 
            this.dockManager1.AutoHiddenPanelShowMode = DevExpress.XtraBars.Docking.AutoHiddenPanelShowMode.MouseClick;
            this.dockManager1.DockingOptions.ShowCloseButton = false;
            this.dockManager1.DockingOptions.ShowMaximizeButton = false;
            this.dockManager1.DockMode = DevExpress.XtraBars.Docking.Helpers.DockMode.Standard;
            this.dockManager1.Form = this;
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
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
            // tabbedView1
            // 
            this.tabbedView1.AppearancePage.Header.Options.UseTextOptions = true;
            this.tabbedView1.AppearancePage.Header.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.tabbedView1.DocumentGroupProperties.DestroyOnRemovingChildren = false;
            this.tabbedView1.DocumentGroupProperties.HeaderButtons = DevExpress.XtraTab.TabButtons.None;
            this.tabbedView1.DocumentGroupProperties.HeaderButtonsShowMode = DevExpress.XtraTab.TabButtonShowMode.Never;
            this.tabbedView1.DocumentGroupProperties.MaxDocuments = 16;
            this.tabbedView1.DocumentGroupProperties.ShowDocumentSelectorButton = false;
            this.tabbedView1.DocumentGroups.AddRange(new DevExpress.XtraBars.Docking2010.Views.Tabbed.DocumentGroup[] {
            this._leftDocumentGroup,
            this._rightDocumentGroup});
            this.tabbedView1.DocumentProperties.AllowClose = false;
            this.tabbedView1.DocumentProperties.TabWidth = 130;
            this.tabbedView1.EnableFreeLayoutMode = DevExpress.Utils.DefaultBoolean.False;
            dockingContainer1.Element = this._leftDocumentGroup;
            dockingContainer2.Element = this._rightDocumentGroup;
            this.tabbedView1.RootContainer.Nodes.AddRange(new DevExpress.XtraBars.Docking2010.Views.Tabbed.DockingContainer[] {
            dockingContainer1,
            dockingContainer2});
            this.tabbedView1.UseDocumentSelector = DevExpress.Utils.DefaultBoolean.True;
            this.tabbedView1.UseLoadingIndicator = DevExpress.Utils.DefaultBoolean.True;
            this.tabbedView1.UseSnappingEmulation = DevExpress.Utils.DefaultBoolean.True;
            // 
            // documentManager1
            // 
            this.documentManager1.ContainerControl = this;
            this.documentManager1.View = this.tabbedView1;
            this.documentManager1.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] {
            this.tabbedView1});
            // 
            // SpreadDockPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "SpreadDockPanel";
            this.Size = new System.Drawing.Size(1075, 489);
            ((System.ComponentModel.ISupportInitialize)(this._leftDocumentGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._rightDocumentGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView tabbedView1;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.DocumentGroup _leftDocumentGroup;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.DocumentGroup _rightDocumentGroup;
        private DevExpress.XtraBars.Docking2010.DocumentManager documentManager1;
    }
}
