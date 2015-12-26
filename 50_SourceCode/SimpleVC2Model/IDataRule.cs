using System;

namespace Telossoft.SimpleVC.Model
{
    public interface IDataRule
    {
        //SlnChanged > DataChange กต RuleChange
        event dlgAfterBizChanged SlnChanged;
        event dlgAfterBizChanged DataChanged;
        event dlgAfterBizChanged RuleChanged;

        void LockRefresh();
        void unLockRefresh();

        EnSolution Solution { get; set; }
        Boolean ReadOnly { get; set; }

        ICrsDataRule Crs { get;}
        ITchDataRule Tch { get;}
        ISqdDataRule Sqd { get; }

        ILsnDataRule Lsn { get;}
        IRuleDataRule Rule { get;}

        void SysInit();
    }

    public delegate void dlgAfterBizChanged();



    public interface ICrsDataRule : IGrpMbrDataRule<EnCourseGroup, EnCourse>
    {
    }

    public interface ITchDataRule : IGrpMbrDataRule<EnTeacherGroup, EnTeacher>
    {
    }

    public interface ISqdDataRule : IGrpMbrDataRule<EnSquadGroup, EnSquad>
    {
    }
}
