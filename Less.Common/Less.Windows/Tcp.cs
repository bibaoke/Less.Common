//bibaoke.com

using Less.Text;
using System;
using System.IO;
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
        /// <exception cref="SocketException">访问套接字时出错</exception>
        /// <exception cref="InvalidOperationException">未连接到远程主机</exception>
        /// <exception cref="IOException">IO异常</exception>
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
        /// <exception cref="SocketException">访问套接字时出错</exception>
        /// <exception cref="InvalidOperationException">未连接到远程主机</exception>
        /// <exception cref="IOException">IO异常</exception>
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
        /// <exception cref="SocketException">访问套接字时出错</exception>
        /// <exception cref="InvalidOperationException">未连接到远程主机</exception>
        /// <exception cref="IOException">IO异常</exception>
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
        /// 监听随机端口
        /// 端口范围 5001-65535
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="action">
        /// 接收到信息的操作
        /// </param>
        /// <param name="started">成功监听端口时执行</param>
        /// <returns></returns>
        /// <exception cref="SocketException">Socket异常</exception>
        public static void Listen(string ip, Action<NetworkStream> action, Action<int> started)
        {
            Tcp.Listen(ip, (stream) =>
            {
                action(stream);

                return true;
            }, started);
        }

        /// <summary>
        /// 监听随机端口
        /// 端口范围 5001-65535
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="func">
        /// 接收到信息的操作
        /// 返回 true 继续监听
        /// 返回 false 停止监听
        /// </param>
        /// <param name="started">成功监听端口时执行</param>
        /// <returns></returns>
        /// <exception cref="SocketException">Socket异常</exception>
        public static void Listen(string ip, Func<NetworkStream, bool> func, Action<int> started)
        {
            IPAddress ipAddress = IPAddress.Parse(ip);

            int port = 0;

            TcpListener server = null;

            int retry = 1;

            for (int i = 0; i <= retry; i++)
            {
                try
                {
                    port = RandomDef.Ins.Next(5001, 65536);

                    server = new TcpListener(ipAddress, port);

                    server.Start();
                }
                catch (SocketException)
                {
                    if (i >= retry)
                    {
                        throw;
                    }
                    else
                    {
                        continue;
                    }
                }

                break;
            }

            started(port);

            while (true)
            {
                using (TcpClient client = server.AcceptTcpClient())
                {
                    using (NetworkStream stream = client.GetStream())
                    {
                        if (!func(stream))
                        {
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 监听
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="action">接收到信息的操作</param>
        /// <returns></returns>
        /// <exception cref="SocketException">Socket异常</exception>
        public static void Listen(string ip, int port, Action<NetworkStream> action)
        {
            Tcp.Listen(ip, port, (stream) =>
            {
                action(stream);

                return true;
            }, null);
        }

        /// <summary>
        /// 监听
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="func">
        /// 接收到信息的操作
        /// 返回 true 继续监听
        /// 返回 false 停止监听
        /// </param>
        /// <param name="started">成功监听端口时执行</param>
        /// <returns></returns>
        /// <exception cref="SocketException">Socket异常</exception>
        public static void Listen(string ip, int port, Func<NetworkStream, bool> func, Action started)
        {
            IPAddress ipAddress = IPAddress.Parse(ip);

            TcpListener server = new TcpListener(ipAddress, port);

            server.Start();

            if (started.IsNotNull())
            {
                started();
            }

            while (true)
            {
                using (TcpClient client = server.AcceptTcpClient())
                {
                    using (NetworkStream stream = client.GetStream())
                    {
                        if (!func(stream))
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
}
