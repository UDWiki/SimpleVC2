using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using SGLibrary.DB;
using Telossoft.SimpleVC.Model;

namespace Telossoft.SimpleVC.WinFormApp.EntitySelect
{
    public partial class GrpSelectFm : Form
    {
        public GrpSelectFm()
        {
            InitializeComponent();
        }

        private IGrpSelect grpSlt;

        public IGrpSelect GrpSlt
        {
            get { return grpSlt; }
            set { grpSlt = value; }
        }
    }
}
