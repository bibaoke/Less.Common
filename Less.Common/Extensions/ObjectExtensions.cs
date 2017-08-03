﻿//bibaoke.com

using System;

namespace Less
{
    /// <summary>
    /// 对象基类的扩展方法
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
        /// <typeparam name="T">类型</typeparam>
        /// <param name="t">实例</param>
        /// <param name="instead">替代实例</param>
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
