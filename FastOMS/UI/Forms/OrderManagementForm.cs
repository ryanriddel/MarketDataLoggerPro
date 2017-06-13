using FastOMS.UI.Interfaces;
using System;
using System.IO;
using System.Xml;

namespace FastOMS.UI
{
    public partial class OrderManagementForm : DevExpress.XtraEditors.XtraForm, ILayoutSaveLoader
    {
        public event OnOrderCancelHandler OnOrderCancel;
        public delegate void OnOrderCancelHandler(ulong ordID);

        public OrderManagementForm()
        {
            InitializeComponent();
            SetupGrids();
        }

        private void SetupGrids()
        {
            orderManagementGridAll.SetOrderStatusFilter(OrderManagementGrid.OrderFilter.All);
            orderManagementGridAll.OnOrderCancel += OnOrdCancel;
            orderManagementGridOpen.SetOrderStatusFilter(OrderManagementGrid.OrderFilter.Open);
            orderManagementGridOpen.OnOrderCancel += OnOrdCancel;
            orderManagementGridCancelled.SetOrderStatusFilter(OrderManagementGrid.OrderFilter.Cancelled);
            orderManagementGridCancelled.OnOrderCancel += OnOrdCancel;
            orderManagementGridExecuted.SetOrderStatusFilter(OrderManagementGrid.OrderFilter.Executed);
            orderManagementGridExecuted.OnOrderCancel += OnOrdCancel;
            orderManagementGridRejected.SetOrderStatusFilter(OrderManagementGrid.OrderFilter.Rejected);
            orderManagementGridAll.OnOrderCancel += OnOrdCancel;
        }

        public void OrderStatusChangedHandler(OrderInfo order, Guid ownerControlGuid)
        {
            orderManagementGridAll.OnOrderStatusChanged(order, ownerControlGuid);
            orderManagementGridOpen.OnOrderStatusChanged(order, ownerControlGuid);
            orderManagementGridCancelled.OnOrderStatusChanged(order, ownerControlGuid);
            orderManagementGridExecuted.OnOrderStatusChanged(order, ownerControlGuid);
            orderManagementGridRejected.OnOrderStatusChanged(order, ownerControlGuid);
        }

        private void OnOrdCancel(ulong orderID)
        {
            OnOrderCancel?.Invoke(orderID);
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
                            case "OrderManagementForm":
                                if (reader["Location"] != null)
                                {
                                    string[] pointSplit = reader["Location"].Split(',');
                                    Location = new System.Drawing.Point(Convert.ToInt32(pointSplit[0]), Convert.ToInt32(pointSplit[1]));
                                }
                                if (reader["Size"] != null)
                                {
                                    string[] pointSplit = reader["Size"].Split(',');
                                    Size = new System.Drawing.Size(Convert.ToInt32(pointSplit[0]), Convert.ToInt32(pointSplit[1]));
                                }
                                if (reader["SelectedPageIndex"] != null)
                                {
                                    tabPane.SelectedPageIndex = Convert.ToInt32(reader["SelectedPageIndex"]);
                                }
                                break;
                            case "OrderManagementGridAll":
                                orderManagementGridAll.LoadLayoutXML(reader.ReadSubtree());
                                break;
                            case "OrderManagementGridOpen":
                                orderManagementGridOpen.LoadLayoutXML(reader.ReadSubtree());
                                break;
                            case "OrderManagementGridCancelled":
                                orderManagementGridCancelled.LoadLayoutXML(reader.ReadSubtree());
                                break;
                            case "OrderManagementGridExecuted":
                                orderManagementGridExecuted.LoadLayoutXML(reader.ReadSubtree());
                                break;
                            case "OrderManagementGridRejected":
                                orderManagementGridRejected.LoadLayoutXML(reader.ReadSubtree());
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
                    xw.WriteStartElement("OrderManagementForm");
                    xw.WriteAttributeString("Location", this.Location.X + "," + this.Location.Y);
                    xw.WriteAttributeString("Size", this.Size.Width + "," + this.Size.Height);
                    xw.WriteAttributeString("SelectedPageIndex", "" + tabPane.SelectedPageIndex);

                    xw.WriteRaw(orderManagementGridAll.GetLayoutXML());
                    xw.WriteRaw(orderManagementGridOpen.GetLayoutXML());
                    xw.WriteRaw(orderManagementGridCancelled.GetLayoutXML());
                    xw.WriteRaw(orderManagementGridExecuted.GetLayoutXML());
                    xw.WriteRaw(orderManagementGridRejected.GetLayoutXML());

                    xw.WriteEndElement();
                }
                return sw.ToString();
            }
        }
    }
}