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
        /// <param name="func">语料库检查委托 如果在语料库中存在返回 true 否则返回 false</param>
        public static void GetCorpus(this string s, Func<string, bool> func)
        {
            int index = 0;

            int length = 1;

            while (index < s.Length)
            {
                while (index + length <= s.Length)
                {
                    if (func(s.Substring(index, length)))
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
        public static string[] SplitByWhiteSpace(this string s)
        {
            int index = 0;

            List<string> result = new List<string>();

            while (true)
            {
                ValueSet<int, int> tuple = s.IndexOfWhiteSpace(index, s.Length - index);

                if (tuple.IsNotNull())
                {
                    result.Add(s.Substring(index, tuple.Value1 - index));

                    index = tuple.Value1 + tuple.Value2;
                }
                else
                {
                    int length = s.Length - index;

                    if (length > 0)
                        result.Add(s.Substring(index, length));

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
        public static ValueSet<int, int> IndexOfWhiteSpace(this string s, int startIndex, int count)
        {
            Match match = SplitExtensions.WhiteSpacePattern.Match(s, startIndex, count);

            if (match.Success)
                return new ValueSet<int, int>(match.Index, match.Length);

            return null;
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
