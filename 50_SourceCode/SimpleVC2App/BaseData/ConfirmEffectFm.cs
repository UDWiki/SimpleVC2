using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGLibrary.DB;
using Telossoft.SimpleVC.Model;
using SGLibrary.Extend.ControlEx;
using SGLibrary.Framework.GridBind;
using SGLibrary.Framework.GridBind.WinForm;

namespace Telossoft.SimpleVC.WinFormApp.BaseData
{
    public partial class ConfirmEffectFm : Form
    {
        public IList<VcEffect> Effects;

        public ConfirmEffectFm()
        {
            InitializeComponent();
        }

        public static Boolean Confirm(IList<VcEffect> Effects)
        {
            if (Effects.Count == 0)
                return true;

            ConfirmEffectFm Fm = new ConfirmEffectFm();
            Fm.Effects = Effects;
            return Fm.ShowDialog() == DialogResult.OK;
        }

        private GridViewBind<VcEffect> GrdBind;
        private void ConfirmEffectFm_Load(object sender, EventArgs e)
        {
            GrdBind = new GridViewBind<VcEffect>(grid: this.EffectsGrd,
               columnMng: new GridBindColumnMngImpl_Appoint(
                   new GridBindColumnAndPropertyMap ("ClsLesson", new GridBindColumnAttribute { Title = "课程" }),
                   new GridBindColumnAndPropertyMap("Description", new GridBindColumnAttribute { Title = "后果" })));
            new FormMemoryImpl(this, VC2WinFmApp.Cfg, "FormRem");  //窗体记忆
        }

        private void ConfirmEffectFm_Shown(object sender, EventArgs e)
        {
            GrdBind.Binding(Effects);
        }
    }
}