namespace FastOMS.UI
{
    partial class MainForm
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
            this.button1 = new System.Windows.Forms.Button();
            this._textBox = new System.Windows.Forms.TextBox();
            this._textBox2 = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(66, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(167, 35);
            this.button1.TabIndex = 0;
            this.button1.Text = "Create Lots O\' Options Forms";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // _textBox
            // 
            this._textBox.Location = new System.Drawing.Point(12, 119);
            this._textBox.Multiline = true;
            this._textBox.Name = "_textBox";
            this._textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._textBox.Size = new System.Drawing.Size(277, 322);
            this._textBox.TabIndex = 3;
            // 
            // _textBox2
            // 
            this._textBox2.Location = new System.Drawing.Point(296, 119);
            this._textBox2.Multiline = true;
            this._textBox2.Name = "_textBox2";
            this._textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._textBox2.Size = new System.Drawing.Size(277, 322);
            this._textBox2.TabIndex = 6;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(341, 12);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(167, 35);
            this.button5.TabIndex = 9;
            this.button5.Text = "Create Lots O\'Equity Forms";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(66, 78);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(167, 35);
            this.button4.TabIndex = 17;
            this.button4.Text = "Enable Feed Logging";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(345, 53);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(163, 37);
            this.button2.TabIndex = 18;
            this.button2.Text = "Disconnect Market Data";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 453);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button5);
            this.Controls.Add(this._textBox2);
            this.Controls.Add(this._textBox);
            this.Controls.Add(this.button1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox _textBox;
        private System.Windows.Forms.TextBox _textBox2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button2;
    }
}