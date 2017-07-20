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
        /// 全部写入
        /// </summary>
        /// <param name="s"></param>
        /// <param name="data"></param>
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
        /// <exception cref="OutOfMemoryException">栈空间不足</exception>
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
