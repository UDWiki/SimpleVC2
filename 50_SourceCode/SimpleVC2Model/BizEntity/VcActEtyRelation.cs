using System;

namespace Telossoft.SimpleVC.Model
{
    public enum eActEtyRelation { teach, clash, rule };
    public class VcActEtyRelation
    {
        public VcActEtyRelation(BaseEntity entity, eActEtyRelation relation)
        {
            this.entity = entity;
            this.relation = relation;
        }

        public VcActEtyRelation(BaseEntity entity, eActEtyRelation relation, eRule rule)
            : this(entity, relation)
        {
            this.rule = rule;
        }

        private BaseEntity entity;

        public BaseEntity Entity
        {
            get { return entity; }
        }

        private eActEtyRelation relation;

        public eActEtyRelation Relation
        {
            get { return relation; }
        }

        public String RelationDsp
        {
            get
            {
                switch (relation)
                {
                    case eActEtyRelation.teach:
                        return "授课教师";
                    case eActEtyRelation.clash:
                        return "冲突";
                    default:
                        return "规则";
                }
            }
        }

        private eRule rule;

        public eRule Rule
        {
            get { return rule; }
        }
    }

}
