using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Telossoft.SimpleVC.WinFormApp.Assist
{
    public partial class WizardSquadFm : Form
    {
        public WizardSquadFm()
        {
            InitializeComponent();
        }

        private IList<CheckBox> CheckBoxes = new List<CheckBox>();

        private void WizardSquadFm_Load(object sender, EventArgs e)
        {
            checkBox1.Tag = numericUpDown1;
            CheckBoxes.Add(checkBox1);

            checkBox2.Tag = numericUpDown2;
            CheckBoxes.Add(checkBox2);

            checkBox3.Tag = numericUpDown3;
            CheckBoxes.Add(checkBox3);

            checkBox4.Tag = numericUpDown4;
            CheckBoxes.Add(checkBox4);

            checkBox5.Tag = numericUpDown5;
            CheckBoxes.Add(checkBox5);

            checkBox6.Tag = numericUpDown6;
            CheckBoxes.Add(checkBox6);

            checkBox7.Tag = numericUpDown7;
            CheckBoxes.Add(checkBox7);

            checkBox8.Tag = numericUpDown8;
            CheckBoxes.Add(checkBox8);

            checkBox9.Tag = numericUpDown9;
            CheckBoxes.Add(checkBox9);

            checkBox10.Tag = numericUpDown10;
            CheckBoxes.Add(checkBox10);

            checkBox11.Tag = numericUpDown11;
            CheckBoxes.Add(checkBox11);

            checkBox12.Tag = numericUpDown12;
            CheckBoxes.Add(checkBox12);

            checkBox13.Tag = numericUpDown13;
            CheckBoxes.Add(checkBox13);

            checkBox14.Tag = numericUpDown14;
            CheckBoxes.Add(checkBox14);

            foreach (CheckBox cb in CheckBoxes)
            {
                (cb.Tag as NumericUpDown).Enabled = false;
                cb.CheckedChanged += CheckStateChanged;
            }
        }

        private void CheckStateChanged(object sender, EventArgs e)
        {
            ((sender as CheckBox).Tag as NumericUpDown).Enabled = (sender as CheckBox).Checked;
        }

        public static Boolean WizardAddSquad(ref Int32 AddCount)
        {
            AddCount = 0;

            WizardSquadFm fm = new WizardSquadFm();
            if (fm.ShowDialog() != DialogResult.OK)
                return false;

            foreach (CheckBox cb in fm.CheckBoxes)
                if (cb.Checked)
                {
                    Int32 addcnt = VC2WinFmApp.DataFacade.Sqd.InitSqd(cb.Text,
                        (Int32)(cb.Tag as NumericUpDown).Value, fm.cbIfExistThenIgnore.Checked);
                    if (addcnt < 0)
                        break;

                    AddCount = AddCount + addcnt;
                }

            return AddCount > 0;
        }

    }
}