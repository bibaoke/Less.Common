//bibaoke.com

using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using Less.Collection;
using System.Linq;
using System;

namespace Less.Text
{
    /// <summary>
    /// 拼接
    /// </summary>
    public static class CombineExtensions
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static string Join<T>(this IEnumerable<T> items)
        {
            return items.Join(string.Empty);
        }

        /// <summary>
        /// 用指定的分隔符连接字符串
        /// </summary>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string Join<T>(this IEnumerable<T> items, string separator)
        {
            return string.Join(separator, items.Select(i => i.ToString()).ToArray());
        }

        /// <summary>
        /// 用文字表示键值集合
        /// </summary>
        /// <param name="list">键值集合</param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        public static string List(this NameValueCollection list, string separator)
        {
            string result = "";

            //拼接键值集合
            foreach (string i in list.AllKeys)
            {
                result += i + "=" + list[i] + separator;
            }

            //移除最后一个分隔符
            if (result.Length > 0)
            {
                result = result.TrimEnd(separator);
            }

            return result;
        }

        /// <summary>
        /// 重复字符串
        /// </summary>
        /// <param name="s"></param>
        /// <param name="count">重复次数</param>
        /// <returns>重复后的字符串</returns>
        /// <exception cref="ArgumentOutOfRangeException">重复次数不能小于零</exception>
        public static string Repeat(this string s, int count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count", count, "重复次数不能小于零");
            }

            string result = "";

            count.Each(() => result += s);

            return result;
        }
    }
}
