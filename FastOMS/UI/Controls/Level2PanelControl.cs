using System;
using System.Collections.Generic;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using FastOMS.Data_Structures;
using System.Threading;
using MktSrvcAPI;
using System.IO;
using System.Xml;
using FastOMS.Preferences;

namespace FastOMS.UI
{
    public partial class Level2PanelControl : DevExpress.XtraEditors.XtraUserControl, 
        IMarketDataConsumer<QuoteBook>, IMarketDataConsumer<TradeInfo>, IInfraReceiver<QuoteBook>
    {
        public event OnOrderCancelHandler OnOrderCancel;
        public delegate void OnOrderCancelHandler(ulong ordID);

        public Level2Canvas level2Canvas;
        public OrderEntryControl orderEntryControl;
        public TimeAndSalesControl timeAndSalesControl;

        public Guid ControlID;
        public Thread controlThread;

        public InstrInfo[] instrumentArray;

        List<System.Windows.Forms.Timer> _timerList;

        private Level2Settings settings = new Level2Settings();

        public Level2PanelControl()
        {
            InitializeComponent();
            level2Canvas.OnRowSelected += OnGridRowSelected;
            level2Canvas.OnOrderCancel += OnOrderCancelled;
            LoadDefaultLayoutXML();
            UpdateSettingsCheckboxes();
            _timerList  = new List<System.Windows.Forms.Timer>();
            
        }

        public Level2PanelControl(Guid _controlID, InstrInfo[] _instrumentArray) 
        {
            InitializeComponent();
            instrumentArray = _instrumentArray;
            ControlID = _controlID;
            controlThread = Thread.CurrentThread;
            level2Canvas.OnRowSelected += OnGridRowSelected;
            level2Canvas.OnOrderCancel += OnOrderCancelled;
            LoadDefaultLayoutXML();
            UpdateSettingsCheckboxes();
            _timerList = new List<System.Windows.Forms.Timer>();
        }

        public void InitializeAllChildControls(Guid _groupGuid, InstrInfo[] _instrAr)
        {
            ControlID = _groupGuid;
            instrumentArray = _instrAr;

            orderEntryControl.ControlID = _groupGuid;
            orderEntryControl.Initialize(_instrAr);

            timeAndSalesControl.ControlID = _groupGuid;

            Console.WriteLine("load event");
            

        }

        private void OnGridRowSelected(string dest, float price, uint size)
        {
            if (settings.Level2CopyDest)
            {
                orderEntryControl.Dest = dest;
            }
            if (settings.Level2CopyPrice)
            {
                orderEntryControl.Price = price;
            }
            if (settings.Level2CopySize)
            {
                orderEntryControl.Qty = size;
            }
        }

        private void OnOrderCancelled(ulong orderID)
        {
            OnOrderCancel?.Invoke(orderID);
        }

        public QuoteBook MostRecentQuote = new QuoteBook();

        public QuoteBook getRecentQuoteBook()
        {
            return MostRecentQuote;
        }
        public void NewDataHandler(Data_Structures.QuoteBook qBook)
        {
            MostRecentQuote = qBook;
            level2Canvas.NewDataHandler(qBook);
        }

        public TradeInfo MostRecentTrade = new TradeInfo();
        public void NewDataHandler(Data_Structures.TradeInfo tInfo)
        {
            MostRecentTrade = tInfo;
            timeAndSalesControl.NewDataHandler(tInfo);
        }

        #region Order Manager Event Handlers
        

        public void NewOrderAcceptedEventHandler(OrderInfo order, Guid ownerControlGuid)
        {
            if (order.instruments == instrumentArray)
            {
                level2Canvas.AddUpdateOrder(order.orderID, order);
                if (ControlID == ownerControlGuid)
                {
                    addOrderToFlyoutPanel(order);
                }
            }
        }

        public void OrderStatusChangedHandler(OrderInfo order, Guid ownerControlGuid)
        {
            if (Utilities.InstrArrayToOldOMSString(order.instruments).Equals(Utilities.InstrArrayToOldOMSString(instrumentArray))) //TODO:this check more efficiently
            {
                level2Canvas.AddUpdateOrder(order.orderID, order);
                if (ControlID == ownerControlGuid)
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(addOrderFlyoutIntermediary), order);
                }
            }
        }
        
        public void addOrderFlyoutIntermediary(object state)
        {
            if (settings.ShowOrderFlyout)
            {
                addOrderToFlyoutPanel((OrderInfo)state);
            }
        }

        public void addOrderToFlyoutPanel(OrderInfo order)
        {
            LabelControl newLabel = new LabelControl();
            newLabel.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            newLabel.Appearance.ForeColor = System.Drawing.Color.White;
            newLabel.Location = new System.Drawing.Point(3, 7);
            newLabel.Size = new System.Drawing.Size(277, 18);

            DevExpress.Utils.FlyoutPanel newOrderFlyout = new DevExpress.Utils.FlyoutPanel();

            newOrderFlyout.Appearance.BackColor = System.Drawing.Color.LimeGreen;
            newOrderFlyout.Appearance.Options.UseBackColor = true;
            newOrderFlyout.Location = new System.Drawing.Point(2, 2);
            newOrderFlyout.Name = "orderConfirmationFlyoutPanel";
            newOrderFlyout.OwnerControl = this;
            newOrderFlyout.Size = new System.Drawing.Size(591, 31);
            newOrderFlyout.TabIndex = 1;

            string orderString;

            if(order.instruments[0].type == InstrInfo.EType.EQUITY)
            {
                orderString = order.qty + "x" + " @ $" + order.prc +" of " + Utilities.InstrToStr(order.instruments); 
            }
            else
            {
                orderString = order.qty + "x " + "@ $" + order.prc + (order.side == OrderInfo.ESide.BUY ? "BUY" : "SELL") + Utilities.InstrToStr(order.instruments);
            }

            if (order.status == OrderStatus.New)
            {
                newLabel.Text = "Order Accepted: " + orderString;
            }
            else if (order.status == OrderStatus.Rejected)
            {
                newLabel.Text = "Order Rejected: " + orderString;
                newOrderFlyout.Appearance.BackColor = System.Drawing.Color.Red;
            }
            else if (order.status == OrderStatus.Filled)
            {
                newLabel.Text = "Order Filled: " + orderString;
                newOrderFlyout.Appearance.BackColor = System.Drawing.Color.DarkBlue;
            }
            else if (order.status == OrderStatus.Cancelled)
            {
                newLabel.Text = "Order Cancelled: " + orderString;
                newOrderFlyout.Appearance.BackColor = System.Drawing.Color.Black;
            }
            else if (order.status == OrderStatus.Partial)
            {
                newLabel.Text = "Order Partially Filled (" + order.lastQtyExecuted + "): " + orderString;
                newOrderFlyout.Appearance.BackColor = System.Drawing.Color.DarkGray;
            }

            newOrderFlyout.Controls.Add(newLabel);

            if (InvokeRequired)
            {
                Invoke(new Action(() => {
                    level2PanelControlHeader.Controls.Add(newOrderFlyout);
                    newOrderFlyout.ShowPopup();

                    System.Windows.Forms.Timer orderShownTimer = new System.Windows.Forms.Timer();
                    orderShownTimer.Interval = 5000;
                    orderShownTimer.Tick += new EventHandler(delegate (Object o, EventArgs e)
                    {
                        DevExpress.Utils.FlyoutPanel panel = newOrderFlyout;
                        System.Windows.Forms.Timer thisTimer = orderShownTimer;
                        panel.HidePopup();
                        level2PanelControlHeader.Controls.Remove(panel);
                        _timerList.Remove(thisTimer);

                    });
                    orderShownTimer.Enabled = true;
                }));
            }
        }

        #endregion

        private void UpdateSettingsCheckboxes()
        {
            checkEditCopyDest.Checked = settings.Level2CopyDest;
            checkEditCopyPrice.Checked = settings.Level2CopyPrice;
            checkEditCopyQty.Checked = settings.Level2CopySize;

            checkEditLevel2ColHeaders.Checked = level2Canvas.ColumnHeadersVisible;
            checkEditTimeAndSaleColHeaders.Checked = timeAndSalesControl.ColumnHeadersVisible;
            checkEditOrderFlyout.Checked = settings.ShowOrderFlyout;
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
                            case "Level2PanelControl":
                                break;
                            case "Level2Canvas":
                                level2Canvas.LoadLayoutXML(reader.ReadSubtree());
                                break;
                            case "TimeAndSalesControl":
                                timeAndSalesControl.LoadLayoutXML(reader.ReadSubtree());
                                break;
                            case "OrderEntrySplitter":
                                if (reader["Collapsed"] != null) { splitContainerControl2.Collapsed = (reader["Collapsed"]).Equals("True"); }
                                if (reader["SplitterPosition"] != null) { splitContainerControl2.SplitterPosition = (Convert.ToInt32(reader["SplitterPosition"])); }
                                break;
                            case "TimeAndSaleSplitter":
                                if (reader["Collapsed"] != null) { splitContainerControl1.Collapsed = (reader["Collapsed"]).Equals("True"); }
                                if (reader["SplitterPosition"] != null) { splitContainerControl1.SplitterPosition = (Convert.ToInt32(reader["SplitterPosition"])); }
                                break;
                            case "Level2Settings":
                                settings.LoadFromXML(reader.ReadSubtree());
                                break;
                        }
                    }
                }
            }
            UpdateSettingsCheckboxes();
        }

        public string GetLayoutXML()
        {
            using (StringWriter sw = new StringWriter())
            {
                XmlWriterSettings xmlSettings = new XmlWriterSettings();
                xmlSettings.ConformanceLevel = ConformanceLevel.Fragment;
                xmlSettings.OmitXmlDeclaration = true;
                using (XmlWriter xw = XmlWriter.Create(sw, xmlSettings))
                {
                    xw.WriteStartElement("Level2PanelControl");
                    
                    xw.WriteRaw(level2Canvas.GetLayoutXML());
                    xw.WriteRaw(timeAndSalesControl.GetLayoutXML());
                    xw.WriteStartElement("OrderEntrySplitter");
                    xw.WriteAttributeString("Collapsed", "" + splitContainerControl2.Collapsed);
                    xw.WriteAttributeString("SplitterPosition", "" + splitContainerControl2.SplitterPosition);
                    xw.WriteEndElement();

                    xw.WriteStartElement("TimeAndSaleSplitter");
                    xw.WriteAttributeString("Collapsed", "" + splitContainerControl1.Collapsed);
                    xw.WriteAttributeString("SplitterPosition", "" + splitContainerControl1.SplitterPosition);
                    xw.WriteEndElement();

                    xw.WriteRaw(settings.GetXML());

                    xw.WriteEndElement();
                }
                return sw.ToString();
            }
        }

        private void SaveDefaultLayoutXML()
        {
            Directory.CreateDirectory("config");
            File.WriteAllText("config/DefaultLevel2PanelLayout.xml", GetLayoutXML());
        }

        private void LoadDefaultLayoutXML()
        {
            if (File.Exists("config/DefaultLevel2PanelLayout.xml"))
            {
                LoadLayoutXML(XmlReader.Create("config/DefaultLevel2PanelLayout.xml"));
            }
        }

        public void toggleTimeAndSalesControlVisibility()
        {
            splitContainerControl1.Collapsed = !splitContainerControl1.Collapsed;
        }

        public void toggleOrderEntryControlVisibility()
        {
            splitContainerControl2.Collapsed = !splitContainerControl2.Collapsed;
        }

        private void toggleTASButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            toggleTimeAndSalesControlVisibility();
        }

        private void toggleOrderEntryButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            toggleOrderEntryControlVisibility();
        }

        private void windowOptionsButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            settingsFlyoutPanel.ShowPopup();
        }

        private void saveLayoutButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaveDefaultLayoutXML();
        }

        System.Windows.Forms.Timer newTimer = new System.Windows.Forms.Timer();
        private void orderConfirmationPopupPanel_Shown(object sender, DevExpress.Utils.FlyoutPanelEventArgs e)
        {
            newTimer.Interval = 3000;
            newTimer.Tick += NewTimer_Tick;
            newTimer.Enabled = true;
        }

        private void NewTimer_Tick(object sender, EventArgs e)
        {
            newTimer.Enabled = false;
            orderConfirmationFlyoutPanel.HidePopup();
        }

        private void checkEditCopyDest_CheckedChanged(object sender, EventArgs e)
        {
            settings.Level2CopyDest = checkEditCopyDest.Checked;
        }

        private void checkEditCopyPrice_CheckedChanged(object sender, EventArgs e)
        {
            settings.Level2CopyPrice = checkEditCopyPrice.Checked;
        }

        private void checkEditCopyQty_CheckedChanged(object sender, EventArgs e)
        {
            settings.Level2CopySize = checkEditCopyQty.Checked;
        }

        private void checkEdiLevel2ColHeaders_CheckedChanged(object sender, EventArgs e)
        {
            level2Canvas.ColumnHeadersVisible = checkEditLevel2ColHeaders.Checked;
        }

        private void checkEditTimeAndSaleColHeaders_CheckedChanged(object sender, EventArgs e)
        {
            timeAndSalesControl.ColumnHeadersVisible = checkEditTimeAndSaleColHeaders.Checked;
        }

        public void InfraDataReceiveHandler(QuoteBook receivedData)
        {
            level2Canvas.ImpliedMarketHandler(receivedData);
        }

        private void checkEditOrderFlyout_CheckedChanged(object sender, EventArgs e)
        {
            settings.ShowOrderFlyout = checkEditOrderFlyout.Checked;
        }
    }
}
