//bibaoke.com

using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Less.Text
{
    /// <summary>
    /// 验证
    /// </summary>
    public static class VerifyExtensions
    {
        /// <summary>
        /// 是否 ipv4 地址
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsIpv4(this string s)
        {
            string[] array = s.Split('.');

            if (array.Length == 4)
            {
                foreach (string i in array)
                {
                    if (!i.IsByte())
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 是否包含 values 中的任意一个值
        /// </summary>
        /// <param name="s"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">values 不能为 null</exception>
        public static bool ContainsAnyOf(this string s, params string[] values)
        {
            if (values.IsNull())
            {
                throw new ArgumentNullException("values");
            }

            foreach (string i in values)
            {
                if (i.IsNotNull() && s.Contains(i))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 是否存在 unicode 字符
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool HasUnicode(this string s)
        {
            return Encoding.UTF8.GetByteCount(s) > s.Length;
        }

        /// <summary>
        /// 检查字符串是否可以解析为 byte 类型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsByte(this string s)
        {
            byte result;

            return s.IsByte(out result);
        }

        /// <summary>
        /// 检查字符串是否可以解析为 byte 类型
        /// </summary>
        /// <param name="s"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool IsByte(this string s, out byte result)
        {
            return byte.TryParse(s, out result);
        }

        /// <summary>
        /// 检查字符串是否可以解析为 short 类型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsShort(this string s)
        {
            short result;

            return s.IsShort(out result);
        }

        /// <summary>
        /// 检查字符串是否可以解析为 short 类型
        /// </summary>
        /// <param name="s"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool IsShort(this string s, out short result)
        {
            return short.TryParse(s, out result);
        }

        /// <summary>
        /// 检查字符串是否一个整数
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsInt(this string s)
        {
            int result;

            return s.IsInt(out result);
        }

        /// <summary>
        /// 检查字符串是否一个整数
        /// </summary>
        /// <param name="s"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool IsInt(this string s, out int result)
        {
            return int.TryParse(s, out result);
        }

        /// <summary>
        /// 是否不为 null 或 "" 或只有空白字符
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNotWhiteSpace(this string s)
        {
            return !s.IsWhiteSpace();
        }

        /// <summary>
        /// 是否 null 或 "" 或只有空白字符
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsWhiteSpace(this string s)
        {
            return s.IsNull() || s.Trim() == "";
        }

        /// <summary>
        /// 是否不为 null 或 ""
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNotEmpty(this string s)
        {
            return !s.IsEmpty();
        }

        /// <summary>
        /// 是否 null 或 ""
        /// </summary>
        /// <param name="s"></param>
        /// <param name="instead">替代字符串</param>
        /// <returns></returns>
        public static string IsEmpty(this string s, string instead)
        {
            return s.IsEmpty() ? instead : s;
        }

        /// <summary>
        /// 是否 null 或 ""
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// 是否全部 null 或 ""
        /// </summary>
        /// <param name="s">待判断的字符串</param>
        /// <returns></returns>
        public static bool IsAllEmpty(this string[] s)
        {
            foreach (string i in s)
            {
                if (!i.IsEmpty())
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 是否全部 null
        /// </summary>
        /// <param name="s">待判断的字符串</param>
        /// <returns></returns>
        public static bool IsAllNull(this string[] s)
        {
            foreach (string i in s)
            {
                if (i != null)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 是否存在 null
        /// </summary>
        /// <param name="s">待判断的字符串</param>
        /// <returns></returns>
        public static bool IsAnyNull(this string[] s)
        {
            foreach (string i in s)
            {
                if (i == null)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 是否没有 null
        /// </summary>
        /// <param name="s">待判断的字符串</param>
        /// <returns></returns>
        public static bool IsNotNull(this string[] s)
        {
            return !IsAnyNull(s);
        }
    }
}
