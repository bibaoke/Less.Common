//bibaoke.com

using System.Threading;
using System;

namespace Less.MultiThread
{
    /// <summary>
    /// 异步执行任务
    /// </summary>
    public static class Asyn
    {
        /// <summary>
        /// 异步执行任务
        /// </summary>
        /// <param name="action">任务委托</param>
        public static void Exec(Action action)
        {
            new Thread(new ThreadStart(action)).Start();
        }

        /// <summary>
        /// 异步执行任务
        /// </summary>
        /// <param name="value">传递的值</param>
        /// <param name="action">任务委托</param>
        public static void Exec<T>(T value, Action<T> action)
        {
            new Thread(new ParameterizedThreadStart(i => action((T)i))).Start(value);
        }
    }
}
