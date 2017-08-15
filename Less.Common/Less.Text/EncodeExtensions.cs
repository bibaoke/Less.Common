//bibaoke.com

using System.Web;

namespace Less.Text
{
    /// <summary>
    /// 编码
    /// </summary>
    public static class EncodeExtensions
    {
        /// <summary>
        /// url 解码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string UrlDecode(this string s)
        {
            return HttpUtility.UrlDecode(s);
        }

        /// <summary>
        /// url 编码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string UrlEncode(this string s)
        {
            return HttpUtility.UrlEncode(s);
        }

        /// <summary>
        /// html 解码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string HtmlDecode(this string s)
        {
            return HttpUtility.HtmlDecode(s);
        }

        /// <summary>
        /// html 编码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string HtmlEncode(this string s)
        {
            return HttpUtility.HtmlEncode(s);
        }
    }
}
