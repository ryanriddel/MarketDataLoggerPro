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
using DevExpress.XtraBars.Docking2010.Views;

namespace FastOMS.UI.Controls
{
    public partial class SpreadDock : DevExpress.XtraEditors.XtraUserControl
    {
        public SpreadDock()
        {
            InitializeComponent();
        }

        public void DockLevel2PanelControl(Level2PanelControl level2Control)
        {
            BaseDocument doc = tabbedView1.AddDocument(level2Control);
            doc.Caption = Utilities.InstrArrayToOldOMSString(level2Control.instrumentArray);
            doc.Header = doc.Caption;
            
        }
    }
}
