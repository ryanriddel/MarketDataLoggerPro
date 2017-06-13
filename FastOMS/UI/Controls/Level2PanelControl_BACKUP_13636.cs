using System;
using System.Collections.Generic;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using FastOMS.Data_Structures;
using System.Threading;
using MktSrvcAPI;
using System.IO;
using System.Xml;
using System.Text;

namespace FastOMS.UI
{
    public partial class Level2PanelControl : DevExpress.XtraEditors.XtraUserControl, IMarketDataConsumer<TradeInfo>, IMarketDataConsumer<QuoteBook>
    {
        public event OnOrderCancelHandler OnOrderCancel;
        public delegate void OnOrderCancelHandler(ulong ordID);

        public Level2Canvas level2Canvas;
        public OrderEntryControl orderEntryControl;
        public TimeAndSalesControl timeAndSalesControl;

        public Guid ControlID;
        public Thread controlThread;

        public InstrInfo[] instrumentArray;

        List<System.Windows.Forms.Timer> _timerList = new List<System.Windows.Forms.Timer>();

        public Level2PanelControl()
        {
            InitializeComponent();
<<<<<<< HEAD

            controlThread = Thread.CurrentThread;

            
=======
            controlThread = Thread.CurrentThread;
>>>>>>> origin/Joe_UI
            level2Canvas.OnRowSelected += OnGridRowSelected;
            level2Canvas.OnOrderCancel += OnOrderCancelled;
            LoadDefaultLayoutXML();
        }

        public Level2PanelControl(Guid _controlID, InstrInfo[] _instrumentArray)
        {
            InitializeComponent();
            instrumentArray = _instrumentArray;
            ControlID = _controlID;
        }

        private void OnGridRowSelected(string dest, float price, uint size)
        {
            orderEntryControl.SetFields(dest, price, size);
        }

        private void OnOrderCancelled(ulong orderID)
        {
            OnOrderCancel?.Invoke(orderID);
        }

        public void NewDataHandler(Data_Structures.QuoteBook qBook)
        {
            level2Canvas.NewDataHandler(qBook);
        }

        public void NewDataHandler(Data_Structures.TradeInfo tInfo)
        {
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
            if (order.instruments == instrumentArray)
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
            addOrderToFlyoutPanel((OrderInfo)state);
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

                    xw.WriteEndElement();
                }
                return sw.ToString();
            }
        }

        private void SaveDefaultLayoutXML()
        {
            File.WriteAllText("DefaultLevel2PanelLayout.xml", GetLayoutXML());
        }

        private void LoadDefaultLayoutXML()
        {
            if (File.Exists("DefaultLevel2PanelLayout.xml"))
            {
                LoadLayoutXML(XmlReader.Create("DefaultLevel2PanelLayout.xml"));
            }
        }

        public void toggleTimeAndSalesControlVisibility()
        {
            splitContainerControl2.Collapsed = !splitContainerControl2.Collapsed;
        }

        public void toggleOrderEntryControlVisibility()
        {
            splitContainerControl1.Collapsed = !splitContainerControl1.Collapsed;
        }

        private void level2Canvas_Load(object sender, EventArgs e)
        {

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

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }
    }
}
