using System;
using System.Windows.Forms;
using SGLibrary.Extend;
using Telossoft.SimpleVC.Model;


namespace Telossoft.SimpleVC.WinFormApp.Assist
{
    public partial class WizardTeacherFm : Form
    {
        public WizardTeacherFm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            String[] names = textBox1.Text.Split(new String[] { Ex.cEntter }, StringSplitOptions.RemoveEmptyEntries);
            foreach (String s in names)
            {
                String name = s.Trim();
                if (!String.IsNullOrEmpty(name))
                    listBox1.Items.Add(name);
            }
        }

        public static Boolean WizardAddTeacher(ref Int32 AddCount)
        {
            AddCount = 0;

            WizardTeacherFm fm = new WizardTeacherFm();
            if (fm.ShowDialog() != DialogResult.OK)
                return false;

            foreach (String s in fm.listBox1.Items)
            {
                String name = s.Trim();
                if (String.IsNullOrEmpty(name))
                    continue;

                if (fm.cbIfExistThenIgnore.Checked && VC2WinFmApp.DataRule.Tch.MbrNameExist(null, name))
                    continue;

                EnTeacher tch = new EnTeacher();
                tch.Name = name;
                if (VC2WinFmApp.DataRule.Tch.SaveNewMbr(tch) == null)
                    break;
                
                AddCount++;
            }

            return AddCount > 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String S = "";
            String mname = "张王李赵";
            for(Int32 i = 0; i <mname.Length; i++)
                for(Int32 j = 1; j <= 10; j++)
                    S = S + mname[i] + j + Ex.cEntter;

            textBox1.Text = S;
        }
    }
}
