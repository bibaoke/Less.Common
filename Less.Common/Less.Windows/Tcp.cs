using Less.Text;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Less.Windows
{
    /// <summary>
    /// TCP 通信
    /// </summary>
    public static class Tcp
    {
        /// <summary>
        /// 发送一行
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="content"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string SendLine(string ip, int port, string content, Encoding encoding)
        {
            return Tcp.Send(ip, port, content + Symbol.NewLine, encoding);
        }

        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="content"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Send(string ip, int port, string content, Encoding encoding)
        {
            return Tcp.Send(ip, port, encoding.GetBytes(content)).ToString(encoding);
        }

        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Send(string ip, int port, byte[] data)
        {
            using (TcpClient client = new TcpClient(ip, port))
            {
                using (NetworkStream stream = client.GetStream())
                {
                    stream.Write(data);

                    return stream.ToByteArray();
                }
            }
        }

        /// <summary>
        /// 监听
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static TcpListener Listen(string ip, int port, Action<NetworkStream> action)
        {
            IPAddress ipAddress = IPAddress.Parse(ip);

            TcpListener server = new TcpListener(ipAddress, port);

            server.Start();

            using (TcpClient client = server.AcceptTcpClient())
            {
                using (NetworkStream stream = client.GetStream())
                {
                    action(stream);
                }
            }

            return server;
        }
    }
}
