//bibaoke.com

using Less.MultiThread;
using Less.Text;
using System;
using System.Diagnostics;
using System.Threading;

namespace Less.Windows
{
    /// <summary>
    /// 代理作业
    /// </summary>
    public class Agent : IDisposable
    {
        private Process Process
        {
            get;
            set;
        }

        /// <summary>
        /// 代理作业模式
        /// </summary>
        public AgentMode Mode
        {
            get;
            private set;
        }

        /// <summary>
        /// 执行间隔
        /// </summary>
        public TimeSpan Interval
        {
            get;
            private set;
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime
        {
            get;
            private set;
        }

        /// <summary>
        /// 是否已经停止
        /// </summary>
        public bool HasShutDown
        {
            get;
            private set;
        }

        /// <summary>
        /// 创建一个启动即执行的代理作业
        /// </summary>
        /// <param name="command">命令 包括文件名和参数</param>
        /// <param name="mode">代理作业模式</param>
        /// <param name="interval">执行间隔</param>
        public Agent(string command, AgentMode mode, TimeSpan interval) : this(command, mode, interval, DateTime.Now)
        {
            //
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="command">命令 包括文件名和参数</param>
        /// <param name="mode">代理作业模式</param>
        /// <param name="interval">执行间隔</param>
        /// <param name="startTime">开始时间</param>
        public Agent(string command, AgentMode mode, TimeSpan interval, DateTime startTime)
        {
            if (command.IsNull())
            {
                throw new ArgumentNullException("command", "command 不能为 null");
            }

            this.Mode = mode;
            this.Interval = interval;
            this.StartTime = startTime;
            this.HasShutDown = false;

            ValueSet<int, int> result = command.IndexOfWhiteSpace();

            this.Process = new Process();

            if (result.IsNull())
            {
                this.Process.StartInfo.FileName = command;
            }
            else
            {
                this.Process.StartInfo.FileName = command.Substring(0, result.Value1);
                this.Process.StartInfo.Arguments = command.Substring(result.Value1 + result.Value2);
            }
        }

        /// <summary>
        /// 析构
        /// </summary>
        ~Agent()
        {
            this.Dispose();
        }

        /// <summary>
        /// 启动
        /// </summary>
        public void StartUp()
        {
            while (true)
            {
                if (DateTime.Now >= this.StartTime)
                {
                    DateTime begin = DateTime.Now;

                    Asyn.Exec(() =>
                    {
                        begin = DateTime.Now;

                        this.Exec();
                    });

                    Asyn.Exec(() =>
                    {
                        while (true)
                        {
                            if (this.HasShutDown)
                            {
                                break;
                            }

                            Asyn.Exec(() =>
                            {
                                if (DateTime.Now - begin > this.Interval)
                                {
                                    begin = DateTime.Now;

                                    this.Exec();
                                }
                            });

                            Thread.Sleep(100);
                        }
                    });

                    break;
                }

                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void ShutDown()
        {
            this.Kill();

            this.HasShutDown = true;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            this.ShutDown();

            this.Process.Dispose();
        }

        private void Exec()
        {
            switch (this.Mode)
            {
                case AgentMode.New:
                    lock (this.Process)
                    {
                        this.Process.Start();
                    }

                    break;
                case AgentMode.Restart:
                    lock (this.Process)
                    {
                        this.Kill();

                        this.Process.Start();
                    }

                    break;
                case AgentMode.Singleton:
                    lock (this.Process)
                    {
                        if (this.HasExited())
                        {
                            this.Process.Start();
                        }
                    }

                    break;
            }
        }

        private void Kill()
        {
            if (!this.HasExited())
            {
                this.Process.Kill();
            }
        }

        private bool HasExited()
        {
            try
            {
                return this.Process.HasExited;
            }
            catch (InvalidOperationException)
            {
                return true;
            }
        }
    }
}
