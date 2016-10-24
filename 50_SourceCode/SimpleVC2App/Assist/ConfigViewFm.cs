using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telossoft.SimpleVC.Model;
using Telossoft.SimpleVC.WinFormApp.Facade;

namespace Telossoft.SimpleVC.WinFormApp.Assist
{
    public partial class ConfigViewFm : Form
    {
        public ConfigViewFm()
        {
            InitializeComponent();
        }

        private void tbCrisscross_DoubleClick(object sender, EventArgs e)
        {
            ColorDialog Dlg = new ColorDialog();
            Dlg.Color = tbCrisscross.BackColor;

            if (Dlg.ShowDialog() == DialogResult.OK)
                tbCrisscross.BackColor = Dlg.Color;
        }

        private void tbIll_DoubleClick(object sender, EventArgs e)
        {
            ColorDialog Dlg = new ColorDialog();
            Dlg.Color = tbIll.BackColor;

            if (Dlg.ShowDialog() == DialogResult.OK)
                tbIll.BackColor = Dlg.Color;
        }

        private void tbFine_DoubleClick(object sender, EventArgs e)
        {
            ColorDialog Dlg = new ColorDialog();
            Dlg.Color = tbFine.BackColor;

            if (Dlg.ShowDialog() == DialogResult.OK)
                tbFine.BackColor = Dlg.Color;
        }

        private void tbExcellent_DoubleClick(object sender, EventArgs e)
        {
            ColorDialog Dlg = new ColorDialog();
            Dlg.Color = tbExcellent.BackColor;

            if (Dlg.ShowDialog() == DialogResult.OK)
                tbExcellent.BackColor = Dlg.Color;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tbCrisscross.BackColor = ViewStyle.cRG_Crisscross;
            tbIll.BackColor = ViewStyle.cRG_Ill;
            tbFine.BackColor = ViewStyle.cRG_Fine;
            tbExcellent.BackColor = ViewStyle.cRG_Excellent;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tbCrisscross.BackColor = ViewStyle.cRB_Crisscross;
            tbIll.BackColor = ViewStyle.cRB_Ill;
            tbFine.BackColor = ViewStyle.cRB_Fine;
            tbExcellent.BackColor = ViewStyle.cRB_Excellent;
        }

        private void ConfigViewFm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
                return;

            ViewStyle.Color_Crisscross = tbCrisscross.BackColor;
            ViewStyle.Color_Ill = tbIll.BackColor;
            ViewStyle.Color_Fine = tbFine.BackColor;
            ViewStyle.Color_Excellent =  tbExcellent.BackColor;

            ViewStyle.Horizontal = this.radioButton2.Checked;
            ViewStyle.Description = this.textBox1.Text;

            SaveViewStyle();
        }

        private void ConfigViewFm_Load(object sender, EventArgs e)
        {
            tbCrisscross.BackColor = ViewStyle.Color_Crisscross;
            tbIll.BackColor = ViewStyle.Color_Ill;
            tbFine.BackColor = ViewStyle.Color_Fine;
            tbExcellent.BackColor = ViewStyle.Color_Excellent;
            
            radioButton1.Checked = !ViewStyle.Horizontal;
            radioButton2.Checked = ViewStyle.Horizontal;
            textBox1.Text = ViewStyle.Description;

        }

        private const String CfgSection = "ViewStyleSection";

        public static void LoadViewStyle()
        {
            ViewStyle.Color_Crisscross
                = Color.FromArgb(VC2WinFmApp.Cfg.TryGetInt32(CfgSection, "Color_Crisscross", ViewStyle.cRG_Crisscross.ToArgb()));
            ViewStyle.Color_Ill
                = Color.FromArgb(VC2WinFmApp.Cfg.TryGetInt32(CfgSection, "Color_Ill", ViewStyle.cRG_Ill.ToArgb()));
            ViewStyle.Color_Fine
                = Color.FromArgb(VC2WinFmApp.Cfg.TryGetInt32(CfgSection, "Color_Fine", ViewStyle.cRG_Fine.ToArgb()));
            ViewStyle.Color_Excellent
                = Color.FromArgb(VC2WinFmApp.Cfg.TryGetInt32(CfgSection, "Color_Excellent", ViewStyle.cRG_Excellent.ToArgb()));

            ViewStyle.Horizontal
                = VC2WinFmApp.Cfg.TryGetBoolean(CfgSection, "Horizontal", ViewStyle.Horizontal);
            ViewStyle.Description
                = VC2WinFmApp.Cfg.TryGetString(CfgSection, "Description", ViewStyle.Description);
        }

        public static void SaveViewStyle()
        {
            VC2WinFmApp.Cfg.SetInt32(CfgSection, "Color_Crisscross", ViewStyle.Color_Crisscross.ToArgb());
            VC2WinFmApp.Cfg.SetInt32(CfgSection, "Color_Ill", ViewStyle.Color_Ill.ToArgb());
            VC2WinFmApp.Cfg.SetInt32(CfgSection, "Color_Fine", ViewStyle.Color_Fine.ToArgb());
            VC2WinFmApp.Cfg.SetInt32(CfgSection, "Color_Excellent", ViewStyle.Color_Excellent.ToArgb());

            VC2WinFmApp.Cfg.SetBoolean(CfgSection, "Horizontal", ViewStyle.Horizontal);
            VC2WinFmApp.Cfg.SetString(CfgSection, "Description", ViewStyle.Description);
        }
    }
}
