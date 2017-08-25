//bibaoke.com

using System.IO;

namespace Less.Windows
{
    /// <summary>
    /// Byte[] 扩展方法
    /// </summary>
    public static class ByteExtensions
    {
        /// <summary>
        /// 转换为 MemoryStream
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        public static MemoryStream ToMemoryStream(this byte[] byteArray)
        {
            return new MemoryStream(byteArray);
        }
    }
}
