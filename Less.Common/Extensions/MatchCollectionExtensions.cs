//bibaoke.com

using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Less
{
    /// <summary>
    /// MatchCollection 的扩展方法
    /// </summary>
    public static class MatchCollectionExtensions
    {
        /// <summary>
        /// 获取匹配
        /// </summary>
        /// <param name="m"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public static string[] GetValues(this MatchCollection m, string groupName)
        {
            List<string> list = new List<string>();

            foreach (Match i in m)
            {
                list.Add(i.Groups[groupName].Value);
            }

            return list.ToArray();
        }

        /// <summary>
        /// 获取匹配
        /// </summary>
        /// <param name="m"></param>
        /// <param name="groupIndex"></param>
        /// <returns></returns>
        public static string[] GetValues(this MatchCollection m, int groupIndex)
        {
            List<string> list = new List<string>();

            foreach (Match i in m)
            {
                list.Add(i.Groups[groupIndex].Value);
            }

            return list.ToArray();
        }
    }
}
