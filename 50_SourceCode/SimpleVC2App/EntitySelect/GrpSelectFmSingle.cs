using System;
using System.Windows.Forms;
using SGLibrary.Extend.ControlEx;
using SGLibrary.Framework.GridBind.WinForm;
using Telossoft.SimpleVC.Model;
using SGLibrary.Framework.GridBind;

namespace Telossoft.SimpleVC.WinFormApp.EntitySelect
{
    public partial class GrpSelectFmSingle : GrpSelectFm
    {
        public GrpSelectFmSingle()
        {
            InitializeComponent();
            Grid.ReadOnly = true;
        }

        private BaseEntity selectEty;

        public BaseEntity SelectEty
        {
            get { return selectEty; }
            set { selectEty = value; }
        }

        protected GridViewBind<BaseEntity> Bind;

        private void GrpSelectFmSingle_Load(object sender, EventArgs e)
        {
            Grid.MouseDoubleClick += this.Grid_MouseDoubleClick;

            this.Text = "选择 <" + GrpSlt.Kind + ">";
            this.Bind = new GridViewBind<BaseEntity>(this.Grid, 
                columnMng: new GridBindColumnMngImpl_Appoint(
                new GridBindColumnAndPropertyMap("Name", new GridBindColumnAttribute { Title = "名称" })));

            Bind.Binding(GrpSlt.List);
            Bind.PositionTo(selectEty);
            new FormMemoryImpl(this, VC2WinFmApp.Cfg, "FormRem");  //窗体记忆
        }

        private void Grid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Bind.Count > 0)
            {
                this.DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void GrpSelectFmSingle_FormClosed(object sender, FormClosedEventArgs e)
        {
            selectEty = Bind.Current;
        }

    }
}

