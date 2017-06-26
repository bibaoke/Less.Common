//bibaoke.com

using System;
using System.Threading;

namespace Less.MultiThread
{
    /// <summary>
    /// 任务代理 
    /// </summary>
    public class Agent
    {
        private Action<DateTime> Job
        {
            get;
            set;
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="action"></param>
        public Agent(Action action)
        {
            this.Job = ending => action();
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="action">任务委托</param>
        public Agent(Action<DateTime> action)
        {
            this.Job = action;
        }

        /// <summary>
        /// 每分钟执行
        /// </summary>
        public void ExecMinutely()
        {
            this.Exec(DateTime.Now, DateTime.MaxValue, new TimeSpan(0, 1, 0), DateTime.MaxValue);
        }

        /// <summary>
        /// 每小时执行
        /// </summary>
        /// <param name="beginningMinute"></param>
        /// <param name="endingMinute"></param>
        public void ExecHourly(int beginningMinute, int endingMinute)
        {
            this.ExecHourly(beginningMinute, endingMinute, DateTime.MaxValue);
        }

        /// <summary>
        /// 每小时执行
        /// </summary>
        /// <param name="beginningMinute"></param>
        /// <param name="endingMinute"></param>
        /// <param name="terminal"></param>
        public void ExecHourly(int beginningMinute, int endingMinute, DateTime terminal)
        {
            DateTime now = DateTime.Now;

            DateTime beginning = new DateTime(now.Year, now.Month, now.Day, now.Hour, beginningMinute, 0);
            DateTime ending = new DateTime(now.Year, now.Month, now.Day, now.Hour, endingMinute, 0);

            if (beginning >= ending)
                ending = ending.AddHours(1);

            this.Exec(beginning, ending, new TimeSpan(1, 0, 0), terminal);
        }

        /// <summary>
        /// 每天执行
        /// </summary>
        /// <param name="beginningHour"></param>
        /// <param name="tomorrow"></param>
        public void ExecDaily(int beginningHour, bool tomorrow)
        {
            this.ExecDaily(beginningHour, 0, tomorrow);
        }

        /// <summary>
        /// 每天执行
        /// </summary>
        /// <param name="beginningHour"></param>
        /// <param name="beginningMinute"></param>
        /// <param name="tomorrow"></param>
        public void ExecDaily(int beginningHour, int beginningMinute, bool tomorrow)
        {
            DateTime beginning = tomorrow ? DateTime.Now.AddDays(1) : DateTime.Now;

            this.Exec(
                new DateTime(beginning.Year, beginning.Month, beginning.Day, beginningHour, beginningMinute, 0),
                DateTime.MaxValue,
                new TimeSpan(24, 0, 0),
                DateTime.MaxValue);
        }

        /// <summary>
        /// 每日执行
        /// </summary>
        /// <param name="beginningHour"></param>
        /// <param name="beginningMinute"></param>
        /// <param name="endingHour"></param>
        /// <param name="endingMinute"></param>
        public void ExecDaily(int beginningHour, int beginningMinute, int endingHour, int endingMinute)
        {
            this.ExecDaily(beginningHour, beginningMinute, endingHour, endingMinute, DateTime.MaxValue);
        }

        /// <summary>
        /// 每日执行
        /// </summary>
        /// <param name="beginningHour"></param>
        /// <param name="beginningMinute"></param>
        /// <param name="endingHour"></param>
        /// <param name="endingMinute"></param>
        /// <param name="terminal"></param>
        public void ExecDaily(int beginningHour, int beginningMinute, int endingHour, int endingMinute, DateTime terminal)
        {
            DateTime now = DateTime.Now;

            DateTime beginning = new DateTime(now.Year, now.Month, now.Day, beginningHour, beginningMinute, 0);
            DateTime ending = new DateTime(now.Year, now.Month, now.Day, endingHour, endingMinute, 0);

            if (beginning >= ending)
                ending = ending.AddDays(1);

            this.Exec(beginning, ending, new TimeSpan(24, 0, 0), terminal);
        }

        /// <summary>
        /// 周期性执行
        /// </summary>
        /// <param name="beginning"></param>
        /// <param name="ending"></param>
        /// <param name="period"></param>
        /// <param name="terminal"></param>
        public void Exec(DateTime beginning, DateTime ending, TimeSpan period, DateTime terminal)
        {
            if (beginning >= ending)
                throw new ArgumentException();

            this.CheckDateTime(ref beginning, ref ending, period);

            bool finished = false;

            while (true)
            {
                if (DateTime.Now >= terminal)
                    return;

                if (DateTime.Now >= beginning)
                {
                    if (!finished)
                        this.Job(ending);

                    finished = !this.CheckDateTime(ref beginning, ref ending, period);
                }

                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// 检查并调整开始结束时间
        /// </summary>
        /// <param name="beginning"></param>
        /// <param name="ending"></param>
        /// <param name="period"></param>
        /// <returns>时间是否经过调整</returns>
        private bool CheckDateTime(ref DateTime beginning, ref DateTime ending, TimeSpan period)
        {
            DateTime now = DateTime.Now;

            if (now >= ending || now >= beginning + period)
            {
                beginning = this.CheckDateTime(now, beginning, period);
                ending = this.CheckDateTime(now, ending, period);

                return true;
            }

            return false;
        }

        /// <summary>
        /// 检查并调整传入的时间在现在之后
        /// </summary>
        /// <param name="now"></param>
        /// <param name="datetime"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        private DateTime CheckDateTime(DateTime now, DateTime datetime, TimeSpan period)
        {
            if (now >= datetime)
            {
                datetime = datetime.Add(period);

                this.CheckDateTime(now, datetime, period);
            }

            return datetime;
        }
    }
}