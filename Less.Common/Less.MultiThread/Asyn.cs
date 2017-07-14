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

        /// <summary>
        /// 异步执行多个任务
        /// </summary>
        /// <param name="actions">任务委托</param>
        public static void Exec(params Action[] actions)
        {
            Asyn.Exec(actions.Length, actions);
        }

        /// <summary>
        /// 异步执行多个任务
        /// </summary>
        /// <param name="threads">线程数</param>
        /// <param name="actions">任务委托</param>
        public static void Exec(int threads, params Action[] actions)
        {
            if (threads > 0)
            {
                Semaphore semaphore = new Semaphore(threads, threads);

                int count = actions.Length;

                object countLock = new object();

                foreach (Action i in actions)
                {
                    if (semaphore.WaitOne())
                    {
                        new Thread(new ThreadStart(() =>
                        {
                            try
                            {
                                i();
                            }
                            finally
                            {
                                semaphore.Release();

                                lock (countLock)
                                    count--;

                                if (count == 0)
                                    semaphore.Close();
                            }
                        })).Start();
                    }
                }
            }
        }
    }
}
