//bibaoke.com

using System;

namespace Less
{
    /// <summary>
    /// DateTime 扩展方法
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 精确的日期时间格式化字符串
        /// yyyy-MM-dd HH:mm:ss.fffffff
        /// </summary>
        /// <param name="dt"></param>
        public static string ToExactString(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss.fffffff");
        }

        /// <summary>
        /// 日期时间格式化字符串
        /// yyyy-M-d H:mm
        /// </summary>
        /// <param name="dt"></param>
        public static string ToDateTimeStringForDisplay(this DateTime dt)
        {
            return dt.ToString("yyyy-M-d HH:mm");
        }

        /// <summary>
        /// 日期格式化字符串
        /// yyyy-M-d
        /// </summary>
        /// <param name="dt"></param>
        public static string ToDateStringForDisplay(this DateTime dt)
        {
            return dt.ToString("yyyy-M-d");
        }

        /// <summary>
        /// 日期格式化字符串
        /// yyyy-MM-dd
        /// </summary>
        /// <param name="dt"></param>
        public static string ToDateString(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 时间格式化字符串
        /// HH:mm:ss.fffffff
        /// </summary>
        /// <param name="dt"></param>
        public static string ToTimeString(this DateTime dt)
        {
            return dt.ToString("HH:mm:ss.fffffff");
        }
    }
}
