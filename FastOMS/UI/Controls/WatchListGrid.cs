using DevExpress.Data;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using FastOMS.Data_Structures;
using MktSrvcAPI;
using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.IO;
using System.Xml;

namespace FastOMS.UI
{
    public partial class WatchListGrid : DevExpress.XtraEditors.XtraUserControl, IMarketDataConsumer<QuoteBook>, IMarketDataConsumer<TradeInfo>
    {
        ConcurrentDictionary<string, WatchListTableObj> _watchListDict = new ConcurrentDictionary<string, WatchListTableObj>();
        BindingList<WatchListTableObj> _watchListBinding = new BindingList<WatchListTableObj>();
        RealTimeSource realTimeSource1 = new RealTimeSource();

        public WatchListGrid()
        {
            InitializeComponent();
            SetupGridView();
        }

        private void SetupGridView()
        {
            realTimeSource1 = new RealTimeSource();
            realTimeSource1.DataSource = _watchListBinding;
            realTimeSource1.UseWeakEventHandler = true;
            gridControl1.DataSource = realTimeSource1;

            gridView1.Columns.Clear();
            GridColumn symCol = new GridColumn() { Caption = "Sym", Visible = true, FieldName = "Sym" };
            gridView1.Columns.Add(symCol);

            GridColumn bidCol = new GridColumn() { Caption = "Bid", Visible = true, FieldName = "Bid" };
            bidCol.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            bidCol.DisplayFormat.FormatString = "f2";
            gridView1.Columns.Add(bidCol);

            GridColumn bidSzCol = new GridColumn() { Caption = "BidSz", Visible = true, FieldName = "BidSz" };
            gridView1.Columns.Add(bidSzCol);

            GridColumn askCol = new GridColumn() { Caption = "Ask", Visible = true, FieldName = "Ask" };
            askCol.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            askCol.DisplayFormat.FormatString = "f2";
            gridView1.Columns.Add(askCol);

            GridColumn askSzCol = new GridColumn() { Caption = "AskSz", Visible = true, FieldName = "AskSz" };
            gridView1.Columns.Add(askSzCol);

            GridColumn lastCol = new GridColumn() { Caption = "Last", Visible = true, FieldName = "Last" };
            lastCol.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            lastCol.DisplayFormat.FormatString = "f2";
            gridView1.Columns.Add(lastCol);
        }

        public void NewDataHandler(QuoteBook book)
        {
            string instrString = Utilities.InstrToStr(book.Instr);
            if (_watchListDict.ContainsKey(instrString))
            {
                _watchListDict[instrString].UpdateQuoteBook(book);
            }
            else
            {
                lock (_watchListDict)
                {
                    if (_watchListDict.ContainsKey(instrString))
                    {
                        _watchListDict[instrString].UpdateQuoteBook(book);
                    }
                    else
                    {
                        WatchListTableObj obj = new WatchListTableObj(book);
                        _watchListDict.TryAdd(instrString, obj);
                        _watchListBinding.Add(obj);
                    }
                }
            }
        }

        public void NewDataHandler(TradeInfo trade)
        {
            string instrString = Utilities.InstrToStr(trade.Instr);
            if (_watchListDict.ContainsKey(instrString))
            {
                _watchListDict[instrString].UpdateTradeInfo(trade);
            }
            else
            {
                lock (_watchListDict)
                {
                    if (_watchListDict.ContainsKey(instrString))
                    {
                        _watchListDict[instrString].UpdateTradeInfo(trade);
                    }
                    else
                    {
                        WatchListTableObj obj = new WatchListTableObj(trade);
                        _watchListDict.TryAdd(instrString, obj);
                        _watchListBinding.Add(obj);
                    }
                }
            }
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
                            case "WatchListGrid":
                                if (reader["ActiveFilterString"] != null)
                                {
                                    gridView1.ActiveFilterString = reader["ActiveFilterString"];
                                }
                                if (reader["ActiveFilterEnabled"] != null)
                                {
                                    gridView1.ActiveFilterEnabled = reader["ActiveFilterEnabled"].Equals("True");
                                }
                                break;
                            default:
                                GridColumn col = gridView1.Columns[reader.Name];
                                if (col != null)
                                {
                                    if (reader["Visible"] != null) { col.Visible = (reader["Visible"]).Equals("True"); }
                                    if (reader["VisibleIndex"] != null) { col.VisibleIndex = (Convert.ToInt32(reader["VisibleIndex"])); }
                                    if (reader["Width"] != null) { col.Width = (Convert.ToInt32(reader["Width"])); }
                                    if (reader["SortOrder"] != null) { col.SortOrder = (ColumnSortOrder)(Enum.Parse(typeof(ColumnSortOrder), (reader["SortOrder"]))); }
                                    if (reader["SortIndex"] != null) { col.SortIndex = (Convert.ToInt32(reader["SortIndex"])); }
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
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.ConformanceLevel = ConformanceLevel.Fragment;
                settings.OmitXmlDeclaration = true;
                using (XmlWriter xw = XmlWriter.Create(sw, settings))
                {
                    xw.WriteStartElement("WatchListGrid");
                    xw.WriteAttributeString("ActiveFilterString", gridView1.ActiveFilterString);
                    xw.WriteAttributeString("ActiveFilterEnabled", "" + gridView1.ActiveFilterEnabled);

                    foreach (GridColumn col in gridView1.Columns)
                    {
                        xw.WriteStartElement(col.Caption);
                        xw.WriteAttributeString("Visible", "" + col.Visible);
                        xw.WriteAttributeString("VisibleIndex", "" + col.VisibleIndex);
                        xw.WriteAttributeString("Width", "" + col.Width);
                        xw.WriteAttributeString("SortOrder", "" + col.SortOrder.ToString());
                        xw.WriteAttributeString("SortIndex", "" + col.SortIndex);
                        xw.WriteEndElement();
                    }
                    xw.WriteEndElement();
                }
                return sw.ToString();
            }
        }

        private class WatchListTableObj : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            private string _Sym;
            public string Sym { get { return _Sym; } set { if (_Sym != value) { _Sym = value; NotifyPropertyChanged("Sym"); } } }
            private float _Bid;
            public float Bid { get { return _Bid; } set { if (_Bid != value) { _Bid = value; NotifyPropertyChanged("Bid"); } } }
            private uint _BidSz;
            public uint BidSz { get { return _BidSz; } set { if (_BidSz != value) { _BidSz = value; NotifyPropertyChanged("BidSz"); } } }
            private float _Ask;
            public float Ask { get { return _Ask; } set { if (_Ask != value) { _Ask = value; NotifyPropertyChanged("Ask"); } } }
            private uint _AskSz;
            public uint AskSz { get { return _AskSz; } set { if (_AskSz != value) { _AskSz = value; NotifyPropertyChanged("AskSz"); } } }
            private float _Last;
            public float Last { get { return _Last; } set { if (_Last != value) { _Last = value; NotifyPropertyChanged("Last"); } } }
            private uint _LastTS;

            public uint PrevBidSz { get; set; }
            public uint PrevAskSz { get; set; }
            public float PrevBid { get; set; }
            public float PrevAsk { get; set; }
            public float PrevLast { get; set; }

            public WatchListTableObj(QuoteBook q)
            {
                Sym = Utilities.InstrToStr(q.Instr);
                UpdateQuoteBook(q);
            }

            public WatchListTableObj(TradeInfo t)
            {
                Sym = Utilities.InstrToStr(t.Instr);
                UpdateTradeInfo(t);
            }

            public void UpdateQuoteBook(QuoteBook q)
            {
                float tobBid = 0;
                uint tobBidSz = 0;
                float tobAsk = 0;
                uint tobAskSz = 0;
                q.GetTOB(ref tobBid, ref tobBidSz, ref tobAsk, ref tobAskSz);

                PrevBid = Bid;
                PrevBidSz = BidSz;
                PrevAsk = Ask;
                PrevAskSz = AskSz;

                Bid = tobBid;
                BidSz = tobBidSz;
                Ask = tobAsk;
                AskSz = tobAskSz;
            }

            public void UpdateTradeInfo(TradeInfo t)
            {
                if (t.TS > _LastTS)
                {
                    PrevLast = Last;

                    Last = t.Prc;
                    _LastTS = t.TS;
                }
            }

            void NotifyPropertyChanged(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.GetRowCellValue(e.RowHandle, view.Columns["Sym"]) != null)
            {
                WatchListTableObj selected = _watchListBinding[gridView1.GetDataSourceRowIndex(e.RowHandle)];
                if (e.Column.Caption.Equals("BidSz"))
                {
                    if (selected.PrevBidSz != 0 &&  selected.BidSz > selected.PrevBidSz)
                    {
                        e.Appearance.ForeColor = System.Drawing.Color.Green;
                    }
                    else if (selected.PrevBidSz != 0 &&  selected.BidSz < selected.PrevBidSz)
                    {
                        e.Appearance.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else if (e.Column.Caption.Equals("AskSz"))
                {
                    if (selected.PrevAskSz != 0 && selected.AskSz > selected.PrevAskSz)
                    {
                        e.Appearance.ForeColor = System.Drawing.Color.Green;
                    }
                    else if (selected.PrevAskSz != 0 &&  selected.AskSz < selected.PrevAskSz)
                    {
                        e.Appearance.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else if (e.Column.Caption.Equals("Bid"))
                {
                    if (selected.PrevBid != 0 && selected.Bid > selected.PrevBid)
                    {
                        e.Appearance.ForeColor = System.Drawing.Color.Green;
                    }
                    else if (selected.PrevBid != 0 && selected.Bid < selected.PrevBid)
                    {
                        e.Appearance.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else if (e.Column.Caption.Equals("Ask"))
                {
                    if (selected.PrevAsk != 0 && selected.Ask > selected.PrevAsk)
                    {
                        e.Appearance.ForeColor = System.Drawing.Color.Green;
                    }
                    else if (selected.PrevAsk != 0 && selected.Ask < selected.PrevAsk)
                    {
                        e.Appearance.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else if (e.Column.Caption.Equals("Last"))
                {
                    if (selected.PrevLast != 0 && selected.Last > selected.PrevLast)
                    {
                        e.Appearance.ForeColor = System.Drawing.Color.Green;
                    }
                    else if (selected.PrevLast != 0 && selected.Last < selected.PrevLast)
                    {
                        e.Appearance.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }
    }
}
