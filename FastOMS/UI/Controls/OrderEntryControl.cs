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

using MktSrvcAPI;
using FastOMS.Preferences;

namespace FastOMS.UI
{
    public partial class OrderEntryControl : DevExpress.XtraEditors.XtraUserControl
    {
        public string Dest
        {
            get
            {
                return comboBoxDest.Text;
            }
            set
            {
                int index = comboBoxDest.Properties.Items.IndexOf(value);
                if (index != -1)
                {
                    comboBoxDest.SelectedIndex = index;
                }
            }
        }
        public float Price { get { return (float)spinnerPrice.Value; } set { spinnerPrice.Value = (decimal)value; } }
        public uint Qty { get { return (uint)spinnerQty.Value; } set { spinnerQty.Value = value; } }

        public delegate void OrderButtonPressedHandler(OrderInfo order, Guid controlID);
        public event OrderButtonPressedHandler OnOrderButtonPressed;

        InstrInfo.EType _instrumentType;
        InstrInfo[] _instruments;

        public Guid ControlID;

        private Level2Settings settings;

        public OrderEntryControl()
        {
            InitializeComponent();
        }

        public OrderEntryControl(InstrInfo[] instr) : this()
        {
            Initialize(instr);
        }

        public void Initialize(InstrInfo[] instr)
        {
            _instruments = instr;
            _instrumentType = instr.Length > 1 ? InstrInfo.EType.OPTION : instr[0].type;
            SetupComboBoxes();
        }

        private void SetupComboBoxes()
        {
            string[] tifNames = Enum.GetNames(typeof(OrdInfo.ETIF));
            comboBoxTiF.Properties.Items.AddRange(tifNames);
            comboBoxTiF.SelectedIndex = 0;

            comboBoxOpenClose.Properties.Items.AddRange(new object[]
            {
                "OPEN",
                "CLOSE"
            });
            comboBoxOpenClose.SelectedIndex = 0;

            if (_instrumentType == InstrInfo.EType.OPTION)
            {
                comboBoxDest.Properties.Items.AddRange(Enum.GetNames(typeof(OptExch)));
            }
            else if (_instrumentType == InstrInfo.EType.EQUITY)
            {
                comboBoxDest.Properties.Items.AddRange(Enum.GetNames(typeof(EqExch)));
            }
            comboBoxDest.SelectedIndex = 0;
        }

        private void SetBuySellEnabled()
        {
            bool enabled = CheckInputs();
            buttonBuy.Enabled = enabled;
            buttonSell.Enabled = enabled;
        }

        private bool CheckInputs()
        {
            if (!(Convert.ToInt32(spinnerQty.Text) > 0) || comboBoxDest.SelectedIndex == -1 ||
                comboBoxTiF.SelectedIndex == 1 || comboBoxOpenClose.SelectedIndex == -1)
            {
                return false;
            }
            return true;
        }

        private void buttonBuy_MouseClick(object sender, MouseEventArgs e)
        {
            PlaceOrder(OrderInfo.ESide.BUY);
        }

        private void buttonSell_MouseClick(object sender, MouseEventArgs e)
        {
            PlaceOrder(OrderInfo.ESide.SELL);
        }

        private void PlaceOrder(OrderInfo.ESide side)
        {
            OrderInfo newOrder = new OrderInfo();
            newOrder.qty = Convert.ToUInt32(spinnerQty.Value);
            newOrder.prc = Convert.ToSingle(spinnerPrice.Value);
            newOrder.type = OrderInfo.EType.LIM;
            newOrder.tif = (OrderInfo.ETIF)(comboBoxTiF.SelectedIndex);
            newOrder.side = side;
            newOrder.exchange = comboBoxDest.Text;
            newOrder.exchByte =  Convert.ToByte(comboBoxDest.SelectedIndex); //TODO: replace with string/byte converter; shouldn't rely on order of strings in combobox
            newOrder.isClosingOrder = comboBoxOpenClose.SelectedIndex != 0;
            newOrder.instruments = _instruments;

            OnOrderButtonPressed.Invoke(newOrder, ControlID);
        }

        private void orderField_EditValueChanged(object sender, EventArgs e)
        {
            SetBuySellEnabled();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            spinnerQty.EditValue = 0;
            spinnerPrice.EditValue = 0;
            comboBoxDest.SelectedIndex = 0;
            comboBoxOpenClose.SelectedIndex = 0;
            comboBoxTiF.SelectedIndex = 0;
        }
    }
}
