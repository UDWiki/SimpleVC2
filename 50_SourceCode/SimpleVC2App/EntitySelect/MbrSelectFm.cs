using System;
using System.Windows.Forms;
using SGLibrary.Framework.GridBind.WinForm;
using Telossoft.SimpleVC.Model;
using SGLibrary.Framework.GridBind;

namespace Telossoft.SimpleVC.WinFormApp.EntitySelect
{
    public partial class MbrSelectFm : Form
    {
        public MbrSelectFm()
        {
            InitializeComponent();
        }

        private IMbrSelect mbrSelect;

        public IMbrSelect MbrSelect
        {
            get { return mbrSelect; }
            set { mbrSelect = value; }
        }

        protected GridViewBind<BaseEntity> GrpBind;

        private void InitGroupGrid()
        {
            GrpBind = new GridViewBind<BaseEntity>(GrpGrid,
                columnMng: new GridBindColumnMngImpl_Appoint(
                new GridBindColumnAndPropertyMap("Name", new GridBindColumnAttribute { Title = "×éÃû³Æ", ReadOnly = true })));
            GrpBind.Binding(MbrSelect.GroupList);
        }

        private void MbrSelectFm_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            this.Text = "Ñ¡Ôñ <" + MbrSelect.Kind + ">";
            InitGroupGrid();
        }
    }
}