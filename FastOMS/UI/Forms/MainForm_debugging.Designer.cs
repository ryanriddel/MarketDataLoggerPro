namespace FastOMS.UI
{
    partial class MainForm_debugging
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
            this.button2 = new System.Windows.Forms.Button();
            this._textBox = new System.Windows.Forms.TextBox();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.buttonSymPicker = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(66, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(167, 76);
            this.button1.TabIndex = 0;
            this.button1.Text = "Create 5 Level 2 Forms";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(66, 94);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(167, 35);
            this.button2.TabIndex = 1;
            this.button2.Text = "spreadForm";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // _textBox
            // 
            this._textBox.Location = new System.Drawing.Point(12, 222);
            this._textBox.Multiline = true;
            this._textBox.Name = "_textBox";
            this._textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._textBox.Size = new System.Drawing.Size(277, 219);
            this._textBox.TabIndex = 3;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(66, 176);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(167, 40);
            this.simpleButton1.TabIndex = 4;
            this.simpleButton1.Text = "Get Performance Report";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // buttonSymPicker
            // 
            this.buttonSymPicker.Location = new System.Drawing.Point(66, 135);
            this.buttonSymPicker.Name = "buttonSymPicker";
            this.buttonSymPicker.Size = new System.Drawing.Size(167, 35);
            this.buttonSymPicker.TabIndex = 5;
            this.buttonSymPicker.Text = "Symbol Picker";
            this.buttonSymPicker.UseVisualStyleBackColor = true;
            this.buttonSymPicker.Click += new System.EventHandler(this.buttonSymPicker_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(296, 222);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(277, 219);
            this.textBox1.TabIndex = 6;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(343, 26);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(167, 49);
            this.button3.TabIndex = 7;
            this.button3.Text = "Connect to Order Manager Server";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(343, 94);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(167, 35);
            this.button4.TabIndex = 8;
            this.button4.Text = "Log in";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(343, 140);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(167, 76);
            this.button5.TabIndex = 9;
            this.button5.Text = "Create Option Level2 Form";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(234, 465);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(121, 26);
            this.simpleButton2.TabIndex = 10;
            this.simpleButton2.Text = "New Main Form";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // MainForm_debugging
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 503);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.buttonSymPicker);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this._textBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "MainForm_debugging";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox _textBox;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.Button buttonSymPicker;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
    }
}