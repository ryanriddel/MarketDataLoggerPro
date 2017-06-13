using DevExpress.Utils.Menu;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using MktSrvcAPI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FastOMS.UI
{
    public partial class SpreadBuilderForm : DevExpress.XtraEditors.XtraForm
    {
        private BindingList<InstTableObj> _instBinding = new BindingList<InstTableObj>();

        DXMenuItem removeSelectedInstrumentMenuItem;
        InstTableObj selectedInst;

        public SpreadBuilderForm()
        {
            InitializeComponent();
            SetupGridView();
        }

        private void SetupGridView()
        {
            gridControl1.DataSource = _instBinding;
            _instBinding.RaiseListChangedEvents = false;

            RepositoryItemComboBox riCombo = new RepositoryItemComboBox();
            riCombo.Items.AddRange(new char[] { 'B', 'S' });
            riCombo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            gridControl1.RepositoryItems.Add(riCombo);

            gridView1.Columns.Clear();
            GridColumn symCol = new GridColumn() { Caption = "Sym", Visible = true, FieldName = "Symbol" };
            symCol.OptionsColumn.AllowEdit = false;
            gridView1.Columns.Add(symCol);

            GridColumn maturityCol = new GridColumn() { Caption = "Maturity", Visible = true, FieldName = "Maturity" };
            maturityCol.OptionsColumn.AllowEdit = false;
            gridView1.Columns.Add(maturityCol);

            GridColumn strikeCol = new GridColumn() { Caption = "Strike", Visible = true, FieldName = "Strike" };
            strikeCol.OptionsColumn.AllowEdit = false;
            gridView1.Columns.Add(strikeCol);

            GridColumn callputCol = new GridColumn() { Caption = "C/P", Visible = true, FieldName = "CallPut" };
            callputCol.OptionsColumn.AllowEdit = false;
            gridView1.Columns.Add(callputCol);

            GridColumn sideCol = new GridColumn() { Caption = "Side", Visible = true, FieldName = "Side" };
            sideCol.ColumnEdit = riCombo;
            gridView1.Columns.Add(sideCol);

            GridColumn ratioCol = new GridColumn() { Caption = "Ratio", Visible = true, FieldName = "Ratio" };
            ratioCol.OptionsColumn.AllowEdit = false;
            gridView1.Columns.Add(ratioCol);

            removeSelectedInstrumentMenuItem = new DXMenuItem("Remove Instrument", RemoveSelectedInstrument);
        }

        private void AddInstrInfo(InstrInfo i)
        {
            _instBinding.Add(new InstTableObj(i));
            PopulateGrid();
        }

        private void FlipSides()
        {
            foreach (InstTableObj i in _instBinding)
            {
                i.Side = i.Side == 'B' ? 'S' : 'B';
            }
            PopulateGrid();
        }

        private void RemoveSelectedInstrument(object sender, System.EventArgs e)
        {
            _instBinding.Remove(selectedInst);
            PopulateGrid();
        }

        private InstrInfo[] GetInstrInfo()
        {
            InstrInfo[] instrs = new InstrInfo[_instBinding.Count];
            for (int i = 0; i < _instBinding.Count; i++)
            {
                instrs[i] = _instBinding[i].ToInstrInfo();
            }
            return instrs;
        }

        private void PopulateGrid()
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
            pictureBoxCopyOut.Image = gridView1.RowCount > 0 ? Properties.Resources.copyout : Properties.Resources.copyoutDisabled;
        }

        private class InstTableObj
        {
            public string Symbol { get; set; }
            public int Maturity { get; set; }
            public float Strike { get; set; }
            public char CallPut { get; set; }
            public char Side { get; set; }
            public int Ratio { get; set; }

            public int Type { get; set; }

            public InstTableObj(InstrInfo i)
            {
                Symbol = i.sym;
                Maturity = i.maturity;
                Strike = i.strike;
                CallPut = i.callput == InstrInfo.ECallPut.CALL ? 'C' : 'P';
                Side = i.side == OrdInfo.ESide.BUY ? 'B' : 'S';
                Ratio = i.ratio;
                Type = (int)i.type;
            }

            public InstrInfo ToInstrInfo()
            {
                InstrInfo i = new InstrInfo();
                i.sym = Symbol;
                i.maturity = Maturity;
                i.strike = Strike;
                i.callput = CallPut == 'C' ? InstrInfo.ECallPut.CALL : InstrInfo.ECallPut.PUT;
                i.side = Side == 'B' ? OrdInfo.ESide.BUY : OrdInfo.ESide.SELL;
                i.ratio = Ratio;
                i.type = (InstrInfo.EType)Type;
                return i;
            }
        }

        private void buttonAdd_Click(object sender, System.EventArgs e)
        {
            SymbolEntryWindow picker = new SymbolEntryWindow(true);
            picker.OnEntryComplete += AddInstrInfo;
            picker.ShowDialog();
        }

        private void buttonQuote_Click(object sender, System.EventArgs e)
        {
           // Hub._formFactory.CreateOrigSpreadForm(GetInstrInfo());
        }

        private void buttonDockQuote_Click(object sender, EventArgs e)
        {
            Hub._formFactory.CreateSpreadForm(GetInstrInfo());
        }

        private void buttonFlip_Click(object sender, EventArgs e)
        {
            FlipSides();
        }

        private void buttonRemoveAll_Click(object sender, EventArgs e)
        {
            _instBinding.Clear();
            PopulateGrid();
        }

        private void gridControl1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void gridControl1_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                string droppedString = e.Data.GetData(DataFormats.Text).ToString();
                InstrInfo[] instrArr = Utilities.OldOMSStringToInstrArray(droppedString);

                _instBinding.Clear();
                foreach (InstrInfo i in instrArr)
                {
                    _instBinding.Add(new InstTableObj(i));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            PopulateGrid();
        }

        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            selectedInst = null;
            GridHitInfo gridHit = gridView1.CalcHitInfo(new Point(e.X, e.Y));
            if (gridHit.InDataRow)
            {
                selectedInst = ((InstTableObj)gridView1.GetRow(gridHit.RowHandle));
            }
        }

        private void gridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                e.Menu.Items.Add(removeSelectedInstrumentMenuItem);
            }
        }

        private void pictureBoxCopyOut_MouseDown(object sender, MouseEventArgs e)
        {
            string str = Utilities.InstrArrayToOldOMSString(GetInstrInfo());
            pictureBoxCopyOut.DoDragDrop(str, DragDropEffects.Move | DragDropEffects.Copy);
        }
    }
}