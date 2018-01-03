//bibaoke.com

using System;

namespace Less
{
    /// <summary>
    /// long 扩展方法
    /// </summary>
    public static class LongExtensions
    {
        /// <summary>
        /// 输出整型
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        public static int ToInt(this long l)
        {
            return Convert.ToInt32(l);
        }
    }
}