//bibaoke.com

using System;
using System.Collections.Generic;

namespace Less.Collection
{
    /// <summary>
    /// 列表扩展方法
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// 获取列表枚举器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="startIndex">起始索引</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">startIndex 不能小于零</exception>
        public static ListEnumerator<T> GetEnumerator<T>(this List<T> list, int startIndex)
        {
            return new ListEnumerator<T>(list, startIndex);
        }

        /// <summary>
        /// 获取列表枚举器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="startIndex">起始索引</param>
        /// <param name="count">枚举次数</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">startIndex 不能小于零</exception>
        /// <exception cref="ArgumentException">count 不能大于 startIndex 到列表末尾的元素数</exception>
        public static ListEnumerator<T> GetEnumerator<T>(this List<T> list, int startIndex, int count)
        {
            return new ListEnumerator<T>(list, startIndex, count);
        }
    }
}
