using Less.Text;
using System;
using System.IO.Pipes;
using System.Text;

namespace Less.Windows
{
    /// <summary>
    /// 命名管道
    /// </summary>
    public static class NamedPipe
    {
        /// <summary>
        /// 发送一行
        /// </summary>
        /// <param name="name"></param>
        /// <param name="content"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string SendLine(string name, string content, Encoding encoding)
        {
            using (NamedPipeClientStream stream = new NamedPipeClientStream(".", name, PipeDirection.InOut))
            {
                stream.Connect();

                stream.WriteLine(content, encoding);

                return stream.ReadLine(encoding);
            }
        }

        /// <summary>
        /// 创建服务端命名管道
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static NamedPipeServerStream Server(string name, Action<NamedPipeServerStream> action)
        {
            NamedPipeServerStream stream = new NamedPipeServerStream(name, PipeDirection.InOut);

            stream.WaitForConnection();

            action(stream);

            return stream;
        }
    }
}
