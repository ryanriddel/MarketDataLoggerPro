using DevExpress.Data;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using static FastOMS.OrderInfo;

namespace FastOMS.UI
{
    public partial class OrderManagementGrid : DevExpress.XtraEditors.XtraUserControl
    {
        public enum OrderFilter
        {
            All,
            Open,
            Cancelled,
            Executed,
            Rejected
        }

        public event OnOrderCancelHandler OnOrderCancel;
        public delegate void OnOrderCancelHandler(ulong ordID);

        private ConcurrentDictionary<ulong, OrderTableObj> _orderDict = new ConcurrentDictionary<ulong, OrderTableObj>();
        private BindingList<OrderTableObj> _orderList = new BindingList<OrderTableObj>();
        private RealTimeSource realTimeSource1;

        private OrderFilter filter;

        public OrderManagementGrid()
        {
            InitializeComponent();
            SetupGridView();

            //do something here to get all previous orders from OrderManager? Or have them passed in from another method
        }

        public void SetOrderStatusFilter(OrderFilter filter)
        {
            this.filter = filter;
            gridView1.Columns["LastExecTS"].Visible = (filter == OrderFilter.Executed);
            gridView1.Columns["Error"].Visible = (filter == OrderFilter.Rejected);
            if (filter == OrderFilter.Rejected)
            {
                gridView1.Columns["Error"].SortOrder = ColumnSortOrder.Descending;
            }
            
        }

        private void SetupGridView()
        {
            realTimeSource1 = new RealTimeSource();
            realTimeSource1.DataSource = _orderList;
            realTimeSource1.UseWeakEventHandler = true;
            gridControl1.DataSource = realTimeSource1;

            gridView1.Columns.Clear();
            GridColumn fillTSCol = new GridColumn() { Caption = "LastExecTS", Visible = false, FieldName = "LastExecTS" };
            fillTSCol.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            fillTSCol.DisplayFormat.FormatString = "HH:mm:ss:fff";
            gridView1.Columns.Add(fillTSCol);

            GridColumn orderIDCol = new GridColumn() { Caption = "OrderID", Visible = false, FieldName = "orderID" };
            gridView1.Columns.Add(orderIDCol);
            orderIDCol.SortOrder = ColumnSortOrder.Descending;

            GridColumn symCol = new GridColumn() { Caption = "Sym", Visible = true, FieldName = "Symbol" };
            gridView1.Columns.Add(symCol);

            GridColumn exchCol = new GridColumn() { Caption = "Exch", Visible = true, FieldName = "exchange" };
            gridView1.Columns.Add(exchCol);

            GridColumn sideCol = new GridColumn() { Caption = "Side", Visible = true, FieldName = "side" };
            gridView1.Columns.Add(sideCol);

            GridColumn statusCol = new GridColumn() { Caption = "Status", Visible = true, FieldName = "status" };
            gridView1.Columns.Add(statusCol);

            GridColumn qtyCol = new GridColumn() { Caption = "OrderQty", Visible = true, FieldName = "qty" };
            gridView1.Columns.Add(qtyCol);

            GridColumn lastQtyCol = new GridColumn() { Caption = "LastQty", Visible = true, FieldName = "lastQtyExecuted" };
            gridView1.Columns.Add(lastQtyCol);

            GridColumn remQtyCol = new GridColumn() { Caption = "RemainingQty", Visible = true, FieldName = "remainingQuantity" };
            gridView1.Columns.Add(remQtyCol);

            GridColumn fillQtyCol = new GridColumn() { Caption = "FillQty", Visible = true, FieldName = "fillQuantity" };
            gridView1.Columns.Add(fillQtyCol);

            GridColumn priceCol = new GridColumn() { Caption = "Price", Visible = true, FieldName = "prc" };
            gridView1.Columns.Add(priceCol);

            GridColumn tifCol = new GridColumn() { Caption = "TiF", Visible = true, FieldName = "tif" };
            gridView1.Columns.Add(tifCol);

            GridColumn typeCol = new GridColumn() { Caption = "Type", Visible = true, FieldName = "type" };
            gridView1.Columns.Add(typeCol);

            GridColumn errorCol = new GridColumn() { Caption = "Error", Visible = true, FieldName = "Error" };
            gridView1.Columns.Add(errorCol);
        }

        public void OnNewOrderAccepted(OrderInfo order, Guid ownerControlGuid)
        {
            OrderTableObj obj = new OrderTableObj(order);
            if (_orderDict.TryAdd(order.orderID, obj))
            {
                _orderList.Add(obj);
            }
        }

        public void OnOrderStatusChanged(OrderInfo order, Guid ownerControlGuid)
        {
            if (StatusMatchesFilter(order.status))
            {
                if (filter == OrderFilter.Executed)
                {
                    _orderList.Add(new OrderTableObj(order));
                }
                else
                {
                    if (_orderDict.ContainsKey(order.orderID))
                    {
                        _orderDict[order.orderID].UpdateOrder(order);
                    }
                    else
                    {
                        OrderTableObj obj = new OrderTableObj(order);
                        _orderDict.TryAdd(order.orderID, obj);
                        _orderList.Add(obj);
                    }
                }
            }
            else
            {
                OrderTableObj remove;
                if (_orderDict.TryRemove(order.orderID, out remove))
                {
                    _orderList.Remove(remove);
                }
            }
        }

        private bool StatusMatchesFilter(OrderStatus status)
        {
            return ((filter == OrderFilter.All) ||
                    (filter == OrderFilter.Open && (status == OrderStatus.New || status == OrderStatus.PendingCancel || status == OrderStatus.Partial)) ||
                    (filter == OrderFilter.Executed && (status == OrderStatus.Filled || status == OrderStatus.Partial)) ||
                    (filter == OrderFilter.Cancelled && (status == OrderStatus.Cancelled)) ||
                    (filter == OrderFilter.Rejected && (status == OrderStatus.Rejected)));
        }

        public void LoadLayoutXML(XmlReader reader)
        {
            using (reader)
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        if (reader.Name.StartsWith("OrderManagementGrid"))
                        {
                            if (reader["ActiveFilterString"] != null)
                            {
                                gridView1.ActiveFilterString = reader["ActiveFilterString"];
                            }
                            if (reader["ActiveFilterEnabled"] != null)
                            {
                                gridView1.ActiveFilterEnabled = reader["ActiveFilterEnabled"].Equals("True");
                            }
                        }
                        else
                        {
                            GridColumn col = gridView1.Columns[reader.Name];
                            if (col != null)
                            {
                                if (reader["Visible"] != null) { col.Visible = (reader["Visible"]).Equals("True"); }
                                if (reader["VisibleIndex"] != null) { col.VisibleIndex = (Convert.ToInt32(reader["VisibleIndex"])); }
                                if (reader["Width"] != null) { col.Width = (Convert.ToInt32(reader["Width"])); }
                                if (reader["SortOrder"] != null) { col.SortOrder = (ColumnSortOrder)(Enum.Parse(typeof(ColumnSortOrder), (reader["SortOrder"]))); }
                                if (reader["SortIndex"] != null) { col.SortIndex = (Convert.ToInt32(reader["SortIndex"])); }
                            }
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
                    xw.WriteStartElement("OrderManagementGrid" + filter.ToString());
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

        private class OrderTableObj : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            public OrderInfo OrderInfo;

            public ulong orderID { get; set; }
            //public InstrInfo[] instruments { get; set; }
            private float _prc;
            public float prc { get { return _prc; } set { if (_prc != value) { _prc = value; NotifyPropertyChanged("prc"); } } }
            private uint _qty;
            public uint qty { get { return _qty; } set { if (_qty != value) { _qty = value; NotifyPropertyChanged("qty"); } } }
            private uint _lastQtyExecuted;
            public uint lastQtyExecuted { get { return _lastQtyExecuted; } set { if (_lastQtyExecuted != value) { _lastQtyExecuted = value; NotifyPropertyChanged("lastQtyExecuted"); } } }
            private uint _remainingQuantity;
            public uint remainingQuantity { get { return _remainingQuantity; } set { if (_remainingQuantity != value) { _remainingQuantity = value; NotifyPropertyChanged("remainingQuantity"); } } }
            public string exchange { get; set; } = "";
            public ESide side { get; set; }
            public ETIF tif { get; set; }
            public EType type { get; set; }
            public bool isClosingOrder { get; set; }
            public bool isContra { get; set; }
            private OrderStatus _status;
            public OrderStatus status { get { return _status; } set { if (_status != value) { _status = value; NotifyPropertyChanged("status"); } } }
            private uint _fillQuantity;
            public uint fillQuantity { get { return _fillQuantity; } set { if (_fillQuantity != value) { _fillQuantity = value; NotifyPropertyChanged("fillQuantity"); } } }
            public string Symbol { get; set; }
            private string _Error = "";
            public string Error
            {
                get
                {
                    return _Error;
                }
                set
                {
                    if (_Error == null || !_Error.Equals(value))
                    {
                        _Error = value;
                        NotifyPropertyChanged("Error");
                    }
                }
            }
            private DateTime _LastExecTS;
            public DateTime LastExecTS { get { return _LastExecTS; } set { if (_LastExecTS != value) { _LastExecTS = value; NotifyPropertyChanged("LastExecTS"); } } }

            public OrderTableObj(OrderInfo o)
            {
                OrderInfo = o;
                UpdateOrder(o);
            }

            public void UpdateOrder(OrderInfo o)
            {
                OrderInfo = o;
                orderID = o.orderID;
                //instruments = o.instruments;
                prc = o.prc;
                qty = o.qty;
                lastQtyExecuted = o.lastQtyExecuted;
                remainingQuantity = o.remainingQuantity;
                exchange = o.exchange;
                side = o.side;
                tif = o.tif;
                type = o.type;
                isClosingOrder = o.isClosingOrder;
                isContra = o.isContra;
                status = o.status;
                fillQuantity = o.fillQuantity;
                Symbol = o.Symbol;
                Error = o.Error;
                LastExecTS = o.LastExecTS;
            }

            void NotifyPropertyChanged(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            OrderTableObj selected = _orderList[gridView1.GetDataSourceRowIndex(e.RowHandle)];
            if (e.Column.Caption.Equals("Side"))
            {
                switch (selected.side)
                {
                    case OrderInfo.ESide.BUY:
                        e.Appearance.ForeColor = System.Drawing.Color.Blue;
                        break;
                    case OrderInfo.ESide.SELL:
                        e.Appearance.ForeColor = System.Drawing.Color.Red;
                        break;
                }
            }
            else if (e.Column.Caption.Equals("Status"))
            {
                switch (selected.status)
                {
                    case OrderStatus.Cancelled:
                        e.Appearance.ForeColor = System.Drawing.Color.Gray;
                        break;
                    case OrderStatus.Filled:
                        e.Appearance.ForeColor = System.Drawing.Color.Green;
                        break;
                    case OrderStatus.Partial:
                        e.Appearance.ForeColor = System.Drawing.Color.Blue;
                        break;
                    case OrderStatus.Rejected:
                        e.Appearance.ForeColor = System.Drawing.Color.Red;
                        break;
                }
            }
        }

        private void gridControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && filter != OrderFilter.Executed)
            {
                int selRows = gridView1.SelectedRowsCount;
                if (selRows > 0)
                {
                    int[] selectRows = gridView1.GetSelectedRows();
                    List<ulong> cancelableOrderIDs = new List<ulong>();
                    foreach (int rowID in selectRows)
                    {
                        OrderTableObj selected = _orderList[gridView1.GetDataSourceRowIndex(rowID)];
                        if (selected.status == OrderStatus.New || selected.status == OrderStatus.Partial)
                        {
                            cancelableOrderIDs.Add(selected.orderID);
                        }
                    }
                    if (cancelableOrderIDs.Count > 0)
                    {
                        DialogResult result = MessageBox.Show("Cancel " + cancelableOrderIDs.Count + " Orders?", "", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            foreach (ulong orderID in cancelableOrderIDs)
                            {
                                OnOrderCancel?.Invoke(orderID);
                            }
                        }
                    }
                }
            }
        }
    }
}
