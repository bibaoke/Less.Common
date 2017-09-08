//bibaoke.com

using System;
using System.Diagnostics;
using System.Management;

namespace Less.Windows
{
    /// <summary>
    /// 性能计数
    /// </summary>
    public static class Performance
    {
        /// <summary>
        /// cpu 使用率
        /// </summary>
        public static float CpuTime
        {
            get
            {
                return Performance.CpuCounter.NextValue();
            }
        }

        /// <summary>
        /// 可用内存
        /// </summary>
        public static float MemoryAvailable
        {
            get
            {
                return Performance.MemoryCounter.NextValue();
            }
        }

        /// <summary>
        /// 核心数
        /// </summary>
        public static int NumberOfCores
        {
            get;
            private set;
        }

        /// <summary>
        /// 处理器数
        /// </summary>
        public static int NumberOfLogicalProcessors
        {
            get;
            private set;
        }

        private static PerformanceCounter CpuCounter
        {
            get;
            set;
        }

        private static PerformanceCounter MemoryCounter
        {
            get;
            set;
        }

        private static ManagementClass Win32_Processor
        {
            get;
            set;
        }

        static Performance()
        {
            //cpu 使用率
            Performance.CpuCounter = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");

            //cpu 信息
            Performance.Win32_Processor = new ManagementClass("Win32_Processor");

            ManagementObjectCollection instances = Performance.Win32_Processor.GetInstances();

            ManagementObject o = null;

            foreach (ManagementObject i in instances)
            {
                o = i;
            }

            if (o.IsNotNull())
            {
                Performance.NumberOfCores = Convert.ToInt32(o["NumberOfCores"]);
                Performance.NumberOfLogicalProcessors = Convert.ToInt32(o["NumberOfLogicalProcessors"]);
            }

            //可用内存
            Performance.MemoryCounter = new PerformanceCounter("Memory", "Available MBytes");
        }
    }
}