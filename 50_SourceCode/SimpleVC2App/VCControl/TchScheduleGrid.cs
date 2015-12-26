using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SGLibrary.Extend;
using Telossoft.SimpleVC.Model;

namespace Telossoft.SimpleVC.WinFormApp.VCControl
{
    internal class TchScheduleGrid : VcGridV2<TchScheduleCell>
    {
        public TchScheduleGrid(Control parent)
            : base(parent)
        { 
        }
    }

    internal class TchScheduleCell : Label, IFcMatrixMatterCell
    {
        public TchScheduleCell()
        {
            this.AutoSize = false;
            this.TextAlign = ContentAlignment.MiddleCenter;
            this.BorderStyle = BorderStyle.Fixed3D;
        }

        private VcTime time = new VcTime();

        public VcTime Time
        {
            get { return time; }
        }

        private eRule rule;

        public eRule Rule
        {
            get { return rule; }
            set
            {
                rule = value;
                if (acts.Count > 1)
                    this.ForeColor = ViewStyle.AdvantageToColor(eRule.crisscross);
                else
                    this.ForeColor = ViewStyle.AdvantageToColor(rule);
            }
        }

        private IList<EnLsnAct> acts = new List<EnLsnAct>();

        public IList<EnLsnAct> Acts
        {
            get { return acts; }
            set
            {
                acts.Clear();
                ExIList.Append<EnLsnAct>(value, acts);
                String S = "";
                foreach (EnLsnAct act in acts)
                {
                    if (!String.IsNullOrEmpty(S))
                        S = S + Ex.cEntter;
                    S = S + act.ClsLesson.Name;
                }

                this.Text = S;
                Rule = Rule;
            }
        }
    }
}
