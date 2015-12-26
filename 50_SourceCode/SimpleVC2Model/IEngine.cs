using System;
using System.Collections.Generic;

namespace Telossoft.SimpleVC.Model
{
    public delegate void dlgGroupChange(IList<BaseEntity> Groups);
    public delegate void dlgFailActChange(IList<EnFailAct> FailActs);
    public delegate void dlgModifiedChange();

    //���У�û�߼���ϵ
    public enum eScheduleUpdateKind { RefreshMatrix, RefreshAct, RefreshRule, Invalidate };
    public delegate void dlgScheduleUpdate(eScheduleUpdateKind Kind);

    public interface IEngine
    {
        event dlgGroupChange GroupChange;     //ȫУ��Ч����䶯��
        event dlgFailActChange FailActChange;    //ȫУ������ĿƱ䶯��
        event dlgModifiedChange ModifiedChange;     //�༭״̬�ı�
        event dlgScheduleUpdate ScheduleUpdate;        //�α���Ҫ���£��α��κ͸��ڿεĹ���
        IMbrSelect GetTchMbrSelect();
        IMbrSelect GetSqdMbrSelect();

        void Init();
        void InitUI();

        IList<BaseEntity> GetEnabledMember(BaseEntity Grp);
        Boolean Modified { get; }
        Boolean TimeIsEnabled(VcTime time);
        Boolean EntityIsEnabled(BaseEntity entity);  //��ǰʵ���Ƿ������壨û�б�ɾ����

        Boolean IsModified(BaseEntity entity);
        IList<EnLsnAct> GetFailLsnActs(EnSquad squad);

        DtMatrix<EnLsnAct> GetSqdMatrix(EnSquad squad);
        DtMatrix<eRule> GetSqdRule(EnSquad squad);
        //��õ�ǰ����̬�Ŀα�ĳ�ڵĹ����б����ͻ 
        IList<VcActEtyRelation> GetSqdClash(EnSquad squad, VcTime time);
        //��ȡ����ʱ����
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
