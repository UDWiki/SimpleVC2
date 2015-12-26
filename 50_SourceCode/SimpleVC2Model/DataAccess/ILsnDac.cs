using System.Collections.Generic;

namespace Telossoft.SimpleVC.Model
{
    public interface ILsnDac
    {
        EnLesson SaveLesson(EnLesson Value);
        IList<EnLesson> GetLessons(EnSquadGroup SquadGrp);
        void DeleteLesson(EnLesson Lesson);

        EnClsLesson SaveClsLesson(EnClsLesson Value);
        IList<EnClsLesson> GetClsLessons(EnLesson Lesson);
        void DeleteClsLesson(EnClsLesson Value);

        void SaveLsnActs(IList<EnLsnAct> Values);
        IList<EnLsnAct> GetLsnActs(EnClsLesson ClsLsn);
        void DeleteLsnActs(IList<EnLsnAct> Values);

        //为处理数据关联逻辑
        IEnumerable<EnLesson> eachLesson();
        IEnumerable<EnClsLesson> eachClsLesson();
        IEnumerable<EnLsnAct> eachLsnAct();

        void ClearAll();
    }

}
