using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Telossoft.SimpleVC.Model;

using Telossoft.SimpleVC.WinFormApp.Facade;

namespace Telossoft.SimpleVC.WinFormApp.EntitySelect
{
    public static class SelectIF
    {
        public static Boolean SquadGroupSelect(ref EnSquadGroup select)
        {
            GrpSelectFmSingle Fm = new GrpSelectFmSingle();
            Fm.GrpSlt = new IGrpMbrBizFacadeToIGrpSelect(VC2WinFmApp.DataFacade.Sqd);
            Fm.SelectEty = select;
            if (Fm.ShowDialog() == DialogResult.OK)
            {
                select = Fm.SelectEty as EnSquadGroup;
                return true;
            }
            else
                return false;
        }

        public static IList<BaseEntity> GroupSelectMulti(IGrpMbrDataFacade GrpMbr,
            IList<BaseEntity> OldSelectItems)
        {
            GrpSelectFmMulti Fm = new GrpSelectFmMulti();
            Fm.GrpSlt = new IGrpMbrBizFacadeToIGrpSelect(GrpMbr);
            Fm.SetOldSelectList(OldSelectItems);

            if (Fm.ShowDialog() == DialogResult.OK)
                return Fm.GetSelectEties();
            else
                return null;
        }

        //自动保存前次选择
        private static EnTeacher OldTch;
        
        public static Boolean TeacherSelect(ref EnTeacher select)
        {
            MbrSelectFmSingle Fm = new MbrSelectFmSingle();
            Fm.btNull.Text = "无教师";
            Fm.btNull.Visible = true;
            Fm.MbrSelect = new IGrpMbrBizFacadeToIMbrSelect(VC2WinFmApp.DataFacade.Tch);
            Fm.SelectEty = select == null ? OldTch : select;
            if (Fm.ShowDialog() == DialogResult.OK)
            {
                select = Fm.SelectEty as EnTeacher;
                OldTch = select;

                return true;
            }
            else
                return false;
        }


        public static IList<BaseEntity> MemberSelectMulti(IGrpMbrDataFacade GrpMbr,
            IList<BaseEntity> OldSelectItems)
        {
            MbrSelectFmMulti Fm = new MbrSelectFmMulti();
            Fm.MbrSelect = new IGrpMbrBizFacadeToIMbrSelect(GrpMbr);
            Fm.SetOldSelectList(OldSelectItems);

            if (Fm.ShowDialog() == DialogResult.OK)
                return Fm.GetSelectEties();
            else
                return null;
        }
    }
}
