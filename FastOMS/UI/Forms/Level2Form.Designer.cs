namespace FastOMS.UI
{
    partial class Level2Form
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
            this.level2PanelControl = new FastOMS.UI.Level2PanelControl();
            this.SuspendLayout();
            // 
            // level2PanelControl
            // 
            this.level2PanelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.level2PanelControl.Location = new System.Drawing.Point(0, 0);
            this.level2PanelControl.Name = "level2PanelControl";
            this.level2PanelControl.Size = new System.Drawing.Size(529, 457);
            this.level2PanelControl.TabIndex = 0;
            // 
            // Level2Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 457);
            this.Controls.Add(this.level2PanelControl);
            this.Name = "Level2Form";
            this.Text = "Level2Form";
            this.Load += new System.EventHandler(this.Level2Form_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public Level2PanelControl level2PanelControl;
    }
}