using MktSrvcAPI;
using System;
using FastOMS.Data_Structures;
using System.IO;
using System.Xml;

namespace FastOMS.UI
{
    public partial class Level2Canvas : DevExpress.XtraEditors.XtraUserControl, IMarketDataConsumer<QuoteBook>
    {
        public event OnGridRowSelected OnRowSelected;
        public delegate void OnGridRowSelected(string dest, float price, uint size);

        public event OnOrderCancelHandler OnOrderCancel;
        public delegate void OnOrderCancelHandler(ulong ordID);

        private bool _ColumnHeadersVisible = true;
        public bool ColumnHeadersVisible
        {
            get { return _ColumnHeadersVisible; }
            set
            {
                _ColumnHeadersVisible = value;
                level2GridBid.SetColumnHeadersVisible(value);
                level2GridAsk.SetColumnHeadersVisible(value);
            }
        }

        public InstrInfo[] _inst { get; set; }

        string _controlID = "";
        bool _perfEnabled = false;

        public Level2Canvas()
        {
            InitializeComponent();
            
            level2GridBid.OnRowClicked += onGridRowSelected;
            level2GridAsk.OnRowClicked += onGridRowSelected;

            level2GridBid.OnOrderCancel += OnOrdCancel;
            level2GridAsk.OnOrderCancel += OnOrdCancel;
        }

        public void setUpPerfAnalyzer(string controlID)
        {
            level2GridBid.setUpPerfAnalysis(controlID + "BID");
            level2GridAsk.setUpPerfAnalysis(controlID + "ASK");
            _controlID = controlID;
            PerfAnalyzer.InitializeActionAnalyzer(controlID + "_FromBufferAddToCanvasHandlerFired");
            _perfEnabled = true;
        }

        public void AddUpdateOrder(ulong orderID, OrderInfo order)
        {
            if (order.side == OrderInfo.ESide.BUY)
            {
                level2GridBid.AddUpdateOrder(orderID, order);
            }
            else if (order.side == OrderInfo.ESide.SELL)
            {
                level2GridAsk.AddUpdateOrder(orderID, order);
            }
        }

        public void NewDataHandler(QuoteBook book)
        {
            if(_perfEnabled) PerfAnalyzer.addTime(DateTime.Now.Ticks - book.ENTERED_BUFFER_TICKS, _controlID + "_FromBufferAddToCanvasHandlerFired");
            level2GridBid.NewDataHandler(book);
            level2GridAsk.NewDataHandler(book);
        }

        public void ImpliedMarketHandler(QuoteBook book)
        {
            level2GridBid.ImpliedMarketHandler(book);
            level2GridAsk.ImpliedMarketHandler(book);
        }

        public void LoadLayoutXML(XmlReader reader)
        {
            using (reader)
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "Level2Canvas":
                                if (reader["ColumnHeadersVisible"] != null)
                                {
                                    ColumnHeadersVisible = reader["ColumnHeadersVisible"].Equals("True");
                                }
                                break;
                            case "Level2GridBid":
                                level2GridBid.LoadLayoutXML(reader.ReadSubtree());
                                break;
                            case "Level2GridAsk":
                                level2GridAsk.LoadLayoutXML(reader.ReadSubtree());
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
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.ConformanceLevel = ConformanceLevel.Fragment;
                settings.OmitXmlDeclaration = true;
                using (XmlWriter xw = XmlWriter.Create(sw, settings))
                {
                    xw.WriteStartElement("Level2Canvas");
                    xw.WriteAttributeString("ColumnHeadersVisible", "" + ColumnHeadersVisible);

                    xw.WriteRaw(level2GridBid.GetLayoutXML());
                    xw.WriteRaw(level2GridAsk.GetLayoutXML());

                    xw.WriteEndElement();
                }
                return sw.ToString();
            }
        }

        private void onGridRowSelected(string dest, float price, uint size)
        {
            OnRowSelected?.Invoke(dest, price, size);
        }

        private void OnOrdCancel(ulong orderID)
        {
            OnOrderCancel?.Invoke(orderID);
        }
    }
}
