using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using SGLibrary.Extend;
using Telossoft.SimpleVC.Model;

namespace Telossoft.SimpleVC.WinFormApp.Print
{
    internal class VcPrintDocument : PrintDocument
    {
        private VcPrintDocument()
        {
        }

        private static VcPrintDocument Instance;

        public static VcPrintDocument GetInstance()
        {
            if (Instance == null)
                Instance = new VcPrintDocument();

            return Instance;
        }

        private IList<BaseEntity> entitis = new List<BaseEntity>();

        public IList<BaseEntity> Entitis
        {
            get { return entitis; }
            set 
            {
                this.entitis.Clear();
                ExIList.Append<BaseEntity>(value, this.entitis);
            }
        }

        private Int32 PrtIdx = 0;
        protected override void OnBeginPrint(PrintEventArgs e)
        {
            PrtIdx = 0;
        }

        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            if (PrtIdx < entitis.Count)
            {
                BaseEntity ety = entitis[PrtIdx];
                IPrtMatrix pm;

                if (ety is EnTeacher)
                    pm = new PrtMatrixTch(ety as EnTeacher);
                else if (ety is EnSquad)
                    pm = new PrtMatrixSqd(ety as EnSquad);
                else
                    pm = new PrtMatrixTest(ety);

                pm.PrintMatrix(this, e.Graphics, e.MarginBounds);
                
                PrtIdx++;
                e.HasMorePages = PrtIdx < entitis.Count;
            }
        }
    }
}
