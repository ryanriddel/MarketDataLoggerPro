namespace FastOMS.UI
{
    partial class SpreadBuilderForm
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
            this.buttonAdd = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.buttonRemoveAll = new DevExpress.XtraEditors.SimpleButton();
            this.buttonFlip = new DevExpress.XtraEditors.SimpleButton();
            this.buttonQuote = new DevExpress.XtraEditors.SimpleButton();
            this.buttonDockQuote = new DevExpress.XtraEditors.SimpleButton();
            this.pictureBoxCopyOut = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCopyOut)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAdd.Location = new System.Drawing.Point(12, 275);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 0;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.AllowDrop = true;
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(515, 268);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.DragDrop += new System.Windows.Forms.DragEventHandler(this.gridControl1_DragDrop);
            this.gridControl1.DragOver += new System.Windows.Forms.DragEventHandler(this.gridControl1_DragOver);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gridView1_PopupMenuShowing);
            this.gridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridView1_MouseDown);
            // 
            // buttonRemoveAll
            // 
            this.buttonRemoveAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRemoveAll.Location = new System.Drawing.Point(93, 275);
            this.buttonRemoveAll.Name = "buttonRemoveAll";
            this.buttonRemoveAll.Size = new System.Drawing.Size(75, 23);
            this.buttonRemoveAll.TabIndex = 3;
            this.buttonRemoveAll.Text = "Remove All";
            this.buttonRemoveAll.Click += new System.EventHandler(this.buttonRemoveAll_Click);
            // 
            // buttonFlip
            // 
            this.buttonFlip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonFlip.Location = new System.Drawing.Point(174, 275);
            this.buttonFlip.Name = "buttonFlip";
            this.buttonFlip.Size = new System.Drawing.Size(75, 23);
            this.buttonFlip.TabIndex = 4;
            this.buttonFlip.Text = "Flip";
            this.buttonFlip.Click += new System.EventHandler(this.buttonFlip_Click);
            // 
            // buttonQuote
            // 
            this.buttonQuote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonQuote.Location = new System.Drawing.Point(397, 275);
            this.buttonQuote.Name = "buttonQuote";
            this.buttonQuote.Size = new System.Drawing.Size(75, 23);
            this.buttonQuote.TabIndex = 5;
            this.buttonQuote.Text = "Quote";
            this.buttonQuote.Click += new System.EventHandler(this.buttonQuote_Click);
            // 
            // buttonDockQuote
            // 
            this.buttonDockQuote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDockQuote.Location = new System.Drawing.Point(306, 275);
            this.buttonDockQuote.Name = "buttonDockQuote";
            this.buttonDockQuote.Size = new System.Drawing.Size(85, 23);
            this.buttonDockQuote.TabIndex = 6;
            this.buttonDockQuote.Text = "Quote (docked)";
            this.buttonDockQuote.Click += new System.EventHandler(this.buttonDockQuote_Click);
            // 
            // pictureBoxCopyOut
            // 
            this.pictureBoxCopyOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxCopyOut.Image = global::FastOMS.Properties.Resources.copyoutDisabled;
            this.pictureBoxCopyOut.Location = new System.Drawing.Point(478, 273);
            this.pictureBoxCopyOut.Name = "pictureBoxCopyOut";
            this.pictureBoxCopyOut.Size = new System.Drawing.Size(25, 25);
            this.pictureBoxCopyOut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxCopyOut.TabIndex = 8;
            this.pictureBoxCopyOut.TabStop = false;
            this.pictureBoxCopyOut.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxCopyOut_MouseDown);
            // 
            // SpreadBuilderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 310);
            this.Controls.Add(this.pictureBoxCopyOut);
            this.Controls.Add(this.buttonDockQuote);
            this.Controls.Add(this.buttonQuote);
            this.Controls.Add(this.buttonFlip);
            this.Controls.Add(this.buttonRemoveAll);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.buttonAdd);
            this.Name = "SpreadBuilderForm";
            this.Text = "Spread Builder";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCopyOut)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton buttonAdd;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton buttonRemoveAll;
        private DevExpress.XtraEditors.SimpleButton buttonFlip;
        private DevExpress.XtraEditors.SimpleButton buttonQuote;
        private DevExpress.XtraEditors.SimpleButton buttonDockQuote;
        private System.Windows.Forms.PictureBox pictureBoxCopyOut;
    }
}