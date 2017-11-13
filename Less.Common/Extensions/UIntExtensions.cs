//bibaoke.com

using Less.Text;
using System;

namespace Less
{
    /// <summary>
    /// uint 的扩展方法
    /// </summary>
    public static class UintExtensions
    {
        /// <summary>
        /// 转换为 int
        /// </summary>
        /// <param name="ui"></param>
        /// <returns></returns>
        public static int ToInt(this uint ui)
        {
            return Convert.ToInt32(ui);
        }
    }
}
