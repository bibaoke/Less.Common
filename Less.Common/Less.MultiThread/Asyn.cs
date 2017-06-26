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
            Asyn.Exec(value, action, null);
        }

        /// <summary>
        /// 异步执行任务
        /// </summary>
        /// <param name="value">传递的值</param>
        /// <param name="action">任务委托</param>
        /// <param name="onError">出错处理委托</param>
        public static void Exec<T>(T value, Action<T> action, Action<Exception> onError)
        {
            new Thread(new ParameterizedThreadStart(delegate (object o)
            {
                try
                {
                    action((T)o);
                }
                catch (Exception ex)
                {
                    if (onError.IsNull())
                        throw;
                    else
                        onError(ex);
                }
            })).Start(value);
        }

        /// <summary>
        /// 异步执行多个任务
        /// </summary>
        /// <param name="actions">任务委托</param>
        public static void Exec(params Action[] actions)
        {
            if (actions.IsNotNull())
                Asyn.Exec(actions.Length, actions);
        }

        /// <summary>
        /// 异步执行多个任务
        /// </summary>
        /// <param name="threads">线程数</param>
        /// <param name="actions">任务委托</param>
        public static void Exec(int threads, params Action[] actions)
        {
            if (actions.IsNotNull() && threads > 0)
            {
                int available = threads;

                object availableLock = new object();

                foreach (Action i in actions)
                {
                    while (true)
                    {
                        if (available > 0)
                        {
                            lock (availableLock)
                            {
                                if (available > 0)
                                    available--;
                                else
                                    continue;
                            }

                            Asyn.Exec(i, action =>
                            {
                                try
                                {
                                    action();
                                }
                                finally
                                {
                                    lock (availableLock)
                                        available++;
                                }
                            });

                            break;
                        }
                        else
                        {
                            Thread.Sleep(100);
                        }
                    }
                }

                while (true)
                {
                    if (available == threads)
                        break;
                    else
                        Thread.Sleep(100);
                }
            }
        }
    }
}
