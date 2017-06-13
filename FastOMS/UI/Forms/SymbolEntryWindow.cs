using MktSrvcAPI;
using System;
using System.Drawing;

namespace FastOMS.UI
{
    public partial class SymbolEntryWindow : DevExpress.XtraEditors.XtraForm
    {
        public event OnEntryCompleteHandler OnEntryComplete;
        public delegate void OnEntryCompleteHandler(InstrInfo inst);

        private bool _spread;

        public SymbolEntryWindow(bool spread)
        {
            _spread = spread;
            InitializeComponent();

            if (!_spread)
            {
                panelSideRatio.Visible = false;
                buttonOK.Location = new Point(buttonOK.Location.X, buttonOK.Location.Y - panelSideRatio.Size.Height);
                this.Size = new Size(this.Size.Width, this.Size.Height - panelSideRatio.Size.Height);
            }
        }

        public InstrInfo GetInstrInfo()
        {
            InstrInfo i = new InstrInfo();

            i.sym = textEditSymbol.Text;
            if (radioGroupType.SelectedIndex == 0)
            {
                i.type = InstrInfo.EType.OPTION;
                if (radioGroupCallPut.SelectedIndex == 0)
                {
                    i.callput = InstrInfo.ECallPut.CALL;
                }
                else if (radioGroupCallPut.SelectedIndex == 1)
                {
                    i.callput = InstrInfo.ECallPut.PUT;
                }
                i.maturity = Convert.ToInt32(dateEditMaturity.DateTime.ToString("yyyyMMdd"));
                i.strike = Convert.ToSingle(textEditStrike.Text);
            }
            else if (radioGroupType.SelectedIndex == 0)
            {
                i.type = InstrInfo.EType.EQUITY;
            }
            if (_spread)
            {
                if (radioGroupSide.SelectedIndex == 0)
                {
                    i.side = OrdInfo.ESide.BUY;
                }
                else if (radioGroupSide.SelectedIndex == 1)
                {
                    i.side = OrdInfo.ESide.SELL;
                }
                i.ratio = Convert.ToInt32(textEditRatio.Text);
            }

            return i;
        }

        private void SetOKEnabled()
        {
            buttonOK.Enabled = CheckInputs();
        }

        private bool CheckInputs()
        {
            if ((textEditSymbol.Text.Length == 0) ||
                (_spread && (textEditRatio.Text.Length == 0 || Convert.ToInt32(textEditRatio.Text) == 0)) ||
                (radioGroupType.SelectedIndex == 0 && (dateEditMaturity.Text.Length == 0 || textEditStrike.Text.Length == 0 || Convert.ToSingle(textEditStrike.Text) == 0.00f)))
            {
                return false;
            }

            return true;
        }

        private void radioGroupType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroupType.SelectedIndex == 0)
            {
                panelOptionDetails.Visible = true;
                panelSideRatio.Location = new Point(panelSideRatio.Location.X, panelSideRatio.Location.Y + panelOptionDetails.Size.Height);
                buttonOK.Location = new Point(buttonOK.Location.X, buttonOK.Location.Y + panelOptionDetails.Size.Height);
                this.Size = new Size(this.Size.Width, this.Size.Height + panelOptionDetails.Size.Height);
            }
            else
            {
                panelOptionDetails.Visible = false;
                panelSideRatio.Location = new Point(panelSideRatio.Location.X, panelSideRatio.Location.Y - panelOptionDetails.Size.Height);
                buttonOK.Location = new Point(buttonOK.Location.X, buttonOK.Location.Y - panelOptionDetails.Size.Height);
                this.Size = new Size(this.Size.Width, this.Size.Height - panelOptionDetails.Size.Height);
            }
            SetOKEnabled();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (OnEntryComplete != null)
            {
                OnEntryComplete.Invoke(GetInstrInfo());
            }
            Dispose();
        }

        private void inputField_EditValueChanged(object sender, EventArgs e)
        {
            SetOKEnabled();
        }
    }
}
