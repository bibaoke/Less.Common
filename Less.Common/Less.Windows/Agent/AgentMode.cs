//bibaoke.com

namespace Less.Windows
{
    /// <summary>
    /// 代理作业模式
    /// </summary>
    public enum AgentMode
    {
        /// <summary>
        /// 单实例，如果进程在运行，则忽略
        /// </summary>
        Singleton,

        /// <summary>
        /// 单实例，如果进程在运行，则重启
        /// </summary>
        Restart,

        /// <summary>
        /// 在新的进程中执行
        /// </summary>
        New
    }
}
