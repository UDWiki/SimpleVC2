using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SGLibrary.Extend.ControlEx;
using SGLibrary.Framework.GridBind.WinForm;
using Telossoft.SimpleVC.Model;
using SGLibrary.Framework.GridBind;

namespace Telossoft.SimpleVC.WinFormApp.EntitySelect
{
    public partial class GrpSelectFmMulti : SimpleVC.WinFormApp.EntitySelect.GrpSelectFm
    {
        public GrpSelectFmMulti()
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

        private void GrpSelectFmMulti_Load(object sender, EventArgs e)
        {
            this.Text = "—°‘Ò <" + GrpSlt.Kind + ">";
            this.Bind = new GridViewBind<CheckSelectEty>(this.Grid,
                null, null, 0, new GridBindColumnMngImpl_Appoint(SelectList.GetColumnPropertis()));

            SelectList.Update(GrpSlt.List);
            Bind.Binding(SelectList);
            new FormMemoryImpl(this, VC2WinFmApp.Cfg, "FormRem");  //¥∞ÃÂº«“‰
        }

        private void GrpSelectFmMulti_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && e.Control)
            {
                SelectList.CheckAll();
                Grid.Refresh();
            }
        }
    }
}

