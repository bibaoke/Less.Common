//bibaoke.com

namespace Less
{
    /// <summary>
    /// 结构体扩展
    /// </summary>
    public static class StructExtensions
    {
        /// <summary>
        /// 输出可空类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T? ToNullable<T>(this T t) where T : struct
        {
            return (T?)t;
        }
    }
}
