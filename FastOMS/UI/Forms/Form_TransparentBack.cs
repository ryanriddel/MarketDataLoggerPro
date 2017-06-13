using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace FastOMS.UI.Forms
{
    public partial class Form_TransparentBack : XtraForm
    {
        public Form_TransparentBack(XtraForm _foregroundForm)
        {
            InitializeComponent();

            StartPosition = _foregroundForm.StartPosition;
            Location = _foregroundForm.Location;
            Size = _foregroundForm.Size;
            _foregroundForm.Resize += _foregroundForm_Resize;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            _foregroundForm.LocationChanged += _foregroundForm_LocationChanged;
            ShowInTaskbar = false;
            //BackColor = Color.WhiteSmoke;
            Opacity = 0.5;
            Timer timer = new Timer() { Interval = 10 };
            timer.Tick += delegate (object sn, EventArgs ea)
            {
                (sn as Timer).Stop();
                _foregroundForm.ShowDialog();
            };
            timer.Start();
            Show();

        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }

        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;

        void _foregroundForm_LocationChanged(object sender, EventArgs e)
        {
            Location = (sender as XtraForm).Location;
        }

        void _foregroundForm_Resize(object sender, EventArgs e)
        {
            WindowState = (sender as XtraForm).WindowState;
            Size = (sender as XtraForm).Size;
        }

        private void Form_TransparentBack_Load(object sender, EventArgs e)
        {

        }
    }
}