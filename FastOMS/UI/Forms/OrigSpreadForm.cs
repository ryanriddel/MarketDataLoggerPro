using DevExpress.XtraLayout;
using FastOMS.Data_Structures;
using FastOMS.UI.Interfaces;
using MktSrvcAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace FastOMS.UI
{
    public partial class OrigSpreadForm : Window, ILayoutSaveLoader
    {
        private SpreadCalculationHelper _spreadCalc;
        QuoteBook impliedMarketBook = new QuoteBook();
        List<QuoteBook> legs = new List<QuoteBook>();

        private InstrInfo[] _instruments;

        public List<Level2PanelControl> Level2Forms = new List<Level2PanelControl>();

        public OrigSpreadForm()
        {
            InitializeComponent();
        }

        public OrigSpreadForm(InstrInfo[] inst) : this()
        {
            _instruments = inst;
            Text = Utilities.InstrArrayToOldOMSString(_instruments);
            Level2PanelControl l2Form = new Level2PanelControl();
            l2Form.orderEntryControl.Initialize(_instruments);
            Level2Forms.Add(l2Form);

            if (inst.Length > 1)
            {
                for (int i = 0; i < inst.Length; i++)
                {
                    InstrInfo[] legInfo = inst.Skip(i).Take(1).ToArray();
                    Level2PanelControl legForm = new Level2PanelControl();
                    legForm.orderEntryControl.Initialize(legInfo);
                    Level2Forms.Add(legForm);
                    legs.Add(legForm.MostRecentQuote);
                }
            }

            for (int i = 0; i < Level2Forms.Count; i++)
            {
                Level2PanelControl legForm = Level2Forms[i];

                LayoutControlItem layoutItem = layoutControl1.Root.AddItem();
                layoutItem.Name = "LayoutItem" + i;
                layoutItem.Control = legForm;
                layoutItem.TextVisible = false;
            }
            layoutControl1.BestFit();
            Size = new System.Drawing.Size(Size.Width * Level2Forms.Count, Size.Height);
            StartImpliedMarkets();
        }
        
        private void StartImpliedMarkets()
        {
            _spreadCalc = new SpreadCalculationHelper(_instruments);
            impliedMarketBook.Instr = _instruments;
            impliedMarketBook.NumBid = 1;
            impliedMarketBook.NumAsk = 1;
            impliedMarketBook.BidExch = new byte[] { 42 };
            impliedMarketBook.AskExch = new byte[] { 42 };

            Timer impliedMarketTimer = new Timer();
            impliedMarketTimer.Interval = 1000;
            impliedMarketTimer.Tick += ImpliedMarketTimer_Tick;
            impliedMarketTimer.Enabled = true;
        }

        private void ImpliedMarketTimer_Tick(object sender, EventArgs e)
        {
            bool canCalculateImpliedMarket = true;
            for (int i = 1; i < Level2Forms.Count; i++)
            {
                legs[i - 1] = Level2Forms[i].getRecentQuoteBook();
                if (legs[i - 1].AskBk == null)
                {
                    canCalculateImpliedMarket = false;
                }
            }

            if (canCalculateImpliedMarket)
            {
                SpreadCalculationHelper.ImpliedMarketData impMkt = _spreadCalc.CalculateImpliedMarket(legs);
                QuoteInfo infoBid = new QuoteInfo() { prc = impMkt.bidPrice, sz = impMkt.bidSize };
                QuoteInfo infoAsk = new QuoteInfo() { prc = impMkt.askPrice, sz = impMkt.askSize };
                impliedMarketBook.BidBk = new QuoteInfo[] { infoBid };
                impliedMarketBook.AskBk = new QuoteInfo[] { infoAsk };

                Level2Forms[0].InfraDataReceiveHandler(impliedMarketBook);
            }
        }

        public void LoadLayoutXML(string layoutXML)
        {
            using (XmlReader reader = XmlReader.Create(new StringReader(layoutXML)))
            {
                int loadedLevel2Layouts = 0;
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "OrigSpreadForm":
                                if (reader["Location"] != null)
                                {
                                    string[] pointSplit = reader["Location"].Split(',');
                                    DesktopLocation = new System.Drawing.Point(Convert.ToInt32(pointSplit[0]), Convert.ToInt32(pointSplit[1]));
                                }
                                if (reader["Size"] != null)
                                {
                                    string[] pointSplit = reader["Size"].Split(',');
                                    Size = new System.Drawing.Size(Convert.ToInt32(pointSplit[0]), Convert.ToInt32(pointSplit[1]));
                                }
                                break;
                            case "Level2PanelControl":
                                Level2Forms[loadedLevel2Layouts].LoadLayoutXML(reader.ReadSubtree());
                                loadedLevel2Layouts++;
                                break;
                        }
                    }
                }
            }
        }

        public string GetLayoutXML()
        {
            using (StringWriter sw = new StringWriter())
            {
                try
                {
                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.ConformanceLevel = ConformanceLevel.Fragment;
                    settings.OmitXmlDeclaration = true;
                    using (XmlWriter xw = XmlWriter.Create(sw, settings))
                    {
                        xw.WriteStartElement("OrigSpreadForm");
                        xw.WriteAttributeString("Location", this.DesktopLocation.X + "," + this.DesktopLocation.Y);
                        xw.WriteAttributeString("Size", this.Size.Width + "," + this.Size.Height);
                        xw.WriteAttributeString("Instruments", Utilities.InstrArrayToOldOMSString(_instruments));

                        for (int i = 0; i < Level2Forms.Count; i++)
                        {
                            xw.WriteRaw(Level2Forms[i].GetLayoutXML());
                        }

                        xw.WriteEndElement();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
                return sw.ToString();
            }
        }
    }
}