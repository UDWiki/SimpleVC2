using SGLibrary.Framework.Config;

namespace Telossoft.SimpleVC.Model
{
    public interface IDataAccess
    {
        IGrpMbrDac<EnCourseGroup, EnCourse> Crs { get;}
        IGrpMbrDac<EnTeacherGroup, EnTeacher> Tch { get;}
        IGrpMbrDac<EnSquadGroup, EnSquad> Sqd { get;}

        ILsnDac Lsn { get;}
        IRuleDac Rule { get;}

        EnSolution Solution { get; set; }

        IConfig Config { get; }
    }
}
