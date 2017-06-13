using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace FastOMS.UI.Controls
{
    public partial class OrderFlyoutPanel : DevExpress.Utils.FlyoutPanel
    {
        private OrderInfo _order = new OrderInfo();

        public OrderFlyoutPanel()
        {
            InitializeComponent();
        }

        public void SetOrderInfo(OrderInfo order)
        {
            _order = order;
            textEdit1.Text = _order.orderID.ToString();
            textEdit2.Text = _order.exchange;
        }
    }
}
