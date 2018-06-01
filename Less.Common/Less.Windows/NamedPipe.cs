using System.IO.Pipes;

namespace Less.Windows
{
    /// <summary>
    /// 命名管道
    /// </summary>
    public static class NamedPipe
    {
        /// <summary>
        /// 创建客户端命名管道
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static NamedPipeClientStream Client(string name)
        {
            return new NamedPipeClientStream(".", name, PipeDirection.InOut);
        }

        /// <summary>
        /// 创建服务端命名管道
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static NamedPipeServerStream Server(string name)
        {
            return new NamedPipeServerStream(name, PipeDirection.InOut);
        }
    }
}
