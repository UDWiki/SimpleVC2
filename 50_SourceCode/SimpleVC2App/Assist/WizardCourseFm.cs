using System;
using System.Windows.Forms;
using Telossoft.SimpleVC.Model;


namespace Telossoft.SimpleVC.WinFormApp.Assist
{
    public partial class WizardCourseFm : Form
    {
        public WizardCourseFm()
        {
            InitializeComponent();
        }

        public static Boolean WizardAddCourse(ref Int32 AddCount)
        {
            AddCount = 0;

            WizardCourseFm fm = new WizardCourseFm();
            if (fm.ShowDialog() != DialogResult.OK)
                return false;

            foreach (String s in fm.CourseList.CheckedItems)
            {
                if (fm.cbIfExistThenIgnore.Checked && VC2WinFmApp.DataRule.Crs.MbrNameExist(null, s))
                    continue;

                EnCourse crs = new EnCourse();
                crs.Name = s;
                if (VC2WinFmApp.DataRule.Crs.SaveNewMbr(crs) == null)
                    break;

                AddCount++;
            }

            return AddCount > 0;
        }

        private void CourseList_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void CourseList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
                for(Int32 i  = 0; i < CourseList.Items.Count; i++)
                    CourseList.SetItemChecked(i, true);
        }
    }
}