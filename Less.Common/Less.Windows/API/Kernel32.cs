//bibaoke.com

using System;
using System.Runtime.InteropServices;

namespace Less.Windows
{
    /// <summary>
    /// Kernel32 API
    /// </summary>
    public static class Kernel32
    {
        /// <summary>
        /// An application-defined function used with the SetConsoleCtrlHandler function
        /// A console process uses this function to handle control signals received by the process
        /// When the signal is received, the system creates a new thread in the process to execute the function
        /// https://docs.microsoft.com/en-us/windows/console/handlerroutine
        /// </summary>
        /// <param name="ctrlType"></param>
        /// <returns></returns>
        public delegate bool HandlerRoutine(CtrlType ctrlType);

        /// <summary>
        /// 添加控制台关闭时的事件处理程序
        /// </summary>
        /// <param name="action">在控制台关闭时执行</param>
        /// <returns></returns>
        public static bool AddConsoleCtrlHandler(Action action)
        {
            return Kernel32.SetConsoleCtrlHandler((ctrlType) =>
            {
                action();

                return false;
            }, true);
        }

        /// <summary>
        /// Adds or removes an application-defined HandlerRoutine function from the list of handler functions for the calling process
        /// https://docs.microsoft.com/en-us/windows/console/setconsolectrlhandler
        /// </summary>
        /// <param name="HandlerRoutine">A pointer to the application-defined HandlerRoutine function to be added or removed. This parameter can be NULL</param>
        /// <param name="Add">If this parameter is TRUE, the handler is added; if it is FALSE, the handler is removed</param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero
        /// If the function fails, the return value is zero
        /// To get extended error information, call GetLastError
        /// </returns>
        [DllImport("kernel32.dll")]
        public static extern bool SetConsoleCtrlHandler(HandlerRoutine HandlerRoutine, bool Add);
    }
}
