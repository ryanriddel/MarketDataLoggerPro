namespace FastOMS.UI
{
    partial class SymbolEntryWindow
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
            this.panelOptionDetails = new DevExpress.XtraEditors.PanelControl();
            this.radioGroupCallPut = new DevExpress.XtraEditors.RadioGroup();
            this.labelStrike = new DevExpress.XtraEditors.LabelControl();
            this.textEditStrike = new DevExpress.XtraEditors.TextEdit();
            this.labelMaturity = new DevExpress.XtraEditors.LabelControl();
            this.textEditSymbol = new DevExpress.XtraEditors.TextEdit();
            this.radioGroupType = new DevExpress.XtraEditors.RadioGroup();
            this.labelSymbol = new DevExpress.XtraEditors.LabelControl();
            this.panelBaseSymbol = new DevExpress.XtraEditors.PanelControl();
            this.panelSideRatio = new DevExpress.XtraEditors.PanelControl();
            this.radioGroupSide = new DevExpress.XtraEditors.RadioGroup();
            this.textEditRatio = new DevExpress.XtraEditors.TextEdit();
            this.labelRatio = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.buttonOK = new DevExpress.XtraEditors.SimpleButton();
            this.dateEditMaturity = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelOptionDetails)).BeginInit();
            this.panelOptionDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupCallPut.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditStrike.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditSymbol.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelBaseSymbol)).BeginInit();
            this.panelBaseSymbol.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelSideRatio)).BeginInit();
            this.panelSideRatio.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupSide.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditRatio.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditMaturity.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditMaturity.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelOptionDetails
            // 
            this.panelOptionDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelOptionDetails.Controls.Add(this.dateEditMaturity);
            this.panelOptionDetails.Controls.Add(this.radioGroupCallPut);
            this.panelOptionDetails.Controls.Add(this.labelStrike);
            this.panelOptionDetails.Controls.Add(this.textEditStrike);
            this.panelOptionDetails.Controls.Add(this.labelMaturity);
            this.panelOptionDetails.Location = new System.Drawing.Point(4, 68);
            this.panelOptionDetails.Margin = new System.Windows.Forms.Padding(1);
            this.panelOptionDetails.Name = "panelOptionDetails";
            this.panelOptionDetails.Size = new System.Drawing.Size(167, 87);
            this.panelOptionDetails.TabIndex = 13;
            // 
            // radioGroupCallPut
            // 
            this.radioGroupCallPut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioGroupCallPut.Location = new System.Drawing.Point(5, 5);
            this.radioGroupCallPut.Name = "radioGroupCallPut";
            this.radioGroupCallPut.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Call"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Put")});
            this.radioGroupCallPut.Size = new System.Drawing.Size(157, 25);
            this.radioGroupCallPut.TabIndex = 2;
            this.radioGroupCallPut.EditValueChanged += new System.EventHandler(this.inputField_EditValueChanged);
            // 
            // labelStrike
            // 
            this.labelStrike.Location = new System.Drawing.Point(5, 64);
            this.labelStrike.Name = "labelStrike";
            this.labelStrike.Size = new System.Drawing.Size(31, 13);
            this.labelStrike.TabIndex = 10;
            this.labelStrike.Text = "Strike:";
            // 
            // textEditStrike
            // 
            this.textEditStrike.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditStrike.Location = new System.Drawing.Point(78, 61);
            this.textEditStrike.Name = "textEditStrike";
            this.textEditStrike.Properties.Mask.EditMask = "f2";
            this.textEditStrike.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.textEditStrike.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.textEditStrike.Size = new System.Drawing.Size(85, 20);
            this.textEditStrike.TabIndex = 4;
            this.textEditStrike.EditValueChanged += new System.EventHandler(this.inputField_EditValueChanged);
            // 
            // labelMaturity
            // 
            this.labelMaturity.Location = new System.Drawing.Point(5, 39);
            this.labelMaturity.Name = "labelMaturity";
            this.labelMaturity.Size = new System.Drawing.Size(44, 13);
            this.labelMaturity.TabIndex = 9;
            this.labelMaturity.Text = "Maturity:";
            // 
            // textEditSymbol
            // 
            this.textEditSymbol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditSymbol.Location = new System.Drawing.Point(77, 5);
            this.textEditSymbol.Name = "textEditSymbol";
            this.textEditSymbol.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textEditSymbol.Size = new System.Drawing.Size(85, 20);
            this.textEditSymbol.TabIndex = 0;
            this.textEditSymbol.EditValueChanged += new System.EventHandler(this.inputField_EditValueChanged);
            // 
            // radioGroupType
            // 
            this.radioGroupType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioGroupType.Location = new System.Drawing.Point(5, 31);
            this.radioGroupType.Name = "radioGroupType";
            this.radioGroupType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Option"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Equity")});
            this.radioGroupType.Size = new System.Drawing.Size(157, 25);
            this.radioGroupType.TabIndex = 1;
            this.radioGroupType.SelectedIndexChanged += new System.EventHandler(this.radioGroupType_SelectedIndexChanged);
            // 
            // labelSymbol
            // 
            this.labelSymbol.Location = new System.Drawing.Point(5, 8);
            this.labelSymbol.Name = "labelSymbol";
            this.labelSymbol.Size = new System.Drawing.Size(38, 13);
            this.labelSymbol.TabIndex = 8;
            this.labelSymbol.Text = "Symbol:";
            // 
            // panelBaseSymbol
            // 
            this.panelBaseSymbol.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBaseSymbol.Controls.Add(this.textEditSymbol);
            this.panelBaseSymbol.Controls.Add(this.labelSymbol);
            this.panelBaseSymbol.Controls.Add(this.radioGroupType);
            this.panelBaseSymbol.Location = new System.Drawing.Point(4, 3);
            this.panelBaseSymbol.Margin = new System.Windows.Forms.Padding(1);
            this.panelBaseSymbol.Name = "panelBaseSymbol";
            this.panelBaseSymbol.Size = new System.Drawing.Size(167, 63);
            this.panelBaseSymbol.TabIndex = 12;
            // 
            // panelSideRatio
            // 
            this.panelSideRatio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSideRatio.Controls.Add(this.radioGroupSide);
            this.panelSideRatio.Controls.Add(this.textEditRatio);
            this.panelSideRatio.Controls.Add(this.labelRatio);
            this.panelSideRatio.Location = new System.Drawing.Point(4, 157);
            this.panelSideRatio.Margin = new System.Windows.Forms.Padding(1);
            this.panelSideRatio.Name = "panelSideRatio";
            this.panelSideRatio.Size = new System.Drawing.Size(167, 63);
            this.panelSideRatio.TabIndex = 14;
            // 
            // radioGroupSide
            // 
            this.radioGroupSide.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioGroupSide.Location = new System.Drawing.Point(5, 5);
            this.radioGroupSide.Name = "radioGroupSide";
            this.radioGroupSide.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Buy"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Sell")});
            this.radioGroupSide.Size = new System.Drawing.Size(157, 25);
            this.radioGroupSide.TabIndex = 5;
            // 
            // textEditRatio
            // 
            this.textEditRatio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditRatio.Location = new System.Drawing.Point(77, 36);
            this.textEditRatio.Name = "textEditRatio";
            this.textEditRatio.Properties.Mask.EditMask = "d";
            this.textEditRatio.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.textEditRatio.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.textEditRatio.Size = new System.Drawing.Size(85, 20);
            this.textEditRatio.TabIndex = 6;
            this.textEditRatio.EditValueChanged += new System.EventHandler(this.inputField_EditValueChanged);
            // 
            // labelRatio
            // 
            this.labelRatio.Location = new System.Drawing.Point(5, 39);
            this.labelRatio.Name = "labelRatio";
            this.labelRatio.Size = new System.Drawing.Size(29, 13);
            this.labelRatio.TabIndex = 11;
            this.labelRatio.Text = "Ratio:";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(50, 20);
            // 
            // buttonOK
            // 
            this.buttonOK.Enabled = false;
            this.buttonOK.Location = new System.Drawing.Point(4, 224);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 7;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // dateEditMaturity
            // 
            this.dateEditMaturity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateEditMaturity.EditValue = null;
            this.dateEditMaturity.Location = new System.Drawing.Point(78, 36);
            this.dateEditMaturity.Name = "dateEditMaturity";
            this.dateEditMaturity.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditMaturity.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditMaturity.Size = new System.Drawing.Size(85, 20);
            this.dateEditMaturity.TabIndex = 3;
            // 
            // SymbolEntryWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(174, 252);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.panelOptionDetails);
            this.Controls.Add(this.panelBaseSymbol);
            this.Controls.Add(this.panelSideRatio);
            this.Name = "SymbolEntryWindow";
            ((System.ComponentModel.ISupportInitialize)(this.panelOptionDetails)).EndInit();
            this.panelOptionDetails.ResumeLayout(false);
            this.panelOptionDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupCallPut.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditStrike.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditSymbol.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelBaseSymbol)).EndInit();
            this.panelBaseSymbol.ResumeLayout(false);
            this.panelBaseSymbol.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelSideRatio)).EndInit();
            this.panelSideRatio.ResumeLayout(false);
            this.panelSideRatio.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupSide.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditRatio.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditMaturity.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditMaturity.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelOptionDetails;
        private DevExpress.XtraEditors.RadioGroup radioGroupCallPut;
        private DevExpress.XtraEditors.LabelControl labelStrike;
        private DevExpress.XtraEditors.TextEdit textEditStrike;
        private DevExpress.XtraEditors.LabelControl labelMaturity;
        private DevExpress.XtraEditors.TextEdit textEditSymbol;
        private DevExpress.XtraEditors.RadioGroup radioGroupType;
        private DevExpress.XtraEditors.LabelControl labelSymbol;
        private DevExpress.XtraEditors.PanelControl panelBaseSymbol;
        private DevExpress.XtraEditors.PanelControl panelSideRatio;
        private DevExpress.XtraEditors.RadioGroup radioGroupSide;
        private DevExpress.XtraEditors.TextEdit textEditRatio;
        private DevExpress.XtraEditors.LabelControl labelRatio;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton buttonOK;
        private DevExpress.XtraEditors.DateEdit dateEditMaturity;
    }
}
