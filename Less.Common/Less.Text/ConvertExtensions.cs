//bibaoke.com

using System;
using System.Text;
using System.Text.RegularExpressions;
using Less.Collection;

namespace Less.Text
{
    /// <summary>
    /// 转换
    /// </summary>
    public static class ConvertExtensions
    {
        /// <summary>
        /// 转换成 Regex
        /// </summary>
        /// <param name="s"></param>
        /// <param name="options">Regex 选项</param>
        /// <returns></returns>
        public static Regex ToRegex(this string s, RegexOptions options)
        {
            return new Regex(s, options);
        }

        /// <summary>
        /// 转换成 Regex
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Regex ToRegex(this string s)
        {
            return new Regex(s);
        }

        /// <summary>
        /// 转换成 byte[]
        /// </summary>
        /// <param name="s"></param>
        /// <param name="encoding">指定的编码</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">字符串不能为 null</exception>
        /// <exception cref="EncoderFallbackException">
        /// 发生回退（请参见了解编码以获得完整的解释） 
        /// - 并且 - 
        /// System.Text.Encoding.EncoderFallback 被设置为 System.Text.EncoderExceptionFallback
        /// </exception>
        public static byte[] ToByteArray(this string s, Encoding encoding)
        {
            return encoding.GetBytes(s);
        }

        /// <summary>
        /// 转换成 DateTime
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Class<DateTime> ToDateTime(this string s)
        {
            DateTime result;

            if (DateTime.TryParse(s, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 转换成 Guid
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Class<Guid> ToGuid(this string s)
        {
            try
            {
                return new Guid(s);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 转换成 Guid[]
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Guid[] ToGuidArray(this string[] s)
        {
            Guid[] result = new Guid[s.Length];

            s.Each((index, i) =>
            {
                result[index] = i.ToGuid();
            });

            return result;
        }

        /// <summary>
        /// 转换成 decimal
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Class<decimal> ToDecimal(this string s)
        {
            decimal result;

            if (decimal.TryParse(s, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 转换成 byte
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Class<byte> ToByte(this string s)
        {
            byte result;

            if (byte.TryParse(s, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 转换成 short
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Class<short> ToShort(this string s)
        {
            short result;

            if (short.TryParse(s, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 转换成 int
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Class<int> ToInt(this string s)
        {
            int result;

            if (int.TryParse(s, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 转换成 int[]
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int[] ToIntArray(this string[] s)
        {
            int[] result = new int[s.Length];

            s.Each((index, i) =>
            {
                result[index] = i.ToInt();
            });

            return result;
        }

        /// <summary>
        /// 转换成 long
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Class<long> ToLong(this string s)
        {
            long result;

            if (long.TryParse(s, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 转换成 bool
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Class<bool> ToBool(this string s)
        {
            bool result;

            if (bool.TryParse(s, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}