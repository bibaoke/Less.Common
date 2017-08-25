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
        /// 连接对象字面量
        /// </summary>
        /// <param name="s"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string Combine(this string s, params object[] values)
        {
            return string.Concat(s.ConstructArray(values));
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        /// <param name="s"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">values 不能为 null</exception>
        /// <exception cref="OutOfMemoryException">内存不足</exception>
        public static string Combine(this string s, params string[] values)
        {
            return string.Concat(s.ConstructArray(values));
        }

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
            //可变字符串
            //保存结果
            StringBuilder b = new StringBuilder();

            //拼接键值集合
            foreach (string i in list.AllKeys)
            {
                b.Append(i).Append("=").Append(list[i]).Append(separator);
            }

            //移除最后一个分隔符
            if (b.Length > 0)
            {
                b.Remove(b.Length - 1, 1);
            }

            return b.ToString();
        }

        /// <summary>
        /// 重复字符串
        /// </summary>
        /// <param name="s"></param>
        /// <param name="count">重复次数</param>
        /// <returns>重复后的字符串</returns>
        public static string Repeat(this string s, int count)
        {
            string[] result = new string[count];

            count.Each(delegate (int index)
            {
                result[index] = s;
            });

            return string.Concat(result);
        }
    }
}
