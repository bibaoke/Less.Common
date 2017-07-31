//bibaoke.com

using System;

namespace Less
{
    /// <summary>
    /// 对象基类的扩展方法
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// 在实例中获取值
        /// </summary>
        /// <typeparam name="T">需要取值实例的类型</typeparam>
        /// <typeparam name="U">取值函数的返回类型</typeparam>
        /// <param name="t"></param>
        /// <param name="func">取值函数</param>
        /// <returns>返回取值函数的结果</returns>
        public static U Get<T, U>(this T t, Func<T, U> func)
        {
            return func(t);
        }

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
        /// <returns></returns>
        public static bool IsDefault<T>(this T t) where T : struct
        {
            return t.Equals(default(T));
        }

        /// <summary>
        /// 如果不是空引用
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool IsNotNull(this object o)
        {
            return o != null;
        }

        /// <summary>
        /// 如果是空引用
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="t">实例</param>
        /// <param name="instead">替代实例</param>
        public static T IsNull<T>(this T t, T instead) where T : class
        {
            return t.IsNull() ? instead : t;
        }

        /// <summary>
        /// 如果是空引用
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool IsNull(this object o)
        {
            return o == null;
        }
    }
}
