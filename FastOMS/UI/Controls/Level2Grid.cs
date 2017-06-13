using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Collections.Concurrent;
using MktSrvcAPI;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using FastOMS.Utility;
using FastOMS.Data_Structures;
using DevExpress.Data;
using System.IO;
using System.Xml;

namespace FastOMS.UI
{
    public enum Level2GridType
    {
        Bid,
        Ask
    }

    public partial class Level2Grid : DevExpress.XtraEditors.XtraUserControl, IMarketDataConsumer<QuoteBook>
    {
        public event OnRowClickEventHandler OnRowClicked;
        public delegate void OnRowClickEventHandler(string dest, float price, uint size);

        public event OnOrderCancelHandler OnOrderCancel;
        public delegate void OnOrderCancelHandler(ulong ordID);

        private SortedSet<float> _priceLevels = new SortedSet<float>();
        private float[] _sortedPriceLvls = new float[0];
        private readonly Color[] _priceLevelBGColors = { Color.FromArgb(255, 255, 96), Color.FromArgb(96, 255, 96), Color.FromArgb(128, 255, 255),
                                                         Color.FromArgb(255, 96, 96), Color.FromArgb(128, 128, 255), Color.FromArgb(255, 96, 255) };

        private QuoteBook _lastQuoteBook;
        private volatile bool _quoteBookReceived = false;
        private QuoteBook _impliedQuoteBook;
        private volatile bool _impliedBookReceived = false;
        //private QuoteBook _mergedQuoteBook = new QuoteBook();
        private ConcurrentDictionary<byte, Lvl2TableObj> _exchBookDict = new ConcurrentDictionary<byte, Lvl2TableObj>(); //for exchange order books
        private ConcurrentDictionary<ulong, Lvl2TableObj> _orderDict = new ConcurrentDictionary<ulong, Lvl2TableObj>(); //for our user orders
        private BindingList<Lvl2TableObj> _bindingList = new BindingList<Lvl2TableObj>();
        private RealTimeSource realTimeSource1;
        
        private System.Windows.Forms.Timer _gridTimer;
        private bool _buy = true;

        private GridHitInfo _mouseDownHit;
        private ulong _mouseDownOrderID;
        private GridHitInfo _mouseUpHit;

        private string _perfLabel = "";
        private bool _perfEnabled = false;

        public Level2Grid(Level2GridType type)
        {
            _buy = type == Level2GridType.Bid;
            InitializeComponent();
            SetupGridView();
            //InitializeTimer();
        }

        public void setUpPerfAnalysis(string controlID)
        {
            PerfAnalyzer.InitializeActionAnalyzer(controlID + "_QuoteArriveDelay");
            PerfAnalyzer.InitializeActionAnalyzer(controlID + "_GridDrawDuration");
            PerfAnalyzer.InitializeActionAnalyzer(controlID + "_SetAllOrdersTime");
            PerfAnalyzer.InitializeActionAnalyzer(controlID + "_RowColoringTime");
            _perfLabel = controlID;
            _perfEnabled = true;
        }

        private void SetupGridView()
        {
            realTimeSource1 = new RealTimeSource();
            realTimeSource1.DataSource = _bindingList;
            realTimeSource1.DisplayableProperties = "Exch;Price;Size;";
            realTimeSource1.UseWeakEventHandler = true;
            gridControl1.DataSource = realTimeSource1;

            gridView1.Columns.Clear();
            GridColumn exchCol = new GridColumn() { Caption = "Exch", Visible = true, FieldName = "Exch" };
            gridView1.Columns.Add(exchCol);

            GridColumn priceCol = new GridColumn() { Caption = "Price", Visible = true, FieldName = "Price" };
            priceCol.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            priceCol.DisplayFormat.FormatString = "f2";
            gridView1.Columns.Add(priceCol);
            priceCol.SortOrder = _buy ? ColumnSortOrder.Descending : ColumnSortOrder.Ascending;
            priceCol.SortIndex = 0;

            GridColumn sizeCol = new GridColumn() { Caption = "Size", Visible = true, FieldName = "Size" };
            gridView1.Columns.Add(sizeCol);
            sizeCol.SortOrder = ColumnSortOrder.Descending;
            sizeCol.SortIndex = 1;
        }

        public delegate void InvokeDelegate();
        public void NewDataHandler(QuoteBook book)
        {
            _lastQuoteBook = book;
            _quoteBookReceived = true;
            if (InvokeRequired)
            {
                BeginInvoke(new InvokeDelegate(RepopulateGrid));
            }
        }
        
        public void ImpliedMarketHandler(QuoteBook book)
        {
            _impliedQuoteBook = book;
            _impliedBookReceived = true;
            if (InvokeRequired)
            {
                BeginInvoke(new InvokeDelegate(RepopulateGrid));
            }
        }

        private QuoteBookStruct mergeQuoteBooks()
        {
            QuoteBookStruct _mergedQuoteBook = new QuoteBookStruct();
            
            if (_quoteBookReceived && !_impliedBookReceived)
            {
                lock (_lastQuoteBook)
                {
                    _mergedQuoteBook = new QuoteBookStruct(_lastQuoteBook);
                }
            }
            else if (!_quoteBookReceived && _impliedBookReceived)
            {
                lock (_impliedQuoteBook)
                {
                    _mergedQuoteBook = new QuoteBookStruct(_impliedQuoteBook);
                }
            }
            else if (_quoteBookReceived && _impliedBookReceived)
            {
                QuoteBookStruct lastQuoteStruct;
                QuoteBookStruct lastImpliedStruct;
                lock (_lastQuoteBook)
                {
                    lastQuoteStruct = new QuoteBookStruct(_lastQuoteBook);
                }
                lock (_impliedQuoteBook)
                {
                    lastImpliedStruct = new QuoteBookStruct(_impliedQuoteBook);
                }

                _mergedQuoteBook = new QuoteBookStruct();
                _mergedQuoteBook.Instr = lastQuoteStruct.Instr;
                _mergedQuoteBook.TS = lastQuoteStruct.TS;
                _mergedQuoteBook.PartID = lastQuoteStruct.PartID;
                _mergedQuoteBook.Mod = lastQuoteStruct.Mod;

                if (lastImpliedStruct.NumBid > 0)
                {
                    _mergedQuoteBook.BidExch = lastQuoteStruct.BidExch == null ? lastImpliedStruct.BidExch : lastQuoteStruct.BidExch.Concat(lastImpliedStruct.BidExch).ToArray();
                    _mergedQuoteBook.BidBk = lastQuoteStruct.BidBk == null ? lastImpliedStruct.BidBk : lastQuoteStruct.BidBk.Concat(lastImpliedStruct.BidBk).ToArray();
                    _mergedQuoteBook.NumBid = (byte)(lastQuoteStruct.NumBid + lastImpliedStruct.NumBid);
                }
                if (lastImpliedStruct.NumAsk > 0)
                {
                    _mergedQuoteBook.AskExch = lastQuoteStruct.AskExch == null ? lastImpliedStruct.AskExch : lastQuoteStruct.AskExch.Concat(lastImpliedStruct.AskExch).ToArray();
                    _mergedQuoteBook.AskBk = lastQuoteStruct.AskBk == null ? lastImpliedStruct.AskBk : lastQuoteStruct.AskBk.Concat(lastImpliedStruct.AskBk).ToArray();
                    _mergedQuoteBook.NumAsk = (byte)(lastQuoteStruct.NumAsk + lastImpliedStruct.NumAsk);
                }
            }
            return _mergedQuoteBook;
        }

        public void AddUpdateOrder(ulong orderID, OrderInfo order)
        {
            if (order.status == OrderStatus.Filled || order.status == OrderStatus.Cancelled || order.status == OrderStatus.Rejected)
            {
                Lvl2TableObj oldOrd;
                if (_orderDict.TryRemove(orderID, out oldOrd))
                {
                    lock (_bindingList)
                    {
                        _bindingList.Remove(oldOrd);
                    }
                }
            }
            else if (_orderDict.ContainsKey(orderID))
            {
                _orderDict[orderID].UpdateOrder(order);
            }
            else
            {
                Lvl2TableObj obj = new Lvl2TableObj(order, orderID);
                _orderDict.TryAdd(orderID, obj);
                lock (_bindingList)
                {
                    _bindingList.Add(obj);
                }
            }
        }
        
        private List<OrderBook> ConvertQuoteBookToOrders(QuoteBookStruct book)
        {
            List<OrderBook> bookList = new List<OrderBook>();
            int bookLength = _buy ? book.NumBid : book.NumAsk;
            for (int i = 0; i < bookLength; i++)
            {
                OrderBook ob = new OrderBook();
                ob._exch = _buy ? book.BidExch[i] : book.AskExch[i];
                ob._instr = (InstrInfo[])book.Instr.Clone();
                ob._ordID = book.PartID;
                ob._ordprc = _buy ? book.BidBk[i].prc : book.AskBk[i].prc;
                ob._ordsz = _buy ? book.BidBk[i].sz : book.AskBk[i].sz;
                bookList.Add(ob);
            }
            return bookList;
        }

        private void InitializeTimer()
        {
            _gridTimer = new System.Windows.Forms.Timer();
            _gridTimer.Interval = 100;
            _gridTimer.Tick += _gridTimer_Elapsed;
            _gridTimer.Enabled = true;
        }

        private void _gridTimer_Elapsed(object sender, EventArgs e)
        {
            RepopulateGrid();
        }

        private void RepopulateGrid()
        {
            QuoteBookStruct book = mergeQuoteBooks();

            //if the book is empty, remove all exchange quotes (can't just clear list or our orders will be removes as well)
            if ((_buy && book.NumBid == 0 || (!_buy && book.NumAsk == 0)))
            {
                foreach (byte key in _exchBookDict.Keys)
                {
                    Lvl2TableObj obj;
                    _exchBookDict.TryRemove(key, out obj);
                    lock (_bindingList)
                    {
                        _bindingList.Remove(obj);
                    }
                }
            }
            else
            {
                //if any exchange has a quote in our dictionary, but not the latest quotebook, it doesn't exist anymore, so remove it
                foreach (byte key in _exchBookDict.Keys)
                {
                    if ((_buy && !book.BidExch.Contains(key)) ||
                        (!_buy && !book.AskExch.Contains(key)))
                    {
                        Lvl2TableObj obj;
                        _exchBookDict.TryRemove(key, out obj);
                        lock (_bindingList)
                        {
                            _bindingList.Remove(obj);
                        }
                    }
                }
            }

            if (_perfEnabled) { PerfAnalyzer.startTime(_perfLabel + "_SetAllOrdersTime"); }
            _priceLevels.Clear();
            List<OrderBook> books = ConvertQuoteBookToOrders(book);
            foreach (OrderBook b in books)
            {
                _priceLevels.Add(b._ordprc);
                if (_exchBookDict.ContainsKey(b._exch))
                {
                    _exchBookDict[b._exch].UpdateQuote(b);
                }
                else
                {
                    Lvl2TableObj obj = new Lvl2TableObj(b);
                    _exchBookDict.TryAdd(b._exch, obj);
                    lock (_bindingList)
                    {
                        _bindingList.Add(obj);
                    }
                }
            }
            _sortedPriceLvls = _buy ? _priceLevels.Reverse().ToArray() : _priceLevels.ToArray();
            if (_perfEnabled) { PerfAnalyzer.endTime(_perfLabel + "_SetAllOrdersTime"); }
        }

        public void SetColumnHeadersVisible(bool visible)
        {
            gridView1.OptionsView.ShowColumnHeaders = visible;
        }

        public void LoadLayoutXML(XmlReader reader)
        {
            using (reader)
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        GridColumn col = gridView1.Columns[reader.Name];
                        if (col != null)
                        {
                            if (reader["Visible"] != null) { col.Visible = (reader["Visible"]).Equals("True"); }
                            if (reader["VisibleIndex"] != null) { col.VisibleIndex = (Convert.ToInt32(reader["VisibleIndex"])); }
                            if (reader["Width"] != null) { col.Width = (Convert.ToInt32(reader["Width"])); }
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
                    xw.WriteStartElement("Level2Grid" + (_buy ? "Bid" : "Ask"));

                    foreach (GridColumn col in gridView1.Columns)
                    {
                        xw.WriteStartElement(col.Caption);
                        xw.WriteAttributeString("Visible", "" + col.Visible);
                        xw.WriteAttributeString("VisibleIndex", "" + col.VisibleIndex);
                        xw.WriteAttributeString("Width", "" + col.Width);
                        xw.WriteEndElement();
                    }
                    xw.WriteEndElement();
                }
                return sw.ToString();
            }
        }

        private class Lvl2TableObj : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            public byte ExchByte { get; set; }
            private string _Exch;
            public string Exch { get { return _Exch; } set { if (_Exch != value) { _Exch = value; NotifyPropertyChanged("Exch"); } } }
            private float _Price;
            public float Price { get { return _Price; } set { if (_Price != value) { _Price = value; NotifyPropertyChanged("Price"); } } }
            private uint _Size;
            public uint Size { get { return _Size; } set { if (_Size != value) { _Size = value; NotifyPropertyChanged("Size"); } } }
            public bool IsOurOrder { get; set; }
            public ulong OrderID { get; set; }

            public Lvl2TableObj(OrderBook q)
            {
                ExchByte = q._exch;
                if (q._instr[0].type == InstrInfo.EType.OPTION)
                {
                    Exch = ExchangeDictionary.GetOptExchange(ExchByte);
                }
                if (q._instr[0].type == InstrInfo.EType.EQUITY)
                {
                    Exch = ExchangeDictionary.GetEqExchange(ExchByte);
                }
                UpdateQuote(q);
                IsOurOrder = false;
            }

            public Lvl2TableObj(OrderInfo o, ulong oID)
            {
                ExchByte = Convert.ToByte(Utilities.ExchangeStringToByte(o.exchange));
                Exch = o.exchange;
                UpdateOrder(o);
                IsOurOrder = true;
                OrderID = oID;
            }

            public void UpdateQuote(OrderBook o)
            {
                Price = o._ordprc;
                Size = o._ordsz;
            }

            public void UpdateOrder(OrderInfo o)
            {
                Price = o.prc;
                Size = o.remainingQuantity;
            }

            void NotifyPropertyChanged(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (_perfEnabled) { PerfAnalyzer.startTime(_perfLabel + "_RowColoringTime"); }
            GridView view = sender as GridView;

            Lvl2TableObj cellObject = null;
            lock (_bindingList)
            {
                if (_bindingList.Count > gridView1.GetDataSourceRowIndex(e.RowHandle))
                {
                    cellObject = _bindingList[gridView1.GetDataSourceRowIndex(e.RowHandle)];
                }
            }
            if (cellObject != null)
            {
                if (cellObject.IsOurOrder)
                {
                    if (e.Column.Caption.Equals("Exch"))
                    {
                        e.Appearance.BackColor = Color.Red;
                        e.Appearance.ForeColor = Color.White;
                    }
                    else
                    {
                        e.Appearance.BackColor = Color.Black;
                        e.Appearance.ForeColor = Color.White;
                    }
                }
                else
                {
                    float px = cellObject.Price;
                    for (int j = 0; j < _sortedPriceLvls.Length; j++)
                    {
                        if (_sortedPriceLvls.ElementAt(j) == px)
                        {
                            e.Appearance.BackColor = _priceLevelBGColors[j % 6];
                        }
                    }
                }
                if (_perfEnabled) { PerfAnalyzer.endTime(_perfLabel + "_RowColoringTime"); }
            }
        }

        private void gridView1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                _mouseDownOrderID = 0;
                _mouseDownHit = gridView1.CalcHitInfo(new Point(e.X, e.Y));
                if (_mouseDownHit.InDataRow)
                {
                    Lvl2TableObj selected = _bindingList[gridView1.GetDataSourceRowIndex(_mouseDownHit.RowHandle)];
                    if (_mouseDownHit.Column.Caption.Equals("Exch") && (selected.IsOurOrder))
                    {
                        _mouseDownOrderID = selected.OrderID;
                    }
                    OnRowClicked?.Invoke(selected.Exch, selected.Price, selected.Size);
                }
            }
        }

        private void gridView1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                _mouseUpHit = gridView1.CalcHitInfo(new Point(e.X, e.Y));
                if (_mouseUpHit.InDataRow)
                {
                    Lvl2TableObj selected = _bindingList[gridView1.GetDataSourceRowIndex(_mouseUpHit.RowHandle)];
                    if (_mouseUpHit.Column.Caption.Equals("Exch") && selected.IsOurOrder && selected.OrderID == _mouseDownOrderID)
                    {
                        OnOrderCancel?.Invoke(selected.OrderID);
                    }
                }
            }
        }
    }
}
