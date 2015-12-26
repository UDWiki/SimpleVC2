using System;
using System.Collections.Generic;
using SGLibrary.Extend;
using Telossoft.SimpleVC.Model;

namespace Telossoft.SimpleVC.BizLogic.EngineV2
{
    public partial class EngineV2
    {
        class MbrSelect : IMbrSelect
        {
            private EngineV2 engine;
            private Type type;
            protected BaseEntity AllEty;

            public MbrSelect(EngineV2 engine, Type type)
            {
                this.type = type;
                this.engine = engine;
                AllEty = new BaseEntity();
                AllEty.Name = "所有" + Kind;
            }

            public string Kind
            {
                get 
                {
                    if (type == typeof(EnSquad))
                        return "班级";
                    else if (type == typeof(EnTeacher))
                        return "教师";
                    else
                        throw new Exception("未知类型");
                }
            }

            public IList<BaseEntity> GroupList
            {
                get 
                { 
                    IList<BaseEntity> Result = new List<BaseEntity>();
                    Result.Add(AllEty);
                    if (type == typeof(EnSquad))
                        foreach (BaseEntity ety in engine.GroupList)
                        {
                            if (ety is EnSquadGroup)
                                Result.Add(ety);
                        }
                    else if (type == typeof(EnTeacher))
                        foreach (BaseEntity ety in engine.GroupList)
                            if (ety is EnTeacherGroup)
                                Result.Add(ety);

                    return Result;
                }
            }

            public IList<BaseEntity> GetMbrList(BaseEntity grp)
            {
                if (grp == AllEty)
                {
                    if (type == typeof(EnSquad))
                        return new GIListTypeChange<EnSquad, BaseEntity>(this.engine.SquadList);
                    else if (type == typeof(EnTeacher))
                        return new GIListTypeChange<EnTeacher, BaseEntity>(this.engine.TeacherList);
                    else
                        throw new Exception("未知类型");
                }
                else
                {
                    if (type == typeof(EnSquad))
                        return this.engine.GetEnabledMember(grp as EnSquadGroup);
                    else if (type == typeof(EnTeacher))
                        return this.engine.GetEnabledMember(grp as EnTeacherGroup);
                    else
                        throw new Exception("未知类型");
                }
            }
        }

        public IMbrSelect GetTchMbrSelect()
        { 
            return new MbrSelect(this, typeof(EnTeacher));
        }

        public IMbrSelect GetSqdMbrSelect()
        {
            return new MbrSelect(this, typeof(EnSquad));
        }
    }
}
