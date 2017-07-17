//bibaoke.com

using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace Less.MultiThread
{
    /// <summary>
    /// 在线程池中执行任务
    /// </summary>
    public static class Pool
    {
        /// <summary>
        ///  获取可用线程数
        /// </summary>
        /// <returns></returns>
        public static int GetAvailableThreads()
        {
            int workerThreads, completionPortThreads;

            ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);

            return workerThreads;
        }

        /// <summary>
        /// 获取最大线程数
        /// </summary>
        /// <returns></returns>
        public static int GetMaxThreads()
        {
            int workerThreads, completionPortThreads;

            ThreadPool.GetMaxThreads(out workerThreads, out completionPortThreads);

            return workerThreads;
        }

        /// <summary>
        /// 设置最大线程数
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void SetMaxThreads(int value)
        {
            int workerThreads, completionPortThreads;

            ThreadPool.GetMaxThreads(out workerThreads, out completionPortThreads);

            if (!ThreadPool.SetMaxThreads(value, completionPortThreads))
                throw new ArgumentException();
        }

        /// <summary>
        /// 在线程池中执行任务
        /// </summary>
        /// <param name="action">任务委托</param>
        /// <exception cref="ApplicationException">遇到了内存不足的情况</exception>
        /// <exception cref="OutOfMemoryException">无法将该工作项排队</exception>
        public static void Exec(Action action)
        {
            if (action.IsNotNull())
                ThreadPool.QueueUserWorkItem(i => action());
        }

        /// <summary>
        /// 在线程池中执行任务
        /// </summary>
        /// <param name="value">传递的值</param>
        /// <param name="action">任务委托</param>        
        /// <exception cref="ApplicationException">遇到了内存不足的情况</exception>
        /// <exception cref="OutOfMemoryException">无法将该工作项排队</exception>
        public static void Exec<T>(T value, Action<T> action)
        {
            if (action.IsNotNull())
                ThreadPool.QueueUserWorkItem(i => action((T)i), value);
        }

        /// <summary>
        /// 在线程池中执行任务
        /// </summary>
        /// <param name="threads">线程数</param>
        /// <param name="enumerable">任务集合</param>
        /// <param name="action">任务委托</param>
        /// <exception cref="ApplicationException">遇到了内存不足的情况</exception>
        /// <exception cref="OutOfMemoryException">无法将该工作项排队</exception>
        public static void Exec<T>(int threads, IEnumerable<T> enumerable, Action<T> action)
        {
            if (threads > 0 && enumerable.IsNotNull() && action.IsNotNull())
            {
                Semaphore semaphore = new Semaphore(threads, threads);

                T[] array = enumerable.ToArray();

                int count = array.Length;

                object countLock = new object();

                foreach (T i in array)
                {
                    semaphore.WaitOne();

                    ThreadPool.QueueUserWorkItem((state) =>
                    {
                        try
                        {
                            action((T)state);
                        }
                        finally
                        {
                            semaphore.Release();

                            lock (countLock)
                                count--;

                            if (count == 0)
                                semaphore.Close();
                        }
                    }, i);
                }
            }
        }
    }
}