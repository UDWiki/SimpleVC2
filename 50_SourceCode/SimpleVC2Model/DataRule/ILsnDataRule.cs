using System.Collections.Generic;

namespace Telossoft.SimpleVC.Model
{
    public interface ILsnDataRule
    {
        IList<EnLesson> GetLessons(EnSquadGroup SquadGrp);
        IList<EnClsLesson> GetCLsLessons(EnLesson lsn);

        void RemoveLsn(EnLesson Lsn);
        void SaveLsnTree(EnLesson Lsn, IList<EnClsLesson> ClsLsns);

        //为处理操作后果分析
        IEnumerable<EnClsLesson> eachClsLesson();


        IEnumerable<EnLsnAct> eachLsnAct();

        IList<EnLsnAct> GetLsnActs(BaseEntity Ety);
        void SaveLsnActs(IList<EnLsnAct> Values);

        void ClearAllLesson();
    }
}
