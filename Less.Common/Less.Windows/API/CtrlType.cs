﻿//bibaoke.com

namespace Less.Windows
{
    /// <summary>
    /// The type of control signal received by the handler
    /// https://docs.microsoft.com/en-us/windows/console/handlerroutine
    /// </summary>
    public enum CtrlType
    {
        /// <summary>
        /// A CTRL+C signal was received, either from keyboard input or from a signal generated by the GenerateConsoleCtrlEvent function
        /// </summary>
        CTRL_C_EVENT = 0,

        /// <summary>
        /// A CTRL+BREAK signal was received, either from keyboard input or from a signal generated by GenerateConsoleCtrlEvent
        /// </summary>
        CTRL_BREAK_EVENT = 1,

        /// <summary>
        /// A signal that the system sends to all processes attached to a console when the user closes the console (either by clicking Close on the console window's window menu, or by clicking the End Task button command from Task Manager)
        /// </summary>
        CTRL_CLOSE_EVENT = 2,

        /// <summary>
        /// A signal that the system sends to all console processes when a user is logging off
        /// </summary>
        CTRL_LOGOFF_EVENT = 5,

        /// <summary>
        /// A signal that the system sends when the system is shutting down
        /// </summary>
        CTRL_SHUTDOWN_EVENT = 6
    }
}
