//bibaoke.com

using System;
using System.Threading;

namespace Less.MultiThread
{
    /// <summary>
    /// 线程池
    /// </summary>
    public class Pool : IDisposable
    {
        private static object MaxThreadsLock = new object();

        private object BusyLock = new object();
        private object AvailableLock = new object();

        private Semaphore Semaphore
        {
            get;
            set;
        }

        /// <summary>
        /// 线程数
        /// </summary>
        public int Threads
        {
            get;
            private set;
        }

        /// <summary>
        /// 正在工作的线程数
        /// </summary>
        public int Busy
        {
            get;
            private set;
        }

        /// <summary>
        /// 空闲的线程数
        /// </summary>
        public int Available
        {
            get;
            private set;
        }

        /// <summary>
        /// 线程池的所有线程已经退出
        /// </summary>
        public bool Done
        {
            get
            {
                return this.Available == this.Threads;
            }
        }

        /// <summary>
        /// 创建一个线程池
        /// </summary>
        /// <param name="threads"></param>
        /// <exception cref="ArgumentOutOfRangeException">线程数超出范围</exception>
        public Pool(int threads)
        {
            lock (Pool.MaxThreadsLock)
            {
                Pool.SetMaxThreads(Pool.GetMaxThreads() + threads);
            }

            this.Threads = threads;
            this.Available = threads;

            this.Semaphore = new Semaphore(threads, threads);
        }

        /// <summary>
        /// 释放非托管资源
        /// </summary>
        ~Pool()
        {
            this.Dispose();
        }

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
        /// <exception cref="ArgumentOutOfRangeException">线程数超出范围</exception>
        public static void SetMaxThreads(int value)
        {
            int workerThreads, completionPortThreads;

            ThreadPool.GetMaxThreads(out workerThreads, out completionPortThreads);

            if (!ThreadPool.SetMaxThreads(value, completionPortThreads))
            {
                throw new ArgumentOutOfRangeException("value", value, "线程数超出范围");
            }
        }

        /// <summary>
        /// 在进程的线程池中执行任务
        /// </summary>
        /// <param name="action">任务委托</param>
        /// <exception cref="ApplicationException">遇到了内存不足的情况</exception>
        /// <exception cref="OutOfMemoryException">无法将该工作项排队</exception>
        public static void Exec(Action action)
        {
            if (action.IsNotNull())
            {
                ThreadPool.QueueUserWorkItem(i => action());
            }
        }

        /// <summary>
        /// 在进程的线程池中执行任务
        /// </summary>
        /// <param name="value">委托参数</param>
        /// <param name="action">任务委托</param>        
        /// <exception cref="ApplicationException">遇到了内存不足的情况</exception>
        /// <exception cref="OutOfMemoryException">无法将该工作项排队</exception>
        public static void Exec<T>(T value, Action<T> action)
        {
            if (action.IsNotNull())
            {
                ThreadPool.QueueUserWorkItem(i => action((T)i), value);
            }
        }

        /// <summary>
        /// 等待所有线程退出
        /// </summary>
        public void Wait()
        {
            Syn.Wait(() => this.Done);
        }

        /// <summary>
        /// 在线程池实例中执行任务
        /// </summary>
        /// <param name="action">任务委托</param>
        /// <exception cref="ApplicationException">遇到了内存不足的情况</exception>
        /// <exception cref="OutOfMemoryException">无法将该工作项排队</exception>
        public void Execute(Action action)
        {
            if (action.IsNotNull())
            {
                this.Semaphore.WaitOne();

                lock (this.BusyLock)
                {
                    this.Busy++;
                }

                lock (this.AvailableLock)
                {
                    this.Available--;
                }

                bool result = ThreadPool.QueueUserWorkItem(i =>
                {
                    try
                    {
                        action();
                    }
                    finally
                    {
                        this.Semaphore.Release();

                        lock (this.BusyLock)
                        {
                            this.Busy--;
                        }

                        lock (this.AvailableLock)
                        {
                            this.Available++;
                        }
                    }
                });

                if (!result)
                {
                    this.Semaphore.Release();

                    lock (this.BusyLock)
                    {
                        this.Busy--;
                    }

                    lock (this.AvailableLock)
                    {
                        this.Available++;
                    }
                }
            }
        }

        /// <summary>
        /// 在线程池实例中执行任务
        /// </summary>
        /// <param name="value">委托参数</param>
        /// <param name="action">任务委托</param>
        /// <exception cref="ApplicationException">遇到了内存不足的情况</exception>
        /// <exception cref="OutOfMemoryException">无法将该工作项排队</exception>
        public void Execute<T>(T value, Action<T> action)
        {
            if (action.IsNotNull())
            {
                this.Semaphore.WaitOne();

                lock (this.BusyLock)
                {
                    this.Busy++;
                }

                lock (this.AvailableLock)
                {
                    this.Available--;
                }

                bool result = ThreadPool.QueueUserWorkItem(i =>
                {
                    try
                    {
                        action((T)i);
                    }
                    finally
                    {
                        this.Semaphore.Release();

                        lock (this.BusyLock)
                        {
                            this.Busy--;
                        }

                        lock (this.AvailableLock)
                        {
                            this.Available++;
                        }
                    }
                }, value);

                if (!result)
                {
                    this.Semaphore.Release();

                    lock (this.BusyLock)
                    {
                        this.Busy--;
                    }

                    lock (this.AvailableLock)
                    {
                        this.Available++;
                    }
                }
            }
        }

        /// <summary>
        /// 释放非托管资源
        /// </summary>
        public void Dispose()
        {
            this.Semaphore.Close();
        }
    }
}