//bibaoke.com

using Less.Collection;
using Less.Text;
using System.Collections.Generic;
using System.Text;
using System;

namespace Less
{
    /// <summary>
    /// 字符类型的扩展方法
    /// </summary>
    public static class CharExtensions
    {
        private static Dictionary<char, char> NumberMap
        {
            get;
            set;
        }

        static CharExtensions()
        {
            CharExtensions.NumberMap = new Dictionary<char, char>();

            CharExtensions.NumberMap.Add('0', '零');
            CharExtensions.NumberMap.Add('1', '一');
            CharExtensions.NumberMap.Add('2', '二');
            CharExtensions.NumberMap.Add('3', '三');
            CharExtensions.NumberMap.Add('4', '四');
            CharExtensions.NumberMap.Add('5', '五');
            CharExtensions.NumberMap.Add('6', '六');
            CharExtensions.NumberMap.Add('7', '七');
            CharExtensions.NumberMap.Add('8', '八');
            CharExtensions.NumberMap.Add('9', '九');
        }

        /// <summary>
        /// 获取引用此实例地址的字符串
        /// </summary>
        /// <param name="array"></param>
        /// <returns>返回引用此实例地址的字符串</returns>
        public static string GetString(this char[] array)
        {
            return array.GetString(0, array.Length);
        }

        /// <summary>
        /// 获取引用此实例地址的字符串
        /// </summary>
        /// <param name="array"></param>
        /// <param name="startIndex">起始索引</param>
        /// <returns>返回引用此实例地址的字符串</returns>
        /// <exception cref="ArgumentOutOfRangeException">起始索引不能小于零</exception>
        public static string GetString(this char[] array, int startIndex)
        {
            return array.GetString(startIndex, array.Length - startIndex);
        }

        /// <summary>
        /// 获取引用此实例地址的字符串
        /// </summary>
        /// <param name="array"></param>
        /// <param name="startIndex">起始索引</param>
        /// <param name="length">长度</param>
        /// <returns>返回引用此实例地址的字符串</returns>
        /// <exception cref="ArgumentOutOfRangeException">起始索引不能小于零</exception>
        /// <exception cref="ArgumentException">length 不能大于 startIndex 到数组末尾的元素数</exception>
        public static string GetString(this char[] array, int startIndex, int length)
        {
            if (array.IsNull())
            {
                throw new ArgumentNullException("array", "数组不能为 nul");
            }

            if (startIndex < 0)
            {
                throw new ArgumentOutOfRangeException("startIndex", startIndex, "startIndex 不能小于零");
            }

            if (length > array.Length - startIndex)
            {
                throw new ArgumentException("length 不能大于 startIndex 到数组末尾的元素数", "length");
            }

            if (array.Length > 0)
            {
                unsafe
                {
                    fixed (char* p = &array[0])
                    {
                        return new string(p, startIndex, length);
                    }
                }
            }
            else
            {
                unsafe
                {
                    fixed (char* p = array)
                    {
                        return new string(p);
                    }
                }
            }
        }

        /// <summary>
        /// 是否 unicode 字符
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsUnicode(this char c)
        {
            return Encoding.UTF8.GetByteCount(c.ConstructArray()) >= 2;
        }

        /// <summary>
        /// 转换为大写
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static char ToUpper(this char c)
        {
            return char.ToUpper(c);
        }

        /// <summary>
        /// 转换为小写
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static char ToLower(this char c)
        {
            return char.ToLower(c);
        }

        /// <summary>
        /// 是否英文字符
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsEnglish(this char c)
        {
            if (c >= 65 && c <= 90)
            {
                return true;
            }

            if (c >= 97 && c <= 122)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 转换为中文数字字符
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static char ToChineseNumber(this char c)
        {
            return CharExtensions.NumberMap[c];
        }
    }
}
