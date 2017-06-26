//bibaoke.com

using System.Text;
using System.Linq;
using System.Security.Cryptography;
using Less.Text;
using Less.Collection;

namespace Less.Encrypt
{
    /// <summary>
    /// 提供加密方法
    /// </summary>
    public static class Encrypt
    {
        /// <summary>
        /// 把若干个字符串连接起来进行MD5 加密
        /// </summary>
        /// <param name="s">要加密的字符串</param>
        /// <param name="e">字符串编码</param>
        /// <param name="sortByValue">对字符串的值进行排序</param>
        /// <returns>散列值</returns>
        public static HexString MD5(this string[] s, Encoding e, bool sortByValue)
        {
            return sortByValue ? s.Sort().MD5(e) : s.MD5(e);
        }

        /// <summary>
        /// 把若干个字符串连接起来进行MD5 加密
        /// </summary>
        /// <param name="s">要加密的字符串</param>
        /// <param name="e">字符串编码</param>
        /// <returns>散列值</returns>
        public static HexString MD5(this string[] s, Encoding e)
        {
            return s.Join().MD5(e);
        }

        /// <summary>
        /// MD5 加密
        /// </summary>
        /// <param name="s">要加密的字符串</param>
        /// <param name="e">字符串编码</param>
        /// <returns>散列值</returns>
        public static HexString MD5(this string s, Encoding e)
        {
            return new MD5CryptoServiceProvider().ComputeHash(s.ToByteArray(e));
        }

        /// <summary>
        /// 把若干个字符串连接起来进行SHA1 加密
        /// </summary>
        /// <param name="s">要加密的字符串</param>
        /// <param name="e">字符串编码</param>
        /// <param name="sortByValue">对字符串的值进行排序</param>
        /// <returns>散列值</returns>
        public static HexString SHA1(this string[] s, Encoding e, bool sortByValue)
        {
            return sortByValue ? s.Sort().SHA1(e) : s.SHA1(e);
        }

        /// <summary>
        /// 把若干个字符串连接起来进行SHA1 加密
        /// </summary>
        /// <param name="s">要加密的字符串</param>
        /// <param name="e">字符串编码</param>
        /// <returns>散列值</returns>
        public static HexString SHA1(this string[] s, Encoding e)
        {
            return s.Join().SHA1(e);
        }

        /// <summary>
        /// SHA1 加密
        /// </summary>
        /// <param name="s">要加密的字符串</param>
        /// <param name="e">字符串编码</param>
        /// <returns>散列值</returns>
        public static HexString SHA1(this string s, Encoding e)
        {
            return new SHA1CryptoServiceProvider().ComputeHash(s.ToByteArray(e));
        }
    }
}
