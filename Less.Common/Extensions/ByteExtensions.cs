//bibaoke.com

using System;
using System.Text;

namespace Less
{
    /// <summary>
    /// byte 的扩展方法
    /// </summary>
    public static class ByteExtensions
    {
        /// <summary>
        /// 输出 Hex 字符串
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static HexString ToHexString(this byte[] array)
        {
            return array;
        }

        /// <summary>
        /// 按指定编码输出字符串
        /// </summary>
        /// <param name="array"></param>
        /// <param name="encoding">指定的编码</param>
        /// <returns></returns>
        public static string ToString(this byte[] array, Encoding encoding)
        {
            return encoding.GetString(array);
        }

        /// <summary>
        /// 复制 byte[]
        /// </summary>
        /// <param name="array"></param>
        /// <returns>返回复制的 byte[] 副本</returns>
        /// <exception cref="OutOfMemoryException">内存不足</exception>
        /// <exception cref="OverflowException">数组超过了最大长度</exception>
        public static byte[] Copy(this byte[] array)
        {
            byte[] clone = new byte[array.Length];

            Buffer.BlockCopy(array, 0, clone, 0, array.Length);

            return clone;
        }

        /// <summary>
        /// 扩展 byte[]
        /// </summary>
        /// <param name="array"></param>
        /// <param name="length">新长度</param>
        /// <returns></returns>
        /// <exception cref="OutOfMemoryException">内存不足</exception>
        /// <exception cref="OverflowException">数组超过了最大长度</exception>
        public static byte[] ExtArray(this byte[] array, int length)
        {
            byte[] result = new byte[length];

            Buffer.BlockCopy(array, 0, result, 0, array.Length);

            return result;
        }
    }
}
