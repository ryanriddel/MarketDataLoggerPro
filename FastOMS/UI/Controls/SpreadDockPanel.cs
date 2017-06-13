using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars.Docking2010.Views.Tabbed;
using DevExpress.XtraBars.Docking;
using FastOMS.Data_Structures;
using MktSrvcAPI;

namespace FastOMS.UI.Controls
{
    public partial class SpreadDockPanel : DevExpress.XtraEditors.XtraUserControl, IInfraSender<QuoteBook>
    {
        public SpreadCalculationHelper impliedMarket;
        public InstrInfo[] instrumentArray;
        public List<Level2PanelControl> _level2PanelList = new List<Level2PanelControl>(2);
        List<QuoteBook> legs = new List<QuoteBook>();

        public SpreadDockPanel()
        {
            InitializeComponent();

           
        }

        public void InitializeSpreadDockPanel(InstrInfo[] instruments)
        {
            instrumentArray = instruments;
            impliedMarket = new SpreadCalculationHelper(instrumentArray);


            Timer impliedMarketTimer = new Timer();
            impliedMarketTimer.Interval = 1000;
            impliedMarketTimer.Tick += ImpliedMarketTimer_Tick;

            bookToSendToLevel2Panel.Instr = instruments;
            bookToSendToLevel2Panel.NumBid = 1;
            bookToSendToLevel2Panel.NumAsk = 1;
            bookToSendToLevel2Panel.BidExch = new byte[] { 42 };
            bookToSendToLevel2Panel.AskExch = new byte[] { 42 };

            impliedMarketTimer.Enabled = true;
        }

        QuoteBook bookToSendToLevel2Panel = new QuoteBook();
        private void ImpliedMarketTimer_Tick(object sender, EventArgs e)
        {
            bool canCalculateImpliedMarket = true;
            for (int i = 0; i < legs.Count; i++)
            {
                legs[i] = _level2PanelList[i].getRecentQuoteBook();
                if (legs[i].AskBk == null)
                    canCalculateImpliedMarket = false;
            }
            
            if (!canCalculateImpliedMarket)
                return;

            SpreadCalculationHelper.ImpliedMarketData impMkt = impliedMarket.CalculateImpliedMarket(legs);
            QuoteInfo infoBid = new QuoteInfo(), infoAsk = new QuoteInfo();
            infoBid.prc = impMkt.bidPrice;
            infoBid.sz = impMkt.bidSize;
            infoAsk.prc = impMkt.askPrice;
            infoAsk.sz = impMkt.askSize;
            bookToSendToLevel2Panel.BidBk = new QuoteInfo[] { infoBid };
            bookToSendToLevel2Panel.AskBk = new QuoteInfo[] { infoAsk };

            SendInfraData(bookToSendToLevel2Panel);
            
        }

        

        public DockPanel CreateDockPanelFromLevel2Control(Level2PanelControl level2Control)
        {
            level2Control.Dock = DockStyle.Fill;

            DockPanel newDockPanel = dockManager1.AddPanel(DockingStyle.Float);
            newDockPanel.ControlContainer.Controls.Add(level2Control);
            newDockPanel.Text = Utilities.InstrToStr(level2Control.instrumentArray);
            return newDockPanel;
        }

        List<DockPanel> _dockPanelList = new List<DockPanel>();

        

        public BaseDocument CreateDocumentFromLevel2Control(Level2PanelControl level2Control)
        {
            string caption = Utilities.InstrToStr(level2Control.instrumentArray);
            int numberOfPanels = _level2PanelList.Count;

            level2Control.Dock = DockStyle.Fill;


            DockPanel newDockPanel = new DockPanel();
            newDockPanel.Controls.Add(level2Control);
            newDockPanel.DockedAsTabbedDocument = true;
            newDockPanel.Name = "newDockPanel" + numberOfPanels;
            newDockPanel.SavedMdiDocument = true;
            newDockPanel.Text = caption;
            newDockPanel.OriginalSize = level2Control.Size;
            _dockPanelList.Add(newDockPanel);
          

            BaseDocument d = tabbedView1.AddDocument(level2Control);
            d.Caption = caption;
            d.Header = caption;
            
            
            return d;
        }

        public void DockLevel2PanelControl(Level2PanelControl ctrl)
        {
            _level2PanelList.Add(ctrl);
            legs.Add(ctrl.MostRecentQuote);
            BaseDocument newDoc = CreateDocumentFromLevel2Control(ctrl);
            
            int count = _level2PanelList.Count;

            if (count % 2 == 0)
            {
                //add to left side
                _leftDocumentGroup.BeginUpdate();
                
                _leftDocumentGroup.EndUpdate();

            }
            else
            {
                //add to right side
                _rightDocumentGroup.BeginUpdate();
                tabbedView1.Controller.MoveToDocumentGroup((Document) newDoc, true);
                _rightDocumentGroup.EndUpdate();
               
            }
        }


        public IInfraConnector<QuoteBook> infraConnector;

        IInfraConnector<QuoteBook> IInfraSender<QuoteBook>.infraConnector
        {
            get
            {
                return infraConnector;
            }

            set
            {
                infraConnector = value;
            }
        }

        public void SendInfraData(QuoteBook infraData)
        {
            if (infraConnector != null)
                infraConnector.SendDataToReceiver(infraData);
            else
                Console.WriteLine("NULL SENDING INFRA DATA");
        }
    }
}
