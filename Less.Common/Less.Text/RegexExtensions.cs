//bibaoke.com

using System.Text.RegularExpressions;

namespace Less.Text
{
    /// <summary>
    /// 正则匹配
    /// </summary>
    public static class RegexExtensions
    {
        /// <summary>
        /// 把正则模式修改为全局匹配
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string GlobalMatching(this string pattern)
        {
            if (!pattern.StartsWith("^"))
            {
                pattern = "^" + pattern;
            }

            if (!pattern.EndsWith("$"))
            {
                pattern = pattern + "$";
            }

            return pattern;
        }

        /// <summary>
        /// 查找所有匹配
        /// </summary>
        /// <param name="s"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        public static MatchCollection FindAll(this string s, string regex)
        {
            return regex.Matches(s);
        }

        /// <summary>
        /// 查找所有匹配
        /// </summary>
        /// <param name="s"></param>
        /// <param name="regex"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static MatchCollection FindAll(this string s, string regex, RegexOptions options)
        {
            return regex.Matches(s, options);
        }

        /// <summary>
        /// 查找所有匹配
        /// </summary>
        /// <param name="s"></param>
        /// <param name="regex"></param>
        /// <param name="beginning"></param>
        /// <returns></returns>
        public static MatchCollection FindAll(this string s, string regex, int beginning)
        {
            return regex.Matches(s, beginning);
        }

        /// <summary>
        /// 查找所有匹配
        /// </summary>
        /// <param name="s"></param>
        /// <param name="regex"></param>
        /// <param name="beginning"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static MatchCollection FindAll(this string s, string regex, int beginning, RegexOptions options)
        {
            return regex.Matches(s, beginning, options);
        }

        /// <summary>
        /// 根据正则表达式查找内容
        /// </summary>
        /// <param name="s"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        public static Match Find(this string s, string regex)
        {
            return regex.Match(s);
        }

        /// <summary>
        /// 根据正则表达式查找内容
        /// </summary>
        /// <param name="s"></param>
        /// <param name="regex"></param>
        /// <param name="beginning"></param>
        /// <returns></returns>
        public static Match Find(this string s, string regex, int beginning)
        {
            return regex.Match(s, beginning);
        }

        /// <summary>
        /// 根据正则表达式查找内容
        /// </summary>
        /// <param name="s"></param>
        /// <param name="options"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        public static Match Find(this string s, string regex, RegexOptions options)
        {
            return regex.Match(s, options);
        }

        /// <summary>
        /// 根据正则表达式查找内容
        /// </summary>
        /// <param name="s"></param>
        /// <param name="regex"></param>
        /// <param name="beginning"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static Match Find(this string s, string regex, int beginning, int length)
        {
            return regex.Match(s, beginning, length);
        }

        /// <summary>
        /// 根据正则表达式查找内容
        /// </summary>
        /// <param name="s"></param>
        /// <param name="regex"></param>
        /// <param name="beginning"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static Match Find(this string s, string regex, int beginning, RegexOptions options)
        {
            return regex.Match(s, beginning, options);
        }

        /// <summary>
        /// 根据正则表达式查找内容
        /// </summary>
        /// <param name="s"></param>
        /// <param name="regex"></param>
        /// <param name="beginning"></param>
        /// <param name="length"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static Match Find(this string s, string regex, int beginning, int length, RegexOptions options)
        {
            return regex.Match(s, beginning, length, options);
        }

        /// <summary>
        /// 正则匹配
        /// </summary>
        /// <param name="s"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static MatchCollection Matches(this string s, string input)
        {
            return s.Matches(input, RegexOptions.None);
        }

        /// <summary>
        /// 正则匹配
        /// </summary>
        /// <param name="s"></param>
        /// <param name="input"></param>
        /// <param name="beginning"></param>
        /// <returns></returns>
        public static MatchCollection Matches(this string s, string input, int beginning)
        {
            return s.Matches(input, beginning, RegexOptions.None);
        }

        /// <summary>
        /// 正则匹配
        /// </summary>
        /// <param name="s"></param>
        /// <param name="input"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static MatchCollection Matches(this string s, string input, RegexOptions options)
        {
            return s.Matches(input, 0, options);
        }

        /// <summary>
        /// 正则匹配
        /// </summary>
        /// <param name="s"></param>
        /// <param name="input"></param>
        /// <param name="beginning"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static MatchCollection Matches(this string s, string input, int beginning, RegexOptions options)
        {
            return s.ToRegex(options).Matches(input, beginning);
        }

        /// <summary>
        /// 正则匹配
        /// </summary>
        /// <param name="s"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Match Match(this string s, string input)
        {
            return s.Match(input, RegexOptions.None);
        }

        /// <summary>
        /// 正则匹配
        /// </summary>
        /// <param name="s"></param>
        /// <param name="input"></param>
        /// <param name="beginning"></param>
        /// <returns></returns>
        public static Match Match(this string s, string input, int beginning)
        {
            return s.Match(input, beginning, RegexOptions.None);
        }

        /// <summary>
        /// 正则匹配
        /// </summary>
        /// <param name="s"></param>
        /// <param name="input"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static Match Match(this string s, string input, RegexOptions options)
        {
            return s.Match(input, 0, options);
        }

        /// <summary>
        /// 正则匹配
        /// </summary>
        /// <param name="s"></param>
        /// <param name="input"></param>
        /// <param name="beginning"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static Match Match(this string s, string input, int beginning, int length)
        {
            return s.Match(input, beginning, length, RegexOptions.None);
        }

        /// <summary>
        /// 正则匹配
        /// </summary>
        /// <param name="s"></param>
        /// <param name="input"></param>
        /// <param name="beginning"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static Match Match(this string s, string input, int beginning, RegexOptions options)
        {
            return s.Match(input, beginning, input.Length - beginning, options);
        }

        /// <summary>
        /// 正则匹配
        /// </summary>
        /// <param name="s"></param>
        /// <param name="input"></param>
        /// <param name="beginning"></param>
        /// <param name="length"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static Match Match(this string s, string input, int beginning, int length, RegexOptions options)
        {
            return s.ToRegex(options).Match(input, beginning, length);
        }
    }
}
