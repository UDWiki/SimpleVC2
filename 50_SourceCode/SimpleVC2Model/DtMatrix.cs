using System;
using System.Collections.Generic;

//矩形的课表
namespace Telossoft.SimpleVC.Model
{
    public class DtMatrixFull<T>
    {
        protected T[, ,] matrix = new T[(Int32)DayOfWeek.Saturday + 1, 
            (Int32)eBetideNode.Evening + 1, 
            VcTimeLogic.cMaxTimeOrder + 1];

        public virtual T this[VcTime time]
        {
            get 
            { 
                return matrix[(Int32)time.Week, (Int32)time.BetideNode, time.Order]; 
            }

            set 
            { 
                matrix[(Int32)time.Week, (Int32)time.BetideNode, time.Order] = value; 
            }
        }
    }

    public class DtMatrix<T> : DtMatrixFull<T>
    {
        public DtMatrix(EnSolution Sln)
        {
            becloud = new DtMatrixFull<Boolean>();

            VcTime time = new VcTime();
            for (time.Week = DayOfWeek.Sunday; time.Week <= DayOfWeek.Saturday; time.Week++)
                for (time.BetideNode = eBetideNode.Morn; time.BetideNode <= eBetideNode.Evening; time.BetideNode++)
                    for (time.Order = 1; time.Order <= VcTimeLogic.cMaxTimeOrder; time.Order++)
                        if (Sln.ActiveWeekArr[(Int32)time.Week]
                            && time.Order <= Sln.LessonNumberArr[(Int32)time.BetideNode])
                            becloud[time] = true;
        }

        protected DtMatrixFull<Boolean> becloud;

        public IEnumerable<T> eachElement()
        {
            foreach (VcTime time in eachTime())
                yield return this[time];
        }

        public IEnumerable<VcTime> eachTime()
        {
            VcTime time = new VcTime();
            VcTime Result = new VcTime();
            
            //这个循环内外次序要关注，合适的次序排出来的课表更好
            for (time.Order = 1; time.Order <= VcTimeLogic.cMaxTimeOrder; time.Order++)
                for (time.BetideNode = eBetideNode.Morn; time.BetideNode <= eBetideNode.Evening; time.BetideNode++)
                    for (time.Week = DayOfWeek.Sunday; time.Week <= DayOfWeek.Saturday; time.Week++)
                        if (becloud[time])
                        {
                            Result.Week = time.Week;
                            Result.BetideNode = time.BetideNode;
                            Result.Order = time.Order;

                            yield return Result;
                        }
        }

        public Boolean TestTime(VcTime time)
        {
            return time != null && becloud[time];
        }

        public override T this[VcTime time]
        {
            get
            {
                if (becloud[time])
                    return base[time];
                else
                    throw new Exception("无效的时间！");
            }
            set
            {
                if (becloud[time])
                    base[time] = value;
                else  
                    throw new Exception("无效的时间！");
            }
        }
    }
}
