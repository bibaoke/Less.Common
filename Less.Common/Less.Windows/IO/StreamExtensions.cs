//bibaoke.com

using System.Text;
using System.IO;
using System;

namespace Less.Windows
{
    /// <summary>
    /// Stream 扩展方法
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// 读取一行
        /// </summary>
        /// <param name="s"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ReadLine(this Stream s, Encoding encoding)
        {
            using (StreamReader sr = new StreamReader(s, encoding))
            {
                return sr.ReadLine();
            }
        }

        /// <summary>
        /// 写入并换行
        /// </summary>
        /// <param name="s"></param>
        /// <param name="content"></param>
        /// <param name="encoding"></param>
        public static void WriteLine(this Stream s, string content, Encoding encoding)
        {
            using (StreamWriter sw = new StreamWriter(s, encoding))
            {
                sw.AutoFlush = true;

                sw.WriteLine(content);
            }
        }

        /// <summary>
        /// 全部写入
        /// </summary>
        /// <param name="s"></param>
        /// <param name="data"></param>
        /// <exception cref="NullReferenceException">Stream 不能为 null</exception>
        /// <exception cref="ArgumentNullException">data 不能为 null</exception>
        /// <exception cref="ArgumentException">data 的长度大于 Stream 缓冲区的长度</exception>
        /// <exception cref="IOException">写入错误</exception>
        /// <exception cref="NotSupportedException">Stream 不支持写入</exception>
        /// <exception cref="ObjectDisposedException">Stream 已关闭</exception>
        public static void Write(this Stream s, byte[] data)
        {
            s.Write(data, 0, data.Length);
        }

        /// <summary>
        /// 转换为 String
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e">编码</param>
        /// <returns></returns>
        public static string ToString(this Stream s, Encoding e)
        {
            return e.GetString(s.ToByteArray());
        }

        /// <summary>
        /// 转换为 byte[]
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        /// <exception cref="OutOfMemoryException">内存不足</exception>
        /// <exception cref="OverflowException">数据过多</exception>
        public static byte[] ToByteArray(this Stream s)
        {
            //可查找流 一次全部读取
            if (s.CanSeek)
            {
                byte[] data = new byte[s.Length];

                s.Read(data, 0, data.Length);

                return data;
            }
            //不可查找流 分次读取
            else
            {
                //分配 8KB 缓存
                Buffer buffer = new Buffer(1024 * 8);

                //读取流并写入缓存
                buffer.Buff(s);

                //输出
                return buffer.ToByteArray();
            }
        }
    }
}
