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
    public partial class SpreadLegDockPanel : DevExpress.XtraEditors.XtraUserControl
    {
        public SpreadLegDockPanel()
        {
            InitializeComponent();
        }

        
        List<Level2PanelControl> _level2PanelList = new List<Level2PanelControl>(2);
        public void DockLevel2PanelControl(Level2PanelControl ctrl)
        {
            _level2PanelList.Add(ctrl);

            int count = _level2PanelList.Count;

            if(count%2 == 0)
            {
                //add to left side
                _leftSpreadDock.DockLevel2PanelControl(ctrl);

            }
            else
            {
                //add to right side
                _rightSpreadDock.DockLevel2PanelControl(ctrl);
            }
        }
    }
}
