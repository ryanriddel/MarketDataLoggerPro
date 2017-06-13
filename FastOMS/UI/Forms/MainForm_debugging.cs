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
using System.Threading;
using MktSrvcAPI;

namespace FastOMS.UI
{
    public partial class MainForm_debugging : DevExpress.XtraEditors.XtraForm
    {
        public MainForm_debugging()
        {
            InitializeComponent();

            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TestingFunctions.CreateLevel2EquityForm("QQQ");

            TestingFunctions.CreateLevel2EquityForm("SPY");

            TestingFunctions.CreateLevel2EquityForm("AAPL");

            TestingFunctions.CreateLevel2EquityForm("NFLX");

            TestingFunctions.CreateLevel2EquityForm("GS");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Console.WriteLine(Utilities.GetTimestampFromDateTime(DateTime.Now));
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            _textBox.Clear();
            _textBox.Text = PerfAnalyzer.GetPerformanceReport();
        }

        private void buttonSymPicker_Click(object sender, EventArgs e)
        {
            SpreadBuilderForm form = new SpreadBuilderForm();
            form.Show();
        }

        private void buttonLevel2_Click(object sender, EventArgs e)
        {
            SymbolEntryWindow picker = new SymbolEntryWindow(false);
            picker.Show();
            picker.OnEntryComplete += TestingFunctions.CreateLevel2Form;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Hub.ConnectOrderManager();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Hub.TryOrderManagerLogin();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TestingFunctions.CreateLevel2OptionForm(TestingFunctions.TestOption);
        }
        Thread mainFormThread;
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            mainFormThread = new Thread(() => { Application.Run(Hub.RunMainForm()); });
            mainFormThread.SetApartmentState(ApartmentState.STA);
            mainFormThread.Start();
        }
    }
}