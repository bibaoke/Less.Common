//bibaoke.com

using System;

namespace Less.Text
{
    /// <summary>
    /// 比较
    /// </summary>
    public static class CompareExtensions
    {
        /// <summary>
        /// 字符串不是数组中的任意一项
        /// </summary>
        /// <param name="s"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool NotIn(this string s, params string[] array)
        {
            return !s.In(array);
        }

        /// <summary>
        /// 字符串不是数组中的任意一项
        /// </summary>
        /// <param name="s"></param>
        /// <param name="option"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool NotIn(this string s, CaseOptions option, params string[] array)
        {
            return !s.In(option, array);
        }

        /// <summary>
        /// 字符串是否与数组中的任意一项相等
        /// 大小写敏感
        /// </summary>
        /// <param name="s"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool In(this string s, params string[] array)
        {
            return s.In(CaseOptions.CaseSensitive, array);
        }

        /// <summary>
        /// 字符串是否与数组中的任意一项相等
        /// </summary>
        /// <param name="s"></param>
        /// <param name="option">大小写选项</param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool In(this string s, CaseOptions option, params string[] array)
        {
            StringComparison comparison = option.ToStringComparison();

            foreach (string i in array)
            {
                if (i.Equals(s, comparison))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 不分大小写比较字符串
        /// </summary>
        /// <param name="s"></param>
        /// <param name="toCompare">要比较的字符串</param>
        /// <returns>是否相同</returns>
        /// <exception cref="NullReferenceException">字符串不能为 null</exception>
        public static bool CompareIgnoreCase(this string s, string toCompare)
        {
            return s.Equals(toCompare, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 比较字符串
        /// 去除首尾空格
        /// </summary>
        /// <param name="s"></param>
        /// <param name="toCompare">要比较的字符串</param>
        /// <returns>是否相同</returns>
        /// <exception cref="NullReferenceException">字符串不能为 null toCompare 不能为 null</exception>
        public static bool CompareTrim(this string s, string toCompare)
        {
            return s.Trim().Equals(toCompare.Trim());
        }

        /// <summary>
        /// 去除首尾空格
        /// 不分大小写比较字符串
        /// </summary>
        /// <param name="s"></param>
        /// <param name="toCompare">要比较的字符串</param>
        /// <returns>是否相同</returns>
        /// <exception cref="NullReferenceException">字符串不能为 null toCompare 不能为 null</exception>
        public static bool CompareTrimAndIgnoreCase(this string s, string toCompare)
        {
            return s.Trim().Equals(toCompare.Trim(), StringComparison.OrdinalIgnoreCase);
        }
    }
}
