using System;

namespace Less.Text
{
    /// <summary>
    /// CaseOptions 的扩展方法
    /// </summary>
    public static class CaseOptionsExtensions
    {
        /// <summary>
        /// 根据大小写选项决定字符串查找选项
        /// </summary>
        /// <param name="caseOption"></param>
        /// <returns></returns>
        public static StringComparison ToStringComparison(this CaseOptions caseOption)
        {
            if (caseOption == CaseOptions.CaseSensitive)
            {
                return StringComparison.Ordinal;
            }
            else
            {
                return StringComparison.OrdinalIgnoreCase;
            }
        }
    }
}
