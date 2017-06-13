namespace FastOMS.UI
{
    partial class Level2PanelControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Level2PanelControl));
            this.timeAndSalesControl = new FastOMS.UI.TimeAndSalesControl();
            this.orderEntryControl = new FastOMS.UI.OrderEntryControl();
            this.level2Canvas = new FastOMS.UI.Level2Canvas();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.level2PanelControlStatusBar = new DevExpress.XtraBars.Bar();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.toggleOrderEntryButton = new DevExpress.XtraBars.BarButtonItem();
            this.toggleTASButton = new DevExpress.XtraBars.BarButtonItem();
            this.windowOptionsButton = new DevExpress.XtraBars.BarButtonItem();
            this.saveLayoutButton = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barStaticSynName = new DevExpress.XtraBars.BarStaticItem();
            this.level2PanelControlHeader = new DevExpress.XtraEditors.GroupControl();
            this.settingsFlyoutPanel = new DevExpress.Utils.FlyoutPanel();
            this.checkEditOrderFlyout = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditTimeAndSaleColHeaders = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditLevel2ColHeaders = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditCopyQty = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditCopyPrice = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditCopyDest = new DevExpress.XtraEditors.CheckEdit();
            this.orderConfirmationFlyoutPanel = new DevExpress.Utils.FlyoutPanel();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.level2PanelControlHeader)).BeginInit();
            this.level2PanelControlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.settingsFlyoutPanel)).BeginInit();
            this.settingsFlyoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditOrderFlyout.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditTimeAndSaleColHeaders.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditLevel2ColHeaders.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCopyQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCopyPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCopyDest.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderConfirmationFlyoutPanel)).BeginInit();
            this.orderConfirmationFlyoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // timeAndSalesControl
            // 
            this.timeAndSalesControl.ColumnHeadersVisible = true;
            this.timeAndSalesControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeAndSalesControl.Location = new System.Drawing.Point(0, 0);
            this.timeAndSalesControl.Name = "timeAndSalesControl";
            this.timeAndSalesControl.Size = new System.Drawing.Size(215, 406);
            this.timeAndSalesControl.TabIndex = 2;
            // 
            // orderEntryControl
            // 
            this.orderEntryControl.Dest = "";
            this.orderEntryControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderEntryControl.Location = new System.Drawing.Point(0, 0);
            this.orderEntryControl.Name = "orderEntryControl";
            this.orderEntryControl.Price = 0F;
            this.orderEntryControl.Qty = ((uint)(0u));
            this.orderEntryControl.Size = new System.Drawing.Size(291, 106);
            this.orderEntryControl.TabIndex = 1;
            // 
            // level2Canvas
            // 
            this.level2Canvas._inst = null;
            this.level2Canvas.ColumnHeadersVisible = true;
            this.level2Canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.level2Canvas.Location = new System.Drawing.Point(0, 0);
            this.level2Canvas.Name = "level2Canvas";
            this.level2Canvas.Size = new System.Drawing.Size(291, 295);
            this.level2Canvas.TabIndex = 0;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2;
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl1.Location = new System.Drawing.Point(2, 20);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.splitContainerControl2);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.timeAndSalesControl);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(511, 406);
            this.splitContainerControl1.SplitterPosition = 215;
            this.splitContainerControl1.TabIndex = 4;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2;
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl2.Horizontal = false;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.level2Canvas);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Controls.Add(this.orderEntryControl);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(291, 406);
            this.splitContainerControl2.SplitterPosition = 106;
            this.splitContainerControl2.TabIndex = 0;
            this.splitContainerControl2.Text = "splitContainerControl2";
            // 
            // bar1
            // 
            this.bar1.DockCol = 0;
            // 
            // barManager1
            // 
            this.barManager1.AllowShowToolbarsPopup = false;
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.level2PanelControlStatusBar});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem1,
            this.barButtonItem2,
            this.toggleOrderEntryButton,
            this.toggleTASButton,
            this.windowOptionsButton,
            this.saveLayoutButton});
            this.barManager1.MaxItemId = 6;
            this.barManager1.StatusBar = this.level2PanelControlStatusBar;
            // 
            // level2PanelControlStatusBar
            // 
            this.level2PanelControlStatusBar.BarName = "level2PanelControlStatusBar";
            this.level2PanelControlStatusBar.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.level2PanelControlStatusBar.DockCol = 0;
            this.level2PanelControlStatusBar.DockRow = 0;
            this.level2PanelControlStatusBar.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.level2PanelControlStatusBar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.toggleOrderEntryButton),
            new DevExpress.XtraBars.LinkPersistInfo(this.toggleTASButton),
            new DevExpress.XtraBars.LinkPersistInfo(this.windowOptionsButton),
            new DevExpress.XtraBars.LinkPersistInfo(this.saveLayoutButton)});
            this.level2PanelControlStatusBar.OptionsBar.AllowQuickCustomization = false;
            this.level2PanelControlStatusBar.OptionsBar.DrawDragBorder = false;
            this.level2PanelControlStatusBar.OptionsBar.UseWholeRow = true;
            this.level2PanelControlStatusBar.Text = "level2PanelControlStatusBar";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "IsConnected";
            this.barButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.Glyph")));
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.LargeGlyph = global::FastOMS.Properties.Resources.apply_16x16;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // toggleOrderEntryButton
            // 
            this.toggleOrderEntryButton.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.toggleOrderEntryButton.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.toggleOrderEntryButton.Caption = "Toggle Order Entry";
            this.toggleOrderEntryButton.Glyph = global::FastOMS.Properties.Resources.snaptocells_16x16;
            this.toggleOrderEntryButton.Id = 2;
            this.toggleOrderEntryButton.LargeGlyph = global::FastOMS.Properties.Resources.snaptocells_32x32;
            this.toggleOrderEntryButton.Name = "toggleOrderEntryButton";
            this.toggleOrderEntryButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.toggleOrderEntryButton_ItemClick);
            // 
            // toggleTASButton
            // 
            this.toggleTASButton.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.toggleTASButton.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.toggleTASButton.Caption = "Toggle Time and Sale";
            this.toggleTASButton.Glyph = ((System.Drawing.Image)(resources.GetObject("toggleTASButton.Glyph")));
            this.toggleTASButton.Id = 3;
            this.toggleTASButton.LargeGlyph = global::FastOMS.Properties.Resources.alignverticalright_16x16;
            this.toggleTASButton.Name = "toggleTASButton";
            this.toggleTASButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.toggleTASButton_ItemClick);
            // 
            // windowOptionsButton
            // 
            this.windowOptionsButton.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.windowOptionsButton.Caption = "Options";
            this.windowOptionsButton.Glyph = ((System.Drawing.Image)(resources.GetObject("windowOptionsButton.Glyph")));
            this.windowOptionsButton.Id = 4;
            this.windowOptionsButton.LargeGlyph = global::FastOMS.Properties.Resources.properties_16x16;
            this.windowOptionsButton.Name = "windowOptionsButton";
            this.windowOptionsButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.windowOptionsButton_ItemClick);
            // 
            // saveLayoutButton
            // 
            this.saveLayoutButton.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.saveLayoutButton.Caption = "Save Default Layout";
            this.saveLayoutButton.Glyph = ((System.Drawing.Image)(resources.GetObject("saveLayoutButton.Glyph")));
            this.saveLayoutButton.Id = 5;
            this.saveLayoutButton.LargeGlyph = global::FastOMS.Properties.Resources.exportfile_16x16;
            this.saveLayoutButton.Name = "saveLayoutButton";
            this.saveLayoutButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.saveLayoutButton_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(515, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 428);
            this.barDockControlBottom.Size = new System.Drawing.Size(515, 27);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 428);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(515, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 428);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "someNetworkMetric";
            this.barButtonItem2.Id = 1;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // bar2
            // 
            this.bar2.DockCol = 0;
            // 
            // bar3
            // 
            this.bar3.DockCol = 0;
            // 
            // barStaticSynName
            // 
            this.barStaticSynName.Id = -1;
            this.barStaticSynName.Name = "barStaticSynName";
            this.barStaticSynName.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // level2PanelControlHeader
            // 
            this.level2PanelControlHeader.AppearanceCaption.Options.UseTextOptions = true;
            this.level2PanelControlHeader.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.level2PanelControlHeader.AppearanceCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.level2PanelControlHeader.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.level2PanelControlHeader.Controls.Add(this.settingsFlyoutPanel);
            this.level2PanelControlHeader.Controls.Add(this.orderConfirmationFlyoutPanel);
            this.level2PanelControlHeader.Controls.Add(this.splitContainerControl1);
            this.level2PanelControlHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.level2PanelControlHeader.Location = new System.Drawing.Point(0, 0);
            this.level2PanelControlHeader.Name = "level2PanelControlHeader";
            this.level2PanelControlHeader.Size = new System.Drawing.Size(515, 428);
            this.level2PanelControlHeader.TabIndex = 1;
            this.level2PanelControlHeader.Text = "Smart Price = $XXXX         Book Imbalance = +150%      Average Midpoint = $XXXX";
            // 
            // settingsFlyoutPanel
            // 
            this.settingsFlyoutPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.settingsFlyoutPanel.Controls.Add(this.checkEditOrderFlyout);
            this.settingsFlyoutPanel.Controls.Add(this.checkEditTimeAndSaleColHeaders);
            this.settingsFlyoutPanel.Controls.Add(this.checkEditLevel2ColHeaders);
            this.settingsFlyoutPanel.Controls.Add(this.checkEditCopyQty);
            this.settingsFlyoutPanel.Controls.Add(this.checkEditCopyPrice);
            this.settingsFlyoutPanel.Controls.Add(this.checkEditCopyDest);
            this.settingsFlyoutPanel.Location = new System.Drawing.Point(0, 0);
            this.settingsFlyoutPanel.Name = "settingsFlyoutPanel";
            this.settingsFlyoutPanel.Options.CloseOnOuterClick = true;
            this.settingsFlyoutPanel.OwnerControl = this;
            this.settingsFlyoutPanel.Size = new System.Drawing.Size(515, 81);
            this.settingsFlyoutPanel.TabIndex = 1;
            // 
            // checkEditOrderFlyout
            // 
            this.checkEditOrderFlyout.Location = new System.Drawing.Point(85, 3);
            this.checkEditOrderFlyout.MenuManager = this.barManager1;
            this.checkEditOrderFlyout.Name = "checkEditOrderFlyout";
            this.checkEditOrderFlyout.Properties.Caption = "Show Order Flyout";
            this.checkEditOrderFlyout.Size = new System.Drawing.Size(116, 19);
            this.checkEditOrderFlyout.TabIndex = 5;
            this.checkEditOrderFlyout.CheckedChanged += new System.EventHandler(this.checkEditOrderFlyout_CheckedChanged);
            // 
            // checkEditTimeAndSaleColHeaders
            // 
            this.checkEditTimeAndSaleColHeaders.Location = new System.Drawing.Point(85, 53);
            this.checkEditTimeAndSaleColHeaders.MenuManager = this.barManager1;
            this.checkEditTimeAndSaleColHeaders.Name = "checkEditTimeAndSaleColHeaders";
            this.checkEditTimeAndSaleColHeaders.Properties.Caption = "Time and Sale Col Headers";
            this.checkEditTimeAndSaleColHeaders.Size = new System.Drawing.Size(152, 19);
            this.checkEditTimeAndSaleColHeaders.TabIndex = 4;
            this.checkEditTimeAndSaleColHeaders.CheckedChanged += new System.EventHandler(this.checkEditTimeAndSaleColHeaders_CheckedChanged);
            // 
            // checkEditLevel2ColHeaders
            // 
            this.checkEditLevel2ColHeaders.Location = new System.Drawing.Point(85, 28);
            this.checkEditLevel2ColHeaders.MenuManager = this.barManager1;
            this.checkEditLevel2ColHeaders.Name = "checkEditLevel2ColHeaders";
            this.checkEditLevel2ColHeaders.Properties.Caption = "Level2 Col Headers";
            this.checkEditLevel2ColHeaders.Size = new System.Drawing.Size(116, 19);
            this.checkEditLevel2ColHeaders.TabIndex = 3;
            this.checkEditLevel2ColHeaders.CheckedChanged += new System.EventHandler(this.checkEdiLevel2ColHeaders_CheckedChanged);
            // 
            // checkEditCopyQty
            // 
            this.checkEditCopyQty.Location = new System.Drawing.Point(4, 53);
            this.checkEditCopyQty.MenuManager = this.barManager1;
            this.checkEditCopyQty.Name = "checkEditCopyQty";
            this.checkEditCopyQty.Properties.Caption = "Copy Qty";
            this.checkEditCopyQty.Size = new System.Drawing.Size(75, 19);
            this.checkEditCopyQty.TabIndex = 2;
            this.checkEditCopyQty.CheckedChanged += new System.EventHandler(this.checkEditCopyQty_CheckedChanged);
            // 
            // checkEditCopyPrice
            // 
            this.checkEditCopyPrice.Location = new System.Drawing.Point(4, 28);
            this.checkEditCopyPrice.MenuManager = this.barManager1;
            this.checkEditCopyPrice.Name = "checkEditCopyPrice";
            this.checkEditCopyPrice.Properties.Caption = "Copy Price";
            this.checkEditCopyPrice.Size = new System.Drawing.Size(75, 19);
            this.checkEditCopyPrice.TabIndex = 1;
            this.checkEditCopyPrice.CheckedChanged += new System.EventHandler(this.checkEditCopyPrice_CheckedChanged);
            // 
            // checkEditCopyDest
            // 
            this.checkEditCopyDest.Location = new System.Drawing.Point(4, 3);
            this.checkEditCopyDest.MenuManager = this.barManager1;
            this.checkEditCopyDest.Name = "checkEditCopyDest";
            this.checkEditCopyDest.Properties.Caption = "Copy Dest";
            this.checkEditCopyDest.Size = new System.Drawing.Size(75, 19);
            this.checkEditCopyDest.TabIndex = 0;
            this.checkEditCopyDest.CheckedChanged += new System.EventHandler(this.checkEditCopyDest_CheckedChanged);
            // 
            // orderConfirmationFlyoutPanel
            // 
            this.orderConfirmationFlyoutPanel.Appearance.BackColor = System.Drawing.Color.LimeGreen;
            this.orderConfirmationFlyoutPanel.Appearance.Options.UseBackColor = true;
            this.orderConfirmationFlyoutPanel.Controls.Add(this.labelControl1);
            this.orderConfirmationFlyoutPanel.Location = new System.Drawing.Point(2, 2);
            this.orderConfirmationFlyoutPanel.Name = "orderConfirmationFlyoutPanel";
            this.orderConfirmationFlyoutPanel.OptionsBeakPanel.BackColor = System.Drawing.Color.LimeGreen;
            this.orderConfirmationFlyoutPanel.OwnerControl = this;
            this.orderConfirmationFlyoutPanel.Size = new System.Drawing.Size(591, 31);
            this.orderConfirmationFlyoutPanel.TabIndex = 1;
            this.orderConfirmationFlyoutPanel.Shown += new DevExpress.Utils.FlyoutPanelEventHandler(this.orderConfirmationPopupPanel_Shown);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl1.Location = new System.Drawing.Point(3, 7);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(277, 18);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Order Accepted: AAPL $100 NASDAQ";
            // 
            // Level2PanelControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.level2PanelControlHeader);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "Level2PanelControl";
            this.Size = new System.Drawing.Size(515, 455);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.level2PanelControlHeader)).EndInit();
            this.level2PanelControlHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.settingsFlyoutPanel)).EndInit();
            this.settingsFlyoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkEditOrderFlyout.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditTimeAndSaleColHeaders.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditLevel2ColHeaders.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCopyQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCopyPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCopyDest.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderConfirmationFlyoutPanel)).EndInit();
            this.orderConfirmationFlyoutPanel.ResumeLayout(false);
            this.orderConfirmationFlyoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarStaticItem barStaticSynName;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.GroupControl level2PanelControlHeader;
        private DevExpress.XtraBars.Bar level2PanelControlStatusBar;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem toggleOrderEntryButton;
        private DevExpress.XtraBars.BarButtonItem toggleTASButton;
        private DevExpress.XtraBars.BarButtonItem windowOptionsButton;
        private DevExpress.XtraBars.BarButtonItem saveLayoutButton;
        private DevExpress.Utils.FlyoutPanel orderConfirmationFlyoutPanel;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.Utils.FlyoutPanel settingsFlyoutPanel;
        private DevExpress.XtraEditors.CheckEdit checkEditCopyQty;
        private DevExpress.XtraEditors.CheckEdit checkEditCopyPrice;
        private DevExpress.XtraEditors.CheckEdit checkEditCopyDest;
        private DevExpress.XtraEditors.CheckEdit checkEditTimeAndSaleColHeaders;
        private DevExpress.XtraEditors.CheckEdit checkEditLevel2ColHeaders;
        private DevExpress.XtraEditors.CheckEdit checkEditOrderFlyout;
        /*private DevExpress.XtraBars.BarDockControl barDockControlTop;
private DevExpress.XtraBars.BarDockControl barDockControlBottom;
private DevExpress.XtraBars.BarDockControl barDockControlLeft;
private DevExpress.XtraBars.BarDockControl barDockControlRight;*/
    }
}
