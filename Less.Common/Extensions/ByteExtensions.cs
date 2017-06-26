//bibaoke.com

using System.Text;

namespace Less
{
    /// <summary>
    /// 字节的扩展方法
    /// </summary>
    public static class ByteExtensions
    {
        /// <summary>
        /// 输出十六进制字符串
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        public static HexString ToHexString(this byte[] byteArray)
        {
            return byteArray;
        }

        /// <summary>
        /// 按指定编码输出字符串
        /// </summary>
        /// <param name="byteArray"></param>
        /// <param name="encoding">指定的编码</param>
        /// <returns></returns>
        public static string ToString(this byte[] byteArray, Encoding encoding)
        {
            return encoding.GetString(byteArray);
        }
    }
}
