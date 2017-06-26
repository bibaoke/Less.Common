//bibaoke.com

using System;

namespace Less
{
    /// <summary>
    /// Guid 扩展方法
    /// </summary>
    public static class GuidExtensions
    {
        /// <summary>
        /// 转换成 Base64
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public static Base64 ToBase64(this Guid g)
        {
            return g.ToByteArray();
        }

        /// <summary>
        /// 是否非空
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public static bool IsNotEmpty(this Guid g)
        {
            return !g.IsEmpty();
        }

        /// <summary>
        /// 是否空Guid
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public static bool IsEmpty(this Guid g)
        {
            return g == Guid.Empty;
        }

        /// <summary>
        /// 输出没有横杠的字符串
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public static string ToStringWithoutHyphen(this Guid g)
        {
            return g.ToString("N");
        }
    }
}
