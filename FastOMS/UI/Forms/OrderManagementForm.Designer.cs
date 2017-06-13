namespace FastOMS.UI
{
    partial class OrderManagementForm
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
            this.tabPane = new DevExpress.XtraBars.Navigation.TabPane();
            this.tabPageRejected = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.orderManagementGridRejected = new FastOMS.UI.OrderManagementGrid();
            this.tabPageExecuted = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.orderManagementGridExecuted = new FastOMS.UI.OrderManagementGrid();
            this.tabPageCancelled = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.orderManagementGridCancelled = new FastOMS.UI.OrderManagementGrid();
            this.tabPageOpen = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.orderManagementGridOpen = new FastOMS.UI.OrderManagementGrid();
            this.tabPageAll = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.orderManagementGridAll = new FastOMS.UI.OrderManagementGrid();
            ((System.ComponentModel.ISupportInitialize)(this.tabPane)).BeginInit();
            this.tabPane.SuspendLayout();
            this.tabPageRejected.SuspendLayout();
            this.tabPageExecuted.SuspendLayout();
            this.tabPageCancelled.SuspendLayout();
            this.tabPageOpen.SuspendLayout();
            this.tabPageAll.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPane
            // 
            this.tabPane.Controls.Add(this.tabPageRejected);
            this.tabPane.Controls.Add(this.tabPageExecuted);
            this.tabPane.Controls.Add(this.tabPageCancelled);
            this.tabPane.Controls.Add(this.tabPageOpen);
            this.tabPane.Controls.Add(this.tabPageAll);
            this.tabPane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPane.Location = new System.Drawing.Point(0, 0);
            this.tabPane.Name = "tabPane";
            this.tabPane.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.tabPageRejected,
            this.tabPageExecuted,
            this.tabPageCancelled,
            this.tabPageOpen,
            this.tabPageAll});
            this.tabPane.RegularSize = new System.Drawing.Size(630, 382);
            this.tabPane.SelectedPage = this.tabPageAll;
            this.tabPane.Size = new System.Drawing.Size(630, 382);
            this.tabPane.TabIndex = 0;
            this.tabPane.Text = "tabPane1";
            // 
            // tabPageRejected
            // 
            this.tabPageRejected.Caption = "Rejected Orders";
            this.tabPageRejected.Controls.Add(this.orderManagementGridRejected);
            this.tabPageRejected.Name = "tabPageRejected";
            this.tabPageRejected.Size = new System.Drawing.Size(612, 337);
            // 
            // orderManagementGridRejected
            // 
            this.orderManagementGridRejected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderManagementGridRejected.Location = new System.Drawing.Point(0, 0);
            this.orderManagementGridRejected.Name = "orderManagementGridRejected";
            this.orderManagementGridRejected.Size = new System.Drawing.Size(612, 337);
            this.orderManagementGridRejected.TabIndex = 1;
            // 
            // tabPageExecuted
            // 
            this.tabPageExecuted.Caption = "Executions";
            this.tabPageExecuted.Controls.Add(this.orderManagementGridExecuted);
            this.tabPageExecuted.Name = "tabPageExecuted";
            this.tabPageExecuted.Size = new System.Drawing.Size(612, 337);
            // 
            // orderManagementGridExecuted
            // 
            this.orderManagementGridExecuted.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderManagementGridExecuted.Location = new System.Drawing.Point(0, 0);
            this.orderManagementGridExecuted.Name = "orderManagementGridExecuted";
            this.orderManagementGridExecuted.Size = new System.Drawing.Size(612, 337);
            this.orderManagementGridExecuted.TabIndex = 1;
            // 
            // tabPageCancelled
            // 
            this.tabPageCancelled.Caption = "Cancelled Orders";
            this.tabPageCancelled.Controls.Add(this.orderManagementGridCancelled);
            this.tabPageCancelled.Name = "tabPageCancelled";
            this.tabPageCancelled.Size = new System.Drawing.Size(612, 337);
            // 
            // orderManagementGridCancelled
            // 
            this.orderManagementGridCancelled.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderManagementGridCancelled.Location = new System.Drawing.Point(0, 0);
            this.orderManagementGridCancelled.Name = "orderManagementGridCancelled";
            this.orderManagementGridCancelled.Size = new System.Drawing.Size(612, 337);
            this.orderManagementGridCancelled.TabIndex = 1;
            // 
            // tabPageOpen
            // 
            this.tabPageOpen.Caption = "Open Orders";
            this.tabPageOpen.Controls.Add(this.orderManagementGridOpen);
            this.tabPageOpen.Name = "tabPageOpen";
            this.tabPageOpen.Size = new System.Drawing.Size(612, 337);
            // 
            // orderManagementGridOpen
            // 
            this.orderManagementGridOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderManagementGridOpen.Location = new System.Drawing.Point(0, 0);
            this.orderManagementGridOpen.Name = "orderManagementGridOpen";
            this.orderManagementGridOpen.Size = new System.Drawing.Size(612, 337);
            this.orderManagementGridOpen.TabIndex = 1;
            // 
            // tabPageAll
            // 
            this.tabPageAll.Caption = "All Orders";
            this.tabPageAll.Controls.Add(this.orderManagementGridAll);
            this.tabPageAll.Name = "tabPageAll";
            this.tabPageAll.Size = new System.Drawing.Size(612, 337);
            // 
            // orderManagementGridAll
            // 
            this.orderManagementGridAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderManagementGridAll.Location = new System.Drawing.Point(0, 0);
            this.orderManagementGridAll.Name = "orderManagementGridAll";
            this.orderManagementGridAll.Size = new System.Drawing.Size(612, 337);
            this.orderManagementGridAll.TabIndex = 0;
            // 
            // OrderManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 382);
            this.Controls.Add(this.tabPane);
            this.Name = "OrderManagementForm";
            this.Text = "OrderManagementForm";
            ((System.ComponentModel.ISupportInitialize)(this.tabPane)).EndInit();
            this.tabPane.ResumeLayout(false);
            this.tabPageRejected.ResumeLayout(false);
            this.tabPageExecuted.ResumeLayout(false);
            this.tabPageCancelled.ResumeLayout(false);
            this.tabPageOpen.ResumeLayout(false);
            this.tabPageAll.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Navigation.TabPane tabPane;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabPageRejected;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabPageExecuted;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabPageCancelled;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabPageOpen;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabPageAll;
        private OrderManagementGrid orderManagementGridRejected;
        private OrderManagementGrid orderManagementGridExecuted;
        private OrderManagementGrid orderManagementGridCancelled;
        private OrderManagementGrid orderManagementGridOpen;
        private OrderManagementGrid orderManagementGridAll;
    }
}