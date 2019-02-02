//bibaoke.com

using System;
using System.Threading;

namespace Less.MultiThread
{
    /// <summary>
    /// 同步方法
    /// </summary>
    public static class Syn
    {
        /// <summary>
        /// 在限定的时间内
        /// 等待函数返回 true
        /// 函数的执行时间必须小于限定时间
        /// </summary>
        /// <param name="span"></param>
        /// <param name="func"></param>
        /// <exception cref="TimeoutException"></exception>
        public static void Wait(TimeSpan span, Func<bool> func)
        {
            DateTime begin = DateTime.Now;

            while (!func())
            {
                if (DateTime.Now - begin > span)
                {
                    throw new TimeoutException();
                }

                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// 等待函数返回 true
        /// </summary>
        /// <param name="func"></param>
        public static void Wait(Func<bool> func)
        {
            while (!func())
            {
                Thread.Sleep(100);
            }
        }
    }
}
