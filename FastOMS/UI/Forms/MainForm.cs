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
using System.IO;

namespace FastOMS.UI
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        public MainForm()
        {
            InitializeComponent();
            //Hub.InitializeHub();
            
        }

        private void LayoutSaveAs()
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                Title = @"Save a Layout File",
                Filter = @"XML-File | *.xml"
            };
            dialog.ShowDialog();
            if (dialog.FileName != "")
            {
                Hub._formFactory.SaveLayoutXML(dialog.FileName);
            }
        }

        private void LoadLayoutFromFile()
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Title = @"Open a Layout File",
                Filter = @"XML-File | *.xml"
            };
            dialog.ShowDialog();
            if (dialog.FileName != "")
            {
                Hub._formFactory.LoadLayoutXML(dialog.FileName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                TestingFunctions.CreateLevel2OptionForm(new InstrInfo[] { TestingFunctions.TestSpread3[1]});
                //TestingFunctions.CreateLevel2OptionForm(TestingFunctions.TestSpread3);
                TestingFunctions.CreateLevel2OptionForm(new InstrInfo[] { TestingFunctions.TestSpread3[0]});
            }
            
        }
        
        private void Hub_OnNewFormCreated(Level2Form form)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hub._formFactory.CreateSpreadForm(TestingFunctions.TestSpread2);
            //OrigSpreadForm frm = new OrigSpreadForm(TestingFunctions.FourLegTestSpread);
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //this.TopMost = true;
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
            for (int i = 0; i < 45; i++)
                TestingFunctions.CreateRandomEquityLevel2Form();
        }

        private void buttonLevel2_Click_1(object sender, EventArgs e)
        {
            SymbolEntryWindow entry = new SymbolEntryWindow(false);
            entry.OnEntryComplete += TestingFunctions.CreateLevel2Form;
            entry.Show();
        }

        private void buttonOrderManager_Click(object sender, EventArgs e)
        {
            Hub._formFactory.CreateOrderManagementForm();
        }

        private void buttonLoadLayout_MouseClick(object sender, MouseEventArgs e)
        {
            LoadLayoutFromFile();
        }

        private void buttonSaveLayout_MouseClick(object sender, MouseEventArgs e)
        {
            LayoutSaveAs();
        }

        private void buttonWatchList_Click(object sender, EventArgs e)
        {
            Hub._formFactory.CreateWatchListForm();
        }
        
        private void button3_Click_2(object sender, EventArgs e)
        {
            TestingFunctions.CreateLevel2OptionForm(TestingFunctions.TestSpread3);
        }
        
        private void button6_Click(object sender, EventArgs e)
        {
            //Hub._formFactory.CreateOrigSpreadForm(TestingFunctions.TestSpread3);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            FeedCollector.setupTextBoxUpdaters(UpdateLogTextBox1, UpdateLogTextBox2);
            FeedCollector.Begin();
        }

        public void UpdateLogTextBox1(string textToAdd)
        {
            Console.WriteLine(textToAdd);
                Invoke(new Action(() => { _textBox.Text += textToAdd + Environment.NewLine; }));
            
        }
        public void UpdateLogTextBox2(string textToAdd)
        {
                Invoke(new Action(() => { _textBox2.Text += textToAdd + Environment.NewLine; }));
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Hub._marketDataFeed.DisconnectFromFeed();
        }
    }
}