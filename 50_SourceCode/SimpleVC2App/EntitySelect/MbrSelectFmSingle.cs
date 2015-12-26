using System;
using System.Windows.Forms;
using SGLibrary.Extend.ControlEx;
using SGLibrary.Framework.GridBind.WinForm;
using Telossoft.SimpleVC.Model;
using SGLibrary.Framework.GridBind;

namespace Telossoft.SimpleVC.WinFormApp.EntitySelect
{
    public partial class MbrSelectFmSingle : MbrSelectFm
    {
        public MbrSelectFmSingle()
        {
            InitializeComponent();
        }

        private BaseEntity selectEty;

        public BaseEntity SelectEty
        {
            get { return selectEty; }
            set { selectEty = value; }
        }

        private GridViewBind<BaseEntity> MbrBind;

        private void SetControlEnabled()
        {
            btOk.Enabled = MbrGrid.SelectedRows.Count == 1;
        }

        private void MbrSelectFmTch_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            this.GrpGrid.SelectionChanged += new System.EventHandler(this.GrpGrid_SelectionChanged);

            this.MbrGrid.MultiSelect = false;
            this.MbrGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            this.MbrGrid.SelectionChanged += new System.EventHandler(this.MbrGrid_SelectionChanged);
            this.MbrGrid.DoubleClick += new System.EventHandler(this.MbrGrid_DoubleClick);

            MbrBind = new GridViewBind<BaseEntity>(grid: MbrGrid,
                columnMng: new GridBindColumnMngImpl_Appoint(
                new GridBindColumnAndPropertyMap("Name", new GridBindColumnAttribute{ Title = "Ãû³Æ", ReadOnly = true })));

            GrpGrid_SelectionChanged(null, null);
            new FormMemoryImpl(this, VC2WinFmApp.Cfg, "FormRem");  //´°Ìå¼ÇÒä
        }

        private void MbrSelectFmSingle_Shown(object sender, EventArgs e)
        {
            MbrBind.PositionTo(this.SelectEty);
        }

        private void GrpGrid_SelectionChanged(object sender, EventArgs e)
        {
            MbrBind.Binding(MbrSelect.GetMbrList(GrpBind.Current));
        }

        private void MbrGrid_SelectionChanged(object sender, EventArgs e)
        {
            SetControlEnabled();
        }

        private void MbrSelectFmTch_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
                this.SelectEty = MbrBind.Current as BaseEntity;
        }

        private void MbrGrid_DoubleClick(object sender, EventArgs e)
        {
            if (this.btOk.Enabled)
            {
                this.DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btNull_Click(object sender, EventArgs e)
        {
            this.FormClosing -= MbrSelectFmTch_FormClosing;
            this.selectEty = null;

            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}

