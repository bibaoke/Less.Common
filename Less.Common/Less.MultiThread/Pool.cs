//bibaoke.com

using System;
using System.Collections.Generic;
using System.Threading;

namespace Less.MultiThread
{
    /// <summary>
    /// 在线程池中执行任务
    /// </summary>
    public static class Pool
    {
        /// <summary>
        /// 设置最大线程数目
        /// </summary>
        /// <param name="value"></param>
        public static void SetMaxThreads(int value)
        {
            int mw, mc;

            ThreadPool.GetMaxThreads(out mw, out mc);

            if (!ThreadPool.SetMaxThreads(value, mc))
                throw new ArgumentException();
        }

        /// <summary>
        /// 所有线程已完成
        /// </summary>
        public static bool AllDone
        {
            get
            {
                return Pool.ActiveThreads == 0;
            }
        }

        /// <summary>
        /// 活动线程
        /// </summary>
        public static int ActiveThreads
        {
            get
            {
                int mw, mc;

                int aw, ac;

                ThreadPool.GetMaxThreads(out mw, out mc);

                ThreadPool.GetAvailableThreads(out aw, out ac);

                return mw - aw;
            }
        }

        /// <summary>
        /// 在线程池中执行任务
        /// </summary>
        /// <param name="action">任务委托</param>
        public static void Exec(Action action)
        {
            ThreadPool.QueueUserWorkItem(o => action());
        }

        /// <summary>
        /// 在线程池中执行任务
        /// </summary>
        /// <param name="value">传递的值</param>
        /// <param name="action">任务委托</param>
        public static void Exec<T>(T value, Action<T> action)
        {
            Pool.Exec(value, action, null);
        }

        /// <summary>
        /// 在线程池中执行任务
        /// </summary>
        /// <param name="value">传递的值</param>
        /// <param name="action">任务委托</param>
        /// <param name="onError">出错处理委托</param>
        public static void Exec<T>(T value, Action<T> action, Action<Exception> onError)
        {
            ThreadPool.QueueUserWorkItem(o =>
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
            }, value);
        }

        /// <summary>
        /// 在线程池中执行任务
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="action"></param>
        public static void Exec(int from, int to, Action<int> action)
        {
            Pool.Exec(from, to, action, null);
        }

        /// <summary>
        /// 在线程池中执行任务
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="action"></param>
        /// <param name="onError"></param>
        public static void Exec(int from, int to, Action<int> action, Action<Exception> onError)
        {
            Pool.Exec(int.MaxValue, from, to, action, onError);
        }

        /// <summary>
        /// 在线程池中执行任务
        /// </summary>
        /// <param name="threads">线程数</param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="action"></param>
        public static void Exec(int threads, int from, int to, Action<int> action)
        {
            Pool.Exec(threads, from, to, action, null);
        }

        /// <summary>
        /// 在线程池中执行任务
        /// </summary>
        /// <param name="threads">线程数</param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="action"></param>
        /// <param name="onError"></param>
        public static void Exec(int threads, int from, int to, Action<int> action, Action<Exception> onError)
        {
            int[] enumerable = new int[to - from + 1];

            for (int i = from, j = 0; i <= to; i++, j++)
                enumerable[j] = i;

            Pool.Exec(threads, enumerable, action, onError);
        }

        /// <summary>
        /// 在线程池中执行任务
        /// </summary>
        /// <param name="enumerable">任务集合</param>
        /// <param name="action">任务委托</param>
        public static void Exec<T>(IEnumerable<T> enumerable, Action<T> action)
        {
            Pool.Exec(enumerable, action, null);
        }

        /// <summary>
        /// 在线程池中执行任务
        /// </summary>
        /// <param name="enumerable">任务集合</param>
        /// <param name="action">任务委托</param>
        /// <param name="onError">出错处理委托</param>
        public static void Exec<T>(IEnumerable<T> enumerable, Action<T> action, Action<Exception> onError)
        {
            Pool.Exec(int.MaxValue, enumerable, action, onError);
        }

        /// <summary>
        /// 在线程池中执行任务
        /// </summary>
        /// <param name="threads">线程数</param>
        /// <param name="enumerable">任务集合</param>
        /// <param name="action">任务委托</param>
        public static void Exec<T>(int threads, IEnumerable<T> enumerable, Action<T> action)
        {
            Pool.Exec(threads, enumerable, action, null);
        }

        /// <summary>
        /// 在线程池中执行任务
        /// </summary>
        /// <param name="threads">线程数</param>
        /// <param name="enumerable">任务集合</param>
        /// <param name="action">任务委托</param>
        /// <param name="onError">出错处理委托</param>
        public static void Exec<T>(int threads, IEnumerable<T> enumerable, Action<T> action, Action<Exception> onError)
        {
            if (threads <= 0)
                return;

            int available = threads;

            object availableLock = new object();

            foreach (T i in enumerable)
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

                        ThreadPool.QueueUserWorkItem(o =>
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
                            finally
                            {
                                lock (availableLock)
                                    available++;
                            }
                        }, i);

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

        /// <summary>
        /// 等待所有线程完成
        /// </summary>
        public static void WaitAll()
        {
            while (true)
            {
                if (Pool.AllDone)
                    break;
                else
                    Thread.Sleep(100);
            }
        }
    }
}