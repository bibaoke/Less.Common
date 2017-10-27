//bibaoke.com

using System;

namespace Less
{
    /// <summary>
    /// object 的扩展方法
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// 如果不是默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsNotDefault<T>(this T t) where T : struct
        {
            return !t.IsDefault();
        }

        /// <summary>
        /// 如果是默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="instead">替代值</param>
        /// <returns></returns>
        public static T IsDefault<T>(this T t, T instead) where T : struct
        {
            return t.IsDefault() ? instead : t;
        }

        /// <summary>
        /// 如果是默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsDefault<T>(this T t) where T : struct
        {
            return t.Equals(default(T));
        }

        /// <summary>
        /// 如果不是 null
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool IsNotNull(this object o)
        {
            return o != null;
        }

        /// <summary>
        /// 如果是 null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="instead">替代实例</param>
        /// <returns></returns>
        public static T IsNull<T>(this T t, T instead) where T : class
        {
            return t.IsNull() ? instead : t;
        }

        /// <summary>
        /// 如果是 null
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool IsNull(this object o)
        {
            return o == null;
        }
    }
}
