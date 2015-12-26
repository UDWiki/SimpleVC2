using System;
using System.Collections.Generic;

namespace Telossoft.SimpleVC.Model
{
    public delegate void dlgGroupChange(IList<BaseEntity> Groups);
    public delegate void dlgFailActChange(IList<EnFailAct> FailActs);
    public delegate void dlgModifiedChange();

    //并行，没逻辑关系
    public enum eScheduleUpdateKind { RefreshMatrix, RefreshAct, RefreshRule, Invalidate };
    public delegate void dlgScheduleUpdate(eScheduleUpdateKind Kind);

    public interface IEngine
    {
        event dlgGroupChange GroupChange;     //全校有效的组变动了
        event dlgFailActChange FailActChange;    //全校待处理的科变动了
        event dlgModifiedChange ModifiedChange;     //编辑状态改变
        event dlgScheduleUpdate ScheduleUpdate;        //课表需要更新（课表、课和各节课的规则）
        IMbrSelect GetTchMbrSelect();
        IMbrSelect GetSqdMbrSelect();

        void Init();
        void InitUI();

        IList<BaseEntity> GetEnabledMember(BaseEntity Grp);
        Boolean Modified { get; }
        Boolean TimeIsEnabled(VcTime time);
        Boolean EntityIsEnabled(BaseEntity entity);  //当前实体是否还有意义（没有被删除）

        Boolean IsModified(BaseEntity entity);
        IList<EnLsnAct> GetFailLsnActs(EnSquad squad);

        DtMatrix<EnLsnAct> GetSqdMatrix(EnSquad squad);
        DtMatrix<eRule> GetSqdRule(EnSquad squad);
        //获得当前虚拟态的课表某节的规则列表与冲突 
        IList<VcActEtyRelation> GetSqdClash(EnSquad squad, VcTime time);
        //获取交换时优势
        DtMatrix<eRule> GetActChangeRule(EnSquad squad, 
            EnLsnAct sAct, VcTime tTime);

        DtMatrix<IList<EnLsnAct>> GetTchMatrix(EnTeacher teacher);

        void Save(EnSquad squad);
        void SaveAll(Boolean updateInterflow);
        void Cancel(EnSquad squad);
        void CancelAll(Boolean updateInterflow);
        void Move(EnSquad squad, EnLsnAct sAct, VcTime tTime);

        void Optimization(IList<EnSquad> Squads, Boolean LockIsEnabled, Boolean AutoSave);
        void Automatic(IList<EnSquad> Squads, Boolean OnlyFail, Boolean LockIsEnabled, Boolean AutoSave);

    }
}
