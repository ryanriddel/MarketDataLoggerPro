namespace FastOMS.UI
{
    partial class TimeAndSalesControl
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.textEditLast = new DevExpress.XtraEditors.TextEdit();
            this.labelLow = new DevExpress.XtraEditors.LabelControl();
            this.textEditOpen = new DevExpress.XtraEditors.TextEdit();
            this.labelHigh = new DevExpress.XtraEditors.LabelControl();
            this.textEditHigh = new DevExpress.XtraEditors.TextEdit();
            this.labelOpen = new DevExpress.XtraEditors.LabelControl();
            this.textEditLow = new DevExpress.XtraEditors.TextEdit();
            this.labelLast = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.textEditChng = new DevExpress.XtraEditors.TextEdit();
            this.labelChng = new DevExpress.XtraEditors.LabelControl();
            this.textEditPercent = new DevExpress.XtraEditors.TextEdit();
            this.labelPercent = new DevExpress.XtraEditors.LabelControl();
            this.labelVol = new DevExpress.XtraEditors.LabelControl();
            this.textEditVol = new DevExpress.XtraEditors.TextEdit();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditLast.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditOpen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditHigh.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditLow.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditChng.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPercent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditVol.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.LookAndFeel.SkinName = "DevExpress Dark Style";
            this.gridControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(216, 322);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsMenu.EnableColumnMenu = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView1_RowCellStyle);
            this.gridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridView1_MouseDown);
            this.gridView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridView1_MouseUp);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.panelControl2);
            this.splitContainerControl1.Panel1.Controls.Add(this.panelControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gridControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(216, 432);
            this.splitContainerControl1.SplitterPosition = 105;
            this.splitContainerControl1.TabIndex = 1;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // panelControl2
            // 
            this.panelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl2.Controls.Add(this.textEditLast);
            this.panelControl2.Controls.Add(this.labelLow);
            this.panelControl2.Controls.Add(this.textEditOpen);
            this.panelControl2.Controls.Add(this.labelHigh);
            this.panelControl2.Controls.Add(this.textEditHigh);
            this.panelControl2.Controls.Add(this.labelOpen);
            this.panelControl2.Controls.Add(this.textEditLow);
            this.panelControl2.Controls.Add(this.labelLast);
            this.panelControl2.Location = new System.Drawing.Point(3, 50);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(210, 55);
            this.panelControl2.TabIndex = 15;
            // 
            // textEditLast
            // 
            this.textEditLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditLast.Location = new System.Drawing.Point(7, 5);
            this.textEditLast.Name = "textEditLast";
            this.textEditLast.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.textEditLast.Properties.Appearance.Options.UseBackColor = true;
            this.textEditLast.Properties.Appearance.Options.UseTextOptions = true;
            this.textEditLast.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.textEditLast.Properties.ReadOnly = true;
            this.textEditLast.Size = new System.Drawing.Size(45, 20);
            this.textEditLast.TabIndex = 3;
            // 
            // labelLow
            // 
            this.labelLow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLow.Location = new System.Drawing.Point(186, 28);
            this.labelLow.Name = "labelLow";
            this.labelLow.Size = new System.Drawing.Size(19, 13);
            this.labelLow.TabIndex = 13;
            this.labelLow.Text = "Low";
            // 
            // textEditOpen
            // 
            this.textEditOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditOpen.Location = new System.Drawing.Point(58, 5);
            this.textEditOpen.Name = "textEditOpen";
            this.textEditOpen.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.textEditOpen.Properties.Appearance.Options.UseBackColor = true;
            this.textEditOpen.Properties.Appearance.Options.UseTextOptions = true;
            this.textEditOpen.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.textEditOpen.Properties.ReadOnly = true;
            this.textEditOpen.Size = new System.Drawing.Size(45, 20);
            this.textEditOpen.TabIndex = 4;
            // 
            // labelHigh
            // 
            this.labelHigh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelHigh.Location = new System.Drawing.Point(133, 28);
            this.labelHigh.Name = "labelHigh";
            this.labelHigh.Size = new System.Drawing.Size(21, 13);
            this.labelHigh.TabIndex = 12;
            this.labelHigh.Text = "High";
            // 
            // textEditHigh
            // 
            this.textEditHigh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditHigh.Location = new System.Drawing.Point(109, 5);
            this.textEditHigh.Name = "textEditHigh";
            this.textEditHigh.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.textEditHigh.Properties.Appearance.Options.UseBackColor = true;
            this.textEditHigh.Properties.Appearance.Options.UseTextOptions = true;
            this.textEditHigh.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.textEditHigh.Properties.ReadOnly = true;
            this.textEditHigh.Size = new System.Drawing.Size(45, 20);
            this.textEditHigh.TabIndex = 5;
            // 
            // labelOpen
            // 
            this.labelOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelOpen.Location = new System.Drawing.Point(77, 28);
            this.labelOpen.Name = "labelOpen";
            this.labelOpen.Size = new System.Drawing.Size(26, 13);
            this.labelOpen.TabIndex = 11;
            this.labelOpen.Text = "Open";
            // 
            // textEditLow
            // 
            this.textEditLow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditLow.Location = new System.Drawing.Point(160, 5);
            this.textEditLow.Name = "textEditLow";
            this.textEditLow.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.textEditLow.Properties.Appearance.Options.UseBackColor = true;
            this.textEditLow.Properties.Appearance.Options.UseTextOptions = true;
            this.textEditLow.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.textEditLow.Properties.ReadOnly = true;
            this.textEditLow.Size = new System.Drawing.Size(45, 20);
            this.textEditLow.TabIndex = 6;
            // 
            // labelLast
            // 
            this.labelLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLast.Location = new System.Drawing.Point(32, 28);
            this.labelLast.Name = "labelLast";
            this.labelLast.Size = new System.Drawing.Size(20, 13);
            this.labelLast.TabIndex = 10;
            this.labelLast.Text = "Last";
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.Controls.Add(this.textEditChng);
            this.panelControl1.Controls.Add(this.labelChng);
            this.panelControl1.Controls.Add(this.textEditPercent);
            this.panelControl1.Controls.Add(this.labelPercent);
            this.panelControl1.Controls.Add(this.labelVol);
            this.panelControl1.Controls.Add(this.textEditVol);
            this.panelControl1.Location = new System.Drawing.Point(3, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(210, 47);
            this.panelControl1.TabIndex = 14;
            // 
            // textEditChng
            // 
            this.textEditChng.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditChng.Location = new System.Drawing.Point(18, 5);
            this.textEditChng.Name = "textEditChng";
            this.textEditChng.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.textEditChng.Properties.Appearance.Options.UseBackColor = true;
            this.textEditChng.Properties.Appearance.Options.UseTextOptions = true;
            this.textEditChng.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.textEditChng.Properties.ReadOnly = true;
            this.textEditChng.Size = new System.Drawing.Size(45, 20);
            this.textEditChng.TabIndex = 0;
            // 
            // labelChng
            // 
            this.labelChng.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelChng.Location = new System.Drawing.Point(38, 29);
            this.labelChng.Name = "labelChng";
            this.labelChng.Size = new System.Drawing.Size(25, 13);
            this.labelChng.TabIndex = 7;
            this.labelChng.Text = "Chng";
            // 
            // textEditPercent
            // 
            this.textEditPercent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditPercent.Location = new System.Drawing.Point(69, 5);
            this.textEditPercent.Name = "textEditPercent";
            this.textEditPercent.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.textEditPercent.Properties.Appearance.Options.UseBackColor = true;
            this.textEditPercent.Properties.Appearance.Options.UseTextOptions = true;
            this.textEditPercent.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.textEditPercent.Properties.ReadOnly = true;
            this.textEditPercent.Size = new System.Drawing.Size(50, 20);
            this.textEditPercent.TabIndex = 1;
            // 
            // labelPercent
            // 
            this.labelPercent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPercent.Location = new System.Drawing.Point(108, 31);
            this.labelPercent.Name = "labelPercent";
            this.labelPercent.Size = new System.Drawing.Size(11, 13);
            this.labelPercent.TabIndex = 8;
            this.labelPercent.Text = "%";
            // 
            // labelVol
            // 
            this.labelVol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelVol.Location = new System.Drawing.Point(191, 31);
            this.labelVol.Name = "labelVol";
            this.labelVol.Size = new System.Drawing.Size(14, 13);
            this.labelVol.TabIndex = 9;
            this.labelVol.Text = "Vol";
            // 
            // textEditVol
            // 
            this.textEditVol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditVol.Location = new System.Drawing.Point(125, 5);
            this.textEditVol.Name = "textEditVol";
            this.textEditVol.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.textEditVol.Properties.Appearance.Options.UseBackColor = true;
            this.textEditVol.Properties.Appearance.Options.UseTextOptions = true;
            this.textEditVol.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.textEditVol.Properties.ReadOnly = true;
            this.textEditVol.Size = new System.Drawing.Size(80, 20);
            this.textEditVol.TabIndex = 2;
            // 
            // TimeAndSalesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "TimeAndSalesControl";
            this.Size = new System.Drawing.Size(216, 432);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditLast.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditOpen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditHigh.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditLow.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditChng.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPercent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditVol.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.TextEdit textEditLast;
        private DevExpress.XtraEditors.LabelControl labelLow;
        private DevExpress.XtraEditors.TextEdit textEditOpen;
        private DevExpress.XtraEditors.LabelControl labelHigh;
        private DevExpress.XtraEditors.TextEdit textEditHigh;
        private DevExpress.XtraEditors.LabelControl labelOpen;
        private DevExpress.XtraEditors.TextEdit textEditLow;
        private DevExpress.XtraEditors.LabelControl labelLast;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit textEditChng;
        private DevExpress.XtraEditors.LabelControl labelChng;
        private DevExpress.XtraEditors.TextEdit textEditPercent;
        private DevExpress.XtraEditors.LabelControl labelPercent;
        private DevExpress.XtraEditors.LabelControl labelVol;
        private DevExpress.XtraEditors.TextEdit textEditVol;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}
