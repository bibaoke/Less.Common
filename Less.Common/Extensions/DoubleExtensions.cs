//bibaoke.com

using System;

namespace Less
{
    /// <summary>
    /// double 扩展方法
    /// </summary>
    public static class DoubleExtensions
    {
        /// <summary>
        /// 转换为整型
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static int ToInt(this double d)
        {
            return Convert.ToInt32(d);
        }

        /// <summary>
        /// 转换为十进制
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this double d)
        {
            return Convert.ToDecimal(d);
        }
    }
}
