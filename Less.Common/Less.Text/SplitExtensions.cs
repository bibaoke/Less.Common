//bibaoke.com

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Less.Collection;

namespace Less.Text
{
    /// <summary>
    /// 分隔
    /// </summary>
    public static class SplitExtensions
    {
        private static Regex WhiteSpacePattern
        {
            get;
            set;
        }

        static SplitExtensions()
        {
            SplitExtensions.WhiteSpacePattern = @"\s+".ToRegex(RegexOptions.Compiled | RegexOptions.Singleline);
        }

        /// <summary>
        /// 获取引用此实例地址的子字符串
        /// </summary>
        /// <param name="s"></param>
        /// <param name="startIndex">起始索引</param>
        /// <returns>返回引用此实例地址的子字符串</returns>
        /// <exception cref="NullReferenceException">字符串不能为 null</exception>
        /// <exception cref="ArgumentOutOfRangeException">起始索引不能小于零</exception>
        public static string SubstringUnsafe(this string s, int startIndex)
        {
            return s.SubstringUnsafe(startIndex, s.Length - startIndex);
        }

        /// <summary>
        /// 获取引用此实例地址的子字符串
        /// </summary>
        /// <param name="s"></param>
        /// <param name="startIndex">起始索引</param>
        /// <param name="length">长度</param>
        /// <returns>返回引用此实例地址的子字符串</returns>
        /// <exception cref="NullReferenceException">字符串不能为 null</exception>
        /// <exception cref="ArgumentOutOfRangeException">起始索引不能小于零</exception>
        /// <exception cref="ArgumentException">length 不能大于 startIndex 到数组末尾的元素数</exception>
        public static string SubstringUnsafe(this string s, int startIndex, int length)
        {
            if (startIndex < 0)
            {
                throw new ArgumentOutOfRangeException("startIndex", startIndex, "startIndex 不能小于零");
            }

            if (length > s.Length - startIndex)
            {
                throw new ArgumentException("length 不能大于 startIndex 到数组末尾的元素数", "length");
            }

            unsafe
            {
                fixed (char* p = s)
                {
                    return new string(p, startIndex, length);
                }
            }
        }

        /// <summary>
        /// 根据出现的第一个分隔符分隔字符串
        /// </summary>
        /// <param name="s"></param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">字符串不能为 null</exception>
        public static string[] SplitByFirst(this string s, char separator)
        {
            int index = s.IndexOf(separator);

            if (index < 0)
            {
                return s.ConstructArray();
            }
            else
            {
                return new string[] { s.Substring(0, index), s.Substring(index + 1) };
            }
        }

        /// <summary>
        /// 获取语料
        /// </summary>
        /// <param name="s"></param>
        /// <param name="func">断句委托 返回 true 表示断句 返回 false 表示连接</param>
        public static void GetCorpus(this string s, Func<string, bool> func)
        {
            s.GetCorpus((corpus, index) => func(corpus));
        }

        /// <summary>
        /// 获取语料
        /// </summary>
        /// <param name="s"></param>
        /// <param name="func">断句委托 返回 true 表示断句 返回 false 表示连接</param>
        public static void GetCorpus(this string s, Func<string, int, bool> func)
        {
            int index = 0;

            int length = 1;

            while (index < s.Length)
            {
                while (index + length <= s.Length)
                {
                    if (func(s.SubstringUnsafe(index, length), index))
                    {
                        index = index + length;
                    }
                    else
                    {
                        length++;
                    }
                }

                index++;

                length = 1;
            }
        }

        /// <summary>
        /// 根据换行符分隔字符串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string[] SplitByNewLine(this string s)
        {
            return s.Split(Symbol.NewLine, CaseOptions.CaseSensitive);
        }

        /// <summary>
        /// 根据空白字符分隔字符串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">参数为 null</exception>
        public static string[] SplitByWhiteSpace(this string s)
        {
            int index = 0;

            List<string> result = new List<string>();

            while (true)
            {
                ValueSet<int, int> tuple = s.IndexOfWhiteSpace(index, s.Length - index);

                if (tuple.IsNotNull())
                {
                    int length = tuple.Value1 - index;

                    if (length > 0)
                    {
                        result.Add(s.Substring(index, length));
                    }

                    index = tuple.Value1 + tuple.Value2;
                }
                else
                {
                    int length = s.Length - index;

                    if (length > 0)
                    {
                        result.Add(s.Substring(index, length));
                    }

                    break;
                }
            }

            return result.ToArray();
        }

        /// <summary>
        /// 空白字符的索引
        /// </summary>
        /// <param name="s"></param>
        /// <returns>第一个值是索引 第二个值是长度</returns>
        /// <exception cref="ArgumentNullException">参数为 null</exception>
        public static ValueSet<int, int> IndexOfWhiteSpace(this string s)
        {
            return s.IndexOfWhiteSpace(0);
        }

        /// <summary>
        /// 空白字符的索引
        /// </summary>
        /// <param name="s"></param>
        /// <param name="startIndex"></param>
        /// <returns>第一个值是索引 第二个值是长度</returns>
        /// <exception cref="ArgumentNullException">参数为 null</exception>
        public static ValueSet<int, int> IndexOfWhiteSpace(this string s, int startIndex)
        {
            return s.IndexOfWhiteSpace(startIndex, s.Length - startIndex);
        }

        /// <summary>
        /// 空白字符的索引
        /// </summary>
        /// <param name="s"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns>第一个值是索引 第二个值是长度</returns>
        /// <exception cref="ArgumentNullException">参数为 null</exception>
        public static ValueSet<int, int> IndexOfWhiteSpace(this string s, int startIndex, int count)
        {
            Match match = SplitExtensions.WhiteSpacePattern.Match(s, startIndex, count);

            if (match.Success)
            {
                return new ValueSet<int, int>(match.Index, match.Length);
            }

            return null;
        }

        /// <summary>
        /// 分隔字符串
        /// </summary>
        /// <param name="s">要分隔的字符串</param>
        /// <param name="separator">分隔符</param>
        /// <returns>分隔后的字符串数组</returns>
        public static string[] Split(this string s, string separator)
        {
            return s.Split(separator, CaseOptions.CaseSensitive);
        }

        /// <summary>
        /// 分隔字符串
        /// </summary>
        /// <param name="s">要分隔的字符串</param>
        /// <param name="separator">分隔符</param>
        /// <param name="caseOption">大小写选项</param>
        /// <returns>分隔后的字符串数组</returns>
        public static string[] Split(this string s, string separator, CaseOptions caseOption)
        {
            return s.Split(separator, caseOption, SeparatorOptions.NoSeparator);
        }

        /// <summary>
        /// 分隔字符串
        /// </summary>
        /// <param name="s">要分隔的字符串</param>
        /// <param name="separator">分隔符</param>
        /// <param name="caseOption">大小写选项</param>
        /// <param name="separatorOption">分隔符选项</param>
        /// <returns>分隔后的字符串数组</returns>
        public static string[] Split(this string s, string separator, CaseOptions caseOption, SeparatorOptions separatorOption)
        {
            //根据大小写选项决定字符串查找选项
            StringComparison comparison = caseOption.ToStringComparison();

            //查找起始索引
            int startIndex = 0;

            //找到的分隔符索引
            int separatorIndex = 0;

            //分隔出来的字符串列表
            List<string> result = new List<string>();

            //在文本中查找分隔符
            while ((separatorIndex = s.IndexOf(separator, startIndex, comparison)) > -1)
            {
                //字符串截取起始索引
                int substringStartIndex = startIndex;

                //字符串截取长度
                int substringCount = separatorIndex - startIndex;

                //根据分隔符选项决定截取起始索引和截取长度
                switch (separatorOption)
                {
                    case SeparatorOptions.NoSeparator:
                        break;
                    case SeparatorOptions.PostSeparator:
                        substringCount += separator.Length;
                        break;
                    case SeparatorOptions.PreSeparator:
                        substringStartIndex -= separator.Length;
                        substringCount += separator.Length;
                        break;
                }

                //如果分隔符不在位置0
                //把分隔符前的内容加入结果队列
                if (separatorIndex > 0)
                {
                    result.Add(s.Substring(substringStartIndex, substringCount));
                }

                //更新起始索引
                startIndex = separatorIndex + separator.Length;
            }

            //剩余长度
            int restCount = s.Length - startIndex;

            //如果还有剩余内容
            //把剩余的内容加入结果队列
            if (restCount > 0)
            {
                result.Add(s.Substring(startIndex, restCount));
            }

            return result.ToArray();
        }
    }
}
