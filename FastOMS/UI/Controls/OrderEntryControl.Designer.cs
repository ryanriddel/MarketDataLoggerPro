namespace FastOMS.UI
{
    partial class OrderEntryControl
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
            this.buttonBuy = new DevExpress.XtraEditors.SimpleButton();
            this.buttonSell = new DevExpress.XtraEditors.SimpleButton();
            this.spinnerQty = new DevExpress.XtraEditors.SpinEdit();
            this.spinnerPrice = new DevExpress.XtraEditors.SpinEdit();
            this.labelQty = new DevExpress.XtraEditors.LabelControl();
            this.labelPrice = new DevExpress.XtraEditors.LabelControl();
            this.labelOpenClose = new DevExpress.XtraEditors.LabelControl();
            this.labelTiF = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxDest = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelDest = new DevExpress.XtraEditors.LabelControl();
            this.buttonClear = new DevExpress.XtraEditors.SimpleButton();
            this.comboBoxTiF = new DevExpress.XtraEditors.ComboBoxEdit();
            this.comboBoxOpenClose = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.spinnerQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinnerPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxDest.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxTiF.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxOpenClose.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonBuy
            // 
            this.buttonBuy.Location = new System.Drawing.Point(3, 20);
            this.buttonBuy.Name = "buttonBuy";
            this.buttonBuy.Size = new System.Drawing.Size(65, 23);
            this.buttonBuy.TabIndex = 0;
            this.buttonBuy.Text = "Buy";
            this.buttonBuy.MouseClick += new System.Windows.Forms.MouseEventHandler(this.buttonBuy_MouseClick);
            // 
            // buttonSell
            // 
            this.buttonSell.Location = new System.Drawing.Point(216, 20);
            this.buttonSell.Name = "buttonSell";
            this.buttonSell.Size = new System.Drawing.Size(65, 23);
            this.buttonSell.TabIndex = 1;
            this.buttonSell.Text = "Sell";
            this.buttonSell.MouseClick += new System.Windows.Forms.MouseEventHandler(this.buttonSell_MouseClick);
            // 
            // spinnerQty
            // 
            this.spinnerQty.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinnerQty.Location = new System.Drawing.Point(74, 21);
            this.spinnerQty.Name = "spinnerQty";
            this.spinnerQty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinnerQty.Properties.Mask.EditMask = "d";
            this.spinnerQty.Properties.MaxValue = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.spinnerQty.Size = new System.Drawing.Size(65, 20);
            this.spinnerQty.TabIndex = 2;
            this.spinnerQty.EditValueChanged += new System.EventHandler(this.orderField_EditValueChanged);
            // 
            // spinnerPrice
            // 
            this.spinnerPrice.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinnerPrice.Location = new System.Drawing.Point(145, 21);
            this.spinnerPrice.Name = "spinnerPrice";
            this.spinnerPrice.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinnerPrice.Properties.Mask.EditMask = "f2";
            this.spinnerPrice.Size = new System.Drawing.Size(65, 20);
            this.spinnerPrice.TabIndex = 3;
            this.spinnerPrice.EditValueChanged += new System.EventHandler(this.orderField_EditValueChanged);
            // 
            // labelQty
            // 
            this.labelQty.Location = new System.Drawing.Point(74, 4);
            this.labelQty.Name = "labelQty";
            this.labelQty.Size = new System.Drawing.Size(22, 13);
            this.labelQty.TabIndex = 4;
            this.labelQty.Text = "Qty:";
            // 
            // labelPrice
            // 
            this.labelPrice.Location = new System.Drawing.Point(145, 4);
            this.labelPrice.Name = "labelPrice";
            this.labelPrice.Size = new System.Drawing.Size(27, 13);
            this.labelPrice.TabIndex = 5;
            this.labelPrice.Text = "Price:";
            // 
            // labelOpenClose
            // 
            this.labelOpenClose.Location = new System.Drawing.Point(3, 47);
            this.labelOpenClose.Name = "labelOpenClose";
            this.labelOpenClose.Size = new System.Drawing.Size(23, 13);
            this.labelOpenClose.TabIndex = 6;
            this.labelOpenClose.Text = "O/C:";
            // 
            // labelTiF
            // 
            this.labelTiF.Location = new System.Drawing.Point(74, 47);
            this.labelTiF.Name = "labelTiF";
            this.labelTiF.Size = new System.Drawing.Size(18, 13);
            this.labelTiF.TabIndex = 9;
            this.labelTiF.Text = "TiF:";
            // 
            // comboBoxDest
            // 
            this.comboBoxDest.Location = new System.Drawing.Point(145, 66);
            this.comboBoxDest.Name = "comboBoxDest";
            this.comboBoxDest.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxDest.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxDest.Size = new System.Drawing.Size(65, 20);
            this.comboBoxDest.TabIndex = 10;
            this.comboBoxDest.EditValueChanged += new System.EventHandler(this.orderField_EditValueChanged);
            // 
            // labelDest
            // 
            this.labelDest.Location = new System.Drawing.Point(145, 47);
            this.labelDest.Name = "labelDest";
            this.labelDest.Size = new System.Drawing.Size(26, 13);
            this.labelDest.TabIndex = 11;
            this.labelDest.Text = "Dest:";
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(216, 64);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(65, 23);
            this.buttonClear.TabIndex = 12;
            this.buttonClear.Text = "Clear";
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // comboBoxTiF
            // 
            this.comboBoxTiF.Location = new System.Drawing.Point(74, 66);
            this.comboBoxTiF.Name = "comboBoxTiF";
            this.comboBoxTiF.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxTiF.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxTiF.Size = new System.Drawing.Size(65, 20);
            this.comboBoxTiF.TabIndex = 13;
            this.comboBoxTiF.EditValueChanged += new System.EventHandler(this.orderField_EditValueChanged);
            // 
            // comboBoxOpenClose
            // 
            this.comboBoxOpenClose.Location = new System.Drawing.Point(3, 66);
            this.comboBoxOpenClose.Name = "comboBoxOpenClose";
            this.comboBoxOpenClose.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxOpenClose.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxOpenClose.Size = new System.Drawing.Size(65, 20);
            this.comboBoxOpenClose.TabIndex = 14;
            this.comboBoxOpenClose.EditValueChanged += new System.EventHandler(this.orderField_EditValueChanged);
            // 
            // OrderEntryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBoxOpenClose);
            this.Controls.Add(this.comboBoxTiF);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.labelDest);
            this.Controls.Add(this.comboBoxDest);
            this.Controls.Add(this.labelTiF);
            this.Controls.Add(this.labelOpenClose);
            this.Controls.Add(this.labelPrice);
            this.Controls.Add(this.labelQty);
            this.Controls.Add(this.spinnerPrice);
            this.Controls.Add(this.spinnerQty);
            this.Controls.Add(this.buttonSell);
            this.Controls.Add(this.buttonBuy);
            this.Name = "OrderEntryControl";
            this.Size = new System.Drawing.Size(291, 101);
            ((System.ComponentModel.ISupportInitialize)(this.spinnerQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinnerPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxDest.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxTiF.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxOpenClose.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        
        private DevExpress.XtraEditors.SpinEdit spinnerQty;
        private DevExpress.XtraEditors.SpinEdit spinnerPrice;
        private DevExpress.XtraEditors.LabelControl labelQty;
        private DevExpress.XtraEditors.LabelControl labelPrice;
        private DevExpress.XtraEditors.LabelControl labelOpenClose;
        private DevExpress.XtraEditors.LabelControl labelTiF;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxDest;
        private DevExpress.XtraEditors.LabelControl labelDest;
        private DevExpress.XtraEditors.SimpleButton buttonBuy;
        private DevExpress.XtraEditors.SimpleButton buttonSell;
        private DevExpress.XtraEditors.SimpleButton buttonClear;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxTiF;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxOpenClose;
    }
}
