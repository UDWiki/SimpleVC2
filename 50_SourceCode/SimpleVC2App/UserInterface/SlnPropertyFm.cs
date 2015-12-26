using System;
using System.Windows.Forms;
using SGLibrary.Extend.ControlEx;
using SGLibrary.Extend;
using Telossoft.SimpleVC.Model;

namespace Telossoft.SimpleVC.WinFormApp.UserInterface
{
    public partial class SlnPropertyFm : Form
    {
        public SlnPropertyFm()
        {
            InitializeComponent();
        }

        public EnSolution Value;

        private void SlnPropertyFm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i <= 6; i++)
                clActiveWeek.Items.Add(ExDateTime.DayOfWeekToChiese((DayOfWeek)i, "ÖÜ"), Value.ActiveWeekArr[i]);

            LsNum0.Value = Value.LessonNumberArr[0];
            LsNum1.Value = Value.LessonNumberArr[1];
            LsNum2.Value = Value.LessonNumberArr[2];
            LsNum3.Value = Value.LessonNumberArr[3];

            new FormMemoryImpl(this, VC2WinFmApp.Cfg, "FormRem");  //´°Ìå¼ÇÒä
        }

        private void SlnPropertyFm_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (int i = 0; i <= 6; i++)
                Value.ActiveWeekArr[i] = clActiveWeek.GetItemChecked(i);

            Value.LessonNumberArr[0] = (Int32)LsNum0.Value;
            Value.LessonNumberArr[1] = (Int32)LsNum1.Value;
            Value.LessonNumberArr[2] = (Int32)LsNum2.Value;
            Value.LessonNumberArr[3] = (Int32)LsNum3.Value;
        }
    }
}