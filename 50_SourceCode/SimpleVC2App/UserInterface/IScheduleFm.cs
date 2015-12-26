using System;
using Telossoft.SimpleVC.Model;

namespace Telossoft.SimpleVC.WinFormApp.UserInterface
{

    public delegate void dlgOpenScheduleFm(BaseEntity entity);

    public interface IScheduleFm
    {
        BaseEntity Entity { get; set; }
        Boolean ReadOnly { get; }

        event dlgOpenScheduleFm OpenScheduleFm;

        void ScheduleUpdate(eScheduleUpdateKind Kind);
    }
}
