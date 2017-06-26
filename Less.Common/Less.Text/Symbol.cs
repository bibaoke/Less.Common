//bibaoke.com

namespace Less.Text
{
    /// <summary>
    /// 提供常用符号
    /// </summary>
    public static class Symbol
    {
        /// <summary>
        /// 半角空格
        /// </summary>
        public static string Space
        {
            get { return " "; }
        }

        /// <summary>
        /// 全角空格
        /// </summary>
        public static string FullWidthSpace
        {
            get { return "　"; }
        }

        /// <summary>
        /// 制表符
        /// </summary>
        public static string Tab
        {
            get { return "  "; }
        }

        /// <summary>
        /// 换车符加换行符
        /// </summary>
        public static string NewLine
        {
            get { return "\r\n"; }
        }

        /// <summary>
        /// 回车符
        /// </summary>
        public static char EnterChar
        {
            get { return '\r'; }
        }

        /// <summary>
        /// 换行符
        /// </summary>
        public static char NewLineChar
        {
            get { return '\n'; }
        }
    }
}
