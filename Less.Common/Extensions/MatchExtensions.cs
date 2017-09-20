//bibaoke.com

using System.Text.RegularExpressions;

namespace Less
{
    /// <summary>
    /// Match 的扩展方法
    /// </summary>
    public static class MatchExtensions
    {
        /// <summary>
        /// 获取匹配
        /// </summary>
        /// <param name="m"></param>
        /// <param name="groupName">分组</param>
        /// <returns></returns>
        public static string GetValue(this Match m, string groupName)
        {
            return m.Groups[groupName].Value;
        }

        /// <summary>
        /// 获取匹配
        /// </summary>
        /// <param name="m"></param>
        /// <param name="groupIndex">分组</param>
        /// <returns></returns>
        public static string GetValue(this Match m, int groupIndex)
        {
            return m.Groups[groupIndex].Value;
        }
    }
}
