using System;
using System.Diagnostics;
using System.Windows.Forms;
using SGLibrary.Extend.ControlEx;
using Telossoft.SimpleVC.WinFormApp.VCControl;
using Telossoft.SimpleVC.Model;

namespace Telossoft.SimpleVC.WinFormApp.UserInterface
{
    public partial class RuleSetupFm : Form
    {
        public RuleSetupFm()
        {
            InitializeComponent();
        }

        private RuleGrid ruleGrid;
        private BaseEntity Entity;
        
        private void RuleSetupFm_Load(object sender, EventArgs e)
        {
            ruleGrid = new RuleGrid();
            ruleGrid.ReadOnly = VC2WinFmApp.DataRule.ReadOnly;

            ruleGrid.Dock = DockStyle.Fill;
            ruleGrid.Solution = VC2WinFmApp.DataRule.Solution;
            ruleGrid.Parent = this.panel2;

            btReload_Click(null, null);
            btReload.Enabled = !VC2WinFmApp.DataRule.ReadOnly;
            btOk.Enabled = !VC2WinFmApp.DataRule.ReadOnly;
            new FormMemoryImpl(this, VC2WinFmApp.Cfg, "FormRem");  //窗体记忆
        }

        private void btReload_Click(object sender, EventArgs e)
        {
            ruleGrid.Rules = VC2WinFmApp.DataRule.Rule.GetRules(Entity);
        }

        private void RuleSetupFm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
                VC2WinFmApp.DataRule.Rule.SetRules(Entity, ruleGrid.Rules);
        }

        public static void SetEntityRule(BaseEntity Ety)
        {
            Debug.Assert(Ety != null, "不可用null调用SetEntityRule");

            RuleSetupFm Fm = new RuleSetupFm();
            Fm.Entity = Ety;
            Fm.Text = "设置<" + Ety + ">的规则";
            Fm.ShowDialog();
        }

    }
}