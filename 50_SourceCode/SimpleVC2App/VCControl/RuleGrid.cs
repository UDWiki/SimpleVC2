using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Telossoft.SimpleVC.Model;

namespace Telossoft.SimpleVC.WinFormApp.VCControl
{
    public partial class RuleGrid : UserControl
    {
        public RuleGrid()
        {
            InitializeComponent();
            BorderStyle = BorderStyle.Fixed3D;
        }

        private Boolean readOnly;

        public Boolean ReadOnly
        {
            get { return readOnly; }
            set 
            { 
                readOnly = value; 
            }
        }

        private EnSolution solution = new EnSolution();

        public EnSolution Solution
        {
            get { return solution; }
            set
            {
                solution = value;
            }
        }

        private VcGridV2<RuleCell> Grid;
        //private VcGrid<RuleCell> Grid;

        private void RuleGrid_Load(object sender, EventArgs e)
        {
            InitSubControl();
            Grid = new VcGridV2<RuleCell>(panel1);
            //Grid = new VcGrid<RuleCell>(panel1);
            Grid.Solution = this.solution;
            Grid.Padding = 10;

            foreach (RuleCell cell in Grid.eachCell())
            {
                cell.MouseMove += new MouseEventHandler(this.pnl_MouseMove);
                cell.MouseDown += new MouseEventHandler(this.pnl_MouseMove);
            }

            Rules = Rules;
        }

        private void NowRuleClick(object sender, EventArgs e)
        {
            if (ReadOnly)
                return;

            this.NowRule = (eRule)((Panel)sender).Tag;
        }

        private eRule nowRule;

        public eRule NowRule
        {
            get { return nowRule; }
            set
            {
                nowRule = value;
                tbNowRule.BackColor = ViewStyle.RuleToColor(nowRule);
            }
        }

        private void InitSubControl()
        {
            tbCrisscross.BackColor = ViewStyle.RuleToColor(eRule.crisscross);
            tbCrisscross.Tag = eRule.crisscross;
            tbCrisscross.Click += NowRuleClick;

            tbIll.BackColor = ViewStyle.RuleToColor(eRule.ill);
            tbIll.Tag = eRule.ill;
            tbIll.Click += NowRuleClick;

            tbCommon.BackColor = ViewStyle.RuleToColor(eRule.common);
            tbCommon.Tag = eRule.common;
            tbCommon.Click += NowRuleClick;

            tbFine.BackColor = ViewStyle.RuleToColor(eRule.fine);
            tbFine.Tag = eRule.fine;
            tbFine.Click += NowRuleClick;

            tbExcellent.BackColor = ViewStyle.RuleToColor(eRule.excellent);
            tbExcellent.Tag = eRule.excellent;
            tbExcellent.Click += NowRuleClick;

            NowRule = eRule.common;
        }

        public IList<VcRuleCell> Rules
        {
            get
            {
                IList<VcRuleCell> Result = new List<VcRuleCell>();
                foreach (RuleCell cell in Grid.eachCell())
                    if (cell.Rule != eRule.common)
                    {
                        VcRuleCell rt = new VcRuleCell();
                        rt.Rule = cell.Rule;
                        rt.Time.Week = cell.Time.Week;
                        rt.Time.BetideNode = cell.Time.BetideNode;
                        rt.Time.Order = cell.Time.Order;

                        Result.Add(rt);
                    }
                return Result;
            }
            set
            {
                foreach (RuleCell cell in Grid.eachCell())
                    cell.Rule = eRule.common;

                foreach (VcRuleCell v in value)
                    foreach (RuleCell cell in Grid.eachCell())
                        if (cell.Time.Week == v.Time.Week
                            && cell.Time.BetideNode == v.Time.BetideNode
                            && cell.Time.Order == v.Time.Order)
                            
                            cell.Rule = v.Rule;
            }
        }

        private void pnl_MouseMove(object sender, MouseEventArgs e)
        {
            if (ReadOnly)
                return;

            if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right)
                return;

            RuleCell Sender;
            Object sdr = this.panel1.GetChildAtPoint(this.panel1.PointToClient((sender as Control).PointToScreen(e.Location)));
            if (sdr is RuleCell)
                Sender = sdr as RuleCell;
            else
                return;

            if (e.Button == MouseButtons.Left)
                Sender.Rule = this.nowRule;
            else
                Sender.Rule = eRule.common;
        }
    }

    internal class RuleCell : Panel, IFcMatrixMatterCell
    {
        public RuleCell()
        {
            this.BorderStyle = BorderStyle.Fixed3D;
            this.BackColor = ViewStyle.RuleToColor(eRule.common);
        }

        private eRule rule;

        public eRule Rule
        {
            get { return rule; }
            set 
            {
                if (value != rule)
                {
                    rule = value;
                    this.BackColor = ViewStyle.RuleToColor(rule);
                }
            }
        }

        private VcTime time = new VcTime();

        public VcTime Time 
        {
            get { return time; } 
        }
    }
}
