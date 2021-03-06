﻿//bibaoke.com

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
        /// 最近一次开始时间
        /// </summary>
        public DateTime Last
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
        /// 创建一个每天执行的代理作业
        /// </summary>
        /// <param name="command">命令 包括文件名和参数</param>
        /// <param name="mode">代理作业模式</param>
        /// <param name="hour">开始执行时间 时</param>
        /// <exception cref="ArgumentOutOfRangeException">hour 超出范围</exception>
        public Agent(string command, AgentMode mode, int hour) : this(command, mode, hour, 0, 0)
        {
            //
        }

        /// <summary>
        /// 创建一个每天执行的代理作业
        /// </summary>
        /// <param name="command">命令 包括文件名和参数</param>
        /// <param name="mode">代理作业模式</param>
        /// <param name="hour">开始执行时间 时</param>
        /// <param name="minute">开始执行时间 分</param>
        /// <exception cref="ArgumentOutOfRangeException">hour 或 minute 超出范围</exception>
        public Agent(string command, AgentMode mode, int hour, int minute) : this(command, mode, hour, minute, 0)
        {
            //
        }

        /// <summary>
        /// 创建一个每天执行的代理作业
        /// </summary>
        /// <param name="command">命令 包括文件名和参数</param>
        /// <param name="mode">代理作业模式</param>
        /// <param name="hour">开始执行时间 时</param>
        /// <param name="minute">开始执行时间 分</param>
        /// <param name="second">开始执行时间 秒</param>
        /// <exception cref="ArgumentOutOfRangeException">hour 或 minute 或 second 超出范围</exception>
        public Agent(string command, AgentMode mode, int hour, int minute, int second)
        {
            if (hour < 0 || hour > 23)
            {
                throw new ArgumentOutOfRangeException("hour");
            }

            if (minute < 0 || minute > 59)
            {
                throw new ArgumentOutOfRangeException("minute");
            }

            if (second < 0 || second > 59)
            {
                throw new ArgumentOutOfRangeException("second");
            }

            DateTime now = DateTime.Now;

            DateTime startTime = new DateTime(now.Year, now.Month, now.Day, hour, minute, second);

            if (startTime < now)
            {
                startTime = startTime.AddDays(1);
            }

            this.Init(command, mode, TimeSpan.FromDays(1), startTime);
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
            this.Init(command, mode, interval, startTime);
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
            Asyn.Exec(() =>
            {
                Syn.Wait(() => DateTime.Now >= this.StartTime);

                if (!this.HasShutDown)
                {
                    this.Exec();
                }

                Asyn.Exec(() =>
                {
                    Syn.Wait(() =>
                    {
                        if (this.HasShutDown)
                        {
                            return true;
                        }
                        else
                        {
                            if (DateTime.Now - this.Last >= this.Interval)
                            {
                                this.Exec();
                            }

                            return false;
                        }
                    });
                });
            });
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

        private void Init(string command, AgentMode mode, TimeSpan interval, DateTime startTime)
        {
            if (command.IsNull())
            {
                throw new ArgumentNullException("command", "command 不能为 null");
            }

            this.Mode = mode;
            this.Interval = interval;
            this.StartTime = startTime;
            this.Last = DateTime.MaxValue;
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

        private void Exec()
        {
            this.Last = DateTime.Now;

            switch (this.Mode)
            {
                case AgentMode.New:
                    lock (this.Process)
                    {
                        Asyn.Exec(() => this.Process.Start());
                    }

                    break;
                case AgentMode.Restart:
                    lock (this.Process)
                    {
                        this.Kill();

                        Asyn.Exec(() => this.Process.Start());
                    }

                    break;
                case AgentMode.Singleton:
                    lock (this.Process)
                    {
                        if (this.HasExited())
                        {
                            Asyn.Exec(() => this.Process.Start());
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
