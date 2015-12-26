using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SGLibrary.Extend.ControlEx;
using SGLibrary.Framework.GridBind.WinForm;
using Telossoft.SimpleVC.Model;
using SGLibrary.Framework.GridBind;

namespace Telossoft.SimpleVC.WinFormApp.EntitySelect
{
    public partial class MbrSelectFmMulti : MbrSelectFm
    {
        public MbrSelectFmMulti()
        {
            InitializeComponent();
        }

        private MultiSelectList SelectList = new MultiSelectList();

        public IList<BaseEntity> GetSelectEties()
        {
            return SelectList.GetSelected();
        }

        public void SetOldSelectList(IList<BaseEntity> OldSelectList)
        {
            SelectList.SetOldSelectList(OldSelectList);
        }

        protected GridViewBind<CheckSelectEty> Bind;

        private void MbrSelectFmMulti_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            this.GrpGrid.SelectionChanged += new System.EventHandler(this.GrpGrid_SelectionChanged);

            this.MbrGrid.MultiSelect = false;
            this.MbrGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            this.Bind = new GridViewBind<CheckSelectEty>(grid: this.MbrGrid, 
                columnMng: new GridBindColumnMngImpl_Appoint( SelectList.GetColumnPropertis()));

            Bind.Binding(SelectList);
            GrpGrid_SelectionChanged(null, null);
            new FormMemoryImpl(this, VC2WinFmApp.Cfg, "FormRem");  //¥∞ÃÂº«“‰
        }

        private void GrpGrid_SelectionChanged(object sender, EventArgs e)
        {
            SelectList.Update(MbrSelect.GetMbrList(GrpBind.Current));

            Bind.Refresh();
        }

        private void MbrSelectFmMulti_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && e.Control)
            {
                SelectList.CheckAll();
                Bind.Refresh();
            }
        }
    }
}

