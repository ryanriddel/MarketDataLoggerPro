using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FastOMS.Data_Structures;
using System.Threading;
using System.IO;
using System.Xml;

namespace FastOMS.UI
{
    public partial class TimeAndSalesControl : DevExpress.XtraEditors.XtraUserControl, IMarketDataConsumer<TradeInfo>
    {
        private System.Windows.Forms.Timer _gridTimer;
        private bool doUpdate = false;

        private ConcurrentQueue<TradeInfo> _tradeQueue = new ConcurrentQueue<TradeInfo>();
        private BindingList<TASTableObj> _tradeList = new BindingList<TASTableObj>();

        private const int MAXHISTORY = 100;

        private float lastPrice = -1;
        
        private Mutex _mutex = new Mutex();

        bool _perfEnabled = false;

        string _performanceLabel = "";

        public Guid ControlID;

        private bool _ColumnHeadersVisible = true;
        public bool ColumnHeadersVisible
        {
            get { return _ColumnHeadersVisible; }
            set
            {
                _ColumnHeadersVisible = value;
                gridView1.OptionsView.ShowColumnHeaders = value;
            }
        }

        public TimeAndSalesControl()
        {
            InitializeComponent();

            SetupGridView();

            InitializeTimer();
            doUpdate = true;
        }

        public void setupPerfAnalyzer(string perfLabel)
        {
            PerfAnalyzer.InitializeActionAnalyzer(perfLabel + "_TradeArriveDelay");
            PerfAnalyzer.InitializeActionAnalyzer(perfLabel + "_FromBufferAddToTASHandlerFired");
            PerfAnalyzer.InitializeActionAnalyzer(perfLabel + "_UpdateValuesDuration");
            PerfAnalyzer.InitializeActionAnalyzer(perfLabel + "_UpdateTextFieldsDuration");
            _performanceLabel = perfLabel;
            _perfEnabled = true;
        }


        private void SetupGridView()
        {
            gridControl1.DataSource = _tradeList;
            _tradeList.RaiseListChangedEvents = false;

            gridView1.Columns.Clear();
            GridColumn timeCol = new GridColumn() { Caption = "Time", Visible = true, FieldName = "Time" };
            timeCol.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            timeCol.DisplayFormat.FormatString = "HH:mm:ss:fff";
            gridView1.Columns.Add(timeCol);
            timeCol.SortOrder = DevExpress.Data.ColumnSortOrder.Descending;

            GridColumn priceCol = new GridColumn() { Caption = "Price", Visible = true, FieldName = "Price" };
            priceCol.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            priceCol.DisplayFormat.FormatString = "f2";
            gridView1.Columns.Add(priceCol);

            GridColumn sizeCol = new GridColumn() { Caption = "Size", Visible = true, FieldName = "Size" };
            gridView1.Columns.Add(sizeCol);

            GridColumn mktCol = new GridColumn() { Caption = "Mkt", Visible = true, FieldName = "Mkt" };
            gridView1.Columns.Add(mktCol);
        }

        public void AddTrade(TradeInfo trade)
        {
            _tradeQueue.Enqueue(trade);
        }

        public void NewDataHandler(TradeInfo trade)
        {
            

                PerfAnalyzer.addTime(DateTime.Now.Ticks - trade.TEST_TIMESTAMP_TICKS, _performanceLabel + "_TradeArriveDelay");
            PerfAnalyzer.addTime(DateTime.Now.Ticks - trade.ENTERED_BUFFER_TICKS, _performanceLabel + "_FromBufferAddToTASHandlerFired");
            AddTrade(trade);
        }

        private void InitializeTimer()
        {
            _gridTimer = new System.Windows.Forms.Timer();
            _gridTimer.Interval = 200;
            _gridTimer.Tick += _gridTimer_Elapsed;

            _gridTimer.Enabled = true;
            
            

        }

        private void _gridTimer_Elapsed(object sender, EventArgs e)
        {
            
            if (doUpdate && !this.IsDisposed)
            {
                this.BeginInvoke(new MethodInvoker(UpdateValues));
            }
        }

        private void UpdateValues()
        {
            if (_perfEnabled) PerfAnalyzer.startTime(_performanceLabel + "_UpdateValuesDuration");

            while (!_tradeQueue.IsEmpty)
            {
                TradeInfo t;
                if (_tradeQueue.TryDequeue(out t))
                {
                    _tradeList.Add(new TASTableObj(t, lastPrice));
                    lastPrice = t.Prc;
                    UpdateTextFields(t);
                }
            }
            while (_tradeList.Count > MAXHISTORY)
            {
                _tradeList.RemoveAt(0);
            }
            PopulateGrid();

            if (_perfEnabled) PerfAnalyzer.endTime(_performanceLabel + "_UpdateValuesDuration");
        }

        private void UpdateTextFields(TradeInfo ti)
        {
            if(_perfEnabled) PerfAnalyzer.startTime(_performanceLabel + "_UpdateTextFieldsDuration");

            textEditVol.Text = ti.TotVol.ToString("##########");
            textEditOpen.Text = ti.Open.ToString("0.00");
            textEditHigh.Text = ti.High.ToString("0.00");
            textEditLow.Text = ti.Low.ToString("0.00");

            double last;
            if (textEditLast.Text == "")
            {
                textEditLast.Text = lastPrice.ToString("0.00");
            }
            else if ((last = Convert.ToDouble(textEditLast.Text)) != lastPrice)
            {
                textEditLast.Text = lastPrice.ToString("0.00");
                textEditLast.BackColor = last > lastPrice ? Color.Red : Color.LightGreen;
            }
            else
            {
                textEditLast.BackColor = DefaultBackColor;
            }

            float change = lastPrice - ti.Open;
            Color dayColor = (change > 0) ? Color.LightGreen : Color.Red;
            textEditChng.Text = change.ToString("0.00");
            textEditPercent.Text = (change / ti.Open).ToString("0.00");

            textEditChng.BackColor = textEditPercent.BackColor = (change == 0 ? DefaultBackColor : dayColor);

            if (_perfEnabled) PerfAnalyzer.endTime(_performanceLabel + "_UpdateTextFieldsDuration");
        }

        private void PopulateGrid()
        {
            if (doUpdate)
            {
                Action action = () =>
                {
                    try
                    {
                        gridControl1.BeginUpdate();
                        gridView1.BeginUpdate();
                        gridView1.BeginDataUpdate();
                        gridView1.RefreshData();
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        gridView1.EndDataUpdate();
                        gridView1.EndUpdate();
                        gridControl1.EndUpdate();
                    }
                };
                gridControl1.BeginInvoke(action);
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
                            case "TimeAndSalesControl":
                                if (reader["ColumnHeadersVisible"] != null)
                                {
                                    ColumnHeadersVisible = reader["ColumnHeadersVisible"].Equals("True");
                                }
                                break;
                            default:
                                GridColumn col = gridView1.Columns[reader.Name];
                                if (col != null)
                                {
                                    if (reader["Visible"] != null) { col.Visible = (reader["Visible"]).Equals("True"); }
                                    if (reader["VisibleIndex"] != null) { col.VisibleIndex = (Convert.ToInt32(reader["VisibleIndex"])); }
                                    if (reader["Width"] != null) { col.Width = (Convert.ToInt32(reader["Width"])); }
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

                    xw.WriteStartElement("TimeAndSalesControl");
                    xw.WriteAttributeString("ColumnHeadersVisible", "" + ColumnHeadersVisible);

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

        private class TASTableObj
        {
            public DateTime Time { get; set; }
            public float Price { get; set; }
            public uint Size { get; set; }
            public char Mkt { get; set; }

            public float LastPrice { get; set; }

            public TASTableObj(TradeInfo i, float lastPrice)
            {
                Time = TS2Str(i.TS);
                Price = i.Prc;
                Size = i.Sz;
                Mkt = Convert.ToChar(i.PartID);
                LastPrice = lastPrice;
            }

            private DateTime TS2Str(uint ts)
            {
                DateTime st = DateTime.Today.Date;
                st = st.AddHours(ts / 10000000);
                ts = ts % 10000000;
                st = st.AddMinutes(ts / 100000);
                ts = ts % 100000;
                st = st.AddSeconds(ts / 1000);
                st = st.AddMilliseconds(ts % 1000);

                return st;
            }
        }

        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            doUpdate = false;
        }

        private void gridView1_MouseUp(object sender, MouseEventArgs e)
        {
            doUpdate = true;
        }

        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.GetRowCellValue(e.RowHandle, view.Columns["Price"]) != null)
            {
                float px = (float)view.GetRowCellValue(e.RowHandle, view.Columns["Price"]);
                float lastPx = ((TASTableObj)gridView1.GetRow(e.RowHandle)).LastPrice;

                if (px > lastPx)
                {
                    e.Appearance.ForeColor = Color.FromArgb(96, 255, 96); //Green
                }
                else if (px < lastPx)
                {
                    e.Appearance.ForeColor = Color.FromArgb(255, 96, 96); //Red
                }
            }
        }
        
    }
}
