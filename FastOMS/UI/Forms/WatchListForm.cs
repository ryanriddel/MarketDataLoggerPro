using FastOMS.Data_Structures;
using FastOMS.UI.Interfaces;
using MktSrvcAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace FastOMS.UI
{
    public partial class WatchListForm : DevExpress.XtraEditors.XtraForm, IGenericMarketDataConsumer, ILayoutSaveLoader
    {
        private List<InstrInfo[]> subscribedInstruemnts = new List<InstrInfo[]>();

        public WatchListForm()
        {
            InitializeComponent();
        }

        public void NewDataHandler(TradeInfo _event)
        {
            ((IMarketDataConsumer<TradeInfo>)watchListGrid1).NewDataHandler(_event);
        }

        public void NewDataHandler(QuoteBook _event)
        {
            ((IMarketDataConsumer<QuoteBook>)watchListGrid1).NewDataHandler(_event);
        }

        private void AddSymbolSubscription(InstrInfo[] instrument)
        {
            subscribedInstruemnts.Add(instrument);
            Hub._marketDataFeed.AddQuoteConsumer(instrument, this);
            Hub._marketDataFeed.AddTradeConsumer(instrument, this);
        }

        public void LoadLayoutXML(string layoutXML)
        {
            using (XmlReader reader = XmlReader.Create(new StringReader(layoutXML)))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "WatchListForm":
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
                            case "WatchListGrid":
                                watchListGrid1.LoadLayoutXML(reader.ReadSubtree());
                                break;
                            case "SubscribedInstrument":
                                if (reader.Read())
                                {
                                    AddSymbolSubscription(Utilities.OldOMSStringToInstrArray(reader.Value));
                                }
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
                        xw.WriteStartElement("WatchListForm");
                        xw.WriteAttributeString("Location", this.DesktopLocation.X + "," + this.DesktopLocation.Y);
                        xw.WriteAttributeString("Size", this.Size.Width + "," + this.Size.Height);
                        
                        xw.WriteStartElement("SubscribedInstruments");
                        foreach (InstrInfo[] inst in subscribedInstruemnts)
                        {
                            xw.WriteElementString("SubscribedInstrument", Utilities.InstrArrayToOldOMSString(inst));
                        }
                        xw.WriteEndElement();

                        xw.WriteRaw(watchListGrid1.GetLayoutXML());

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

        private void buttonAddSymbol_Click(object sender, EventArgs e)
        {
            SymbolEntryWindow picker = new SymbolEntryWindow(false);
            picker.Show();
            picker.OnEntryComplete += (InstrInfo inst) => AddSymbolSubscription(new InstrInfo[] { inst });
        }
    }
}