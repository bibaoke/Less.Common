//bibaoke.com

using System;
using System.Collections.Generic;

namespace Less.Collection
{
    /// <summary>
    /// 数组扩展方法
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// 获取数组迭代器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="startIndex">起始索引</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">startIndex 不能小于零</exception>
        public static ArrayEnumerator<T> GetEnumerator<T>(this T[] array, int startIndex)
        {
            return new ArrayEnumerator<T>(array, startIndex);
        }

        /// <summary>
        /// 获取数组迭代器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="startIndex">起始索引</param>
        /// <param name="count">迭代次数</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">startIndex 不能小于零</exception>
        /// <exception cref="ArgumentException">count 不能大于 startIndex 到数组末尾的元素数</exception>
        public static ArrayEnumerator<T> GetEnumerator<T>(this T[] array, int startIndex, int count)
        {
            return new ArrayEnumerator<T>(array, startIndex, count);
        }

        /// <summary>
        /// 倒序迭代
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="action"></param>
        public static void EachDesc<T>(this T[] array, Action<T> action)
        {
            array.EachDesc((index, item) =>
            {
                action(item);
            });
        }

        /// <summary>
        /// 倒序迭代
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="action"></param>
        public static void EachDesc<T>(this T[] array, Action<int, T> action)
        {
            array.EachDesc((index, item) =>
            {
                action(index, item);

                return true;
            });
        }

        /// <summary>
        /// 倒序迭代
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="func"></param>
        public static void EachDesc<T>(this T[] array, Func<int, T, bool> func)
        {
            array.Length.EachDesc(i =>
            {
                return func(i, array[i]);
            });
        }

        /// <summary>
        /// 降序排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns>降序数组</returns>
        public static T[] SortDescending<T>(this T[] array)
        {
            List<T> list = new List<T>(array);

            list.Sort();

            list.Reverse();

            return list.ToArray();
        }

        /// <summary>
        /// 升序排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns>升序数组</returns>
        public static T[] Sort<T>(this T[] array)
        {
            List<T> list = new List<T>(array);

            list.Sort();

            return list.ToArray();
        }

        /// <summary>
        /// 扩展数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="extension">扩展元素</param>
        /// <returns></returns>
        /// <exception cref="OutOfMemoryException">内存不足</exception>
        /// <exception cref="OverflowException">数组超过了最大长度</exception>
        public static T[] ExtArray<T>(this T[] array, params T[] extension)
        {
            T[] result = new T[array.Length + extension.Length];

            Array.Copy(array, result, array.Length);

            Array.Copy(extension, 0, result, array.Length, extension.Length);

            return result;
        }

        /// <summary>
        /// 扩展数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="length">新长度</param>
        /// <returns></returns>
        /// <exception cref="OutOfMemoryException">内存不足</exception>
        /// <exception cref="OverflowException">数组超过了最大长度</exception>
        public static T[] ExtArray<T>(this T[] array, int length)
        {
            T[] result = new T[length];

            Array.Copy(array, result, array.Length);

            return result;
        }

        /// <summary>
        /// 获取子数组
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="array">父数组</param>
        /// <param name="startIndex">起始索引</param>
        /// <returns>子数组</returns>
        /// <exception cref="NullReferenceException">数组不能为 null</exception>
        /// <exception cref="ArgumentOutOfRangeException">startIndex 不能小于数组的下限</exception>
        public static T[] SubArray<T>(this T[] array, int startIndex)
        {
            T[] result = new T[array.Length - startIndex];

            Array.Copy(array, startIndex, result, 0, result.Length);

            return result;
        }

        /// <summary>
        /// 获取子数组
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="array">父数组</param>
        /// <param name="startIndex">起始索引</param>
        /// <param name="count">元素个数</param>
        /// <returns>子数组</returns>
        /// <exception cref="ArgumentNullException">数组不能为 null</exception>
        /// <exception cref="ArgumentOutOfRangeException">startIndex 不能小于数组的下限</exception>
        /// <exception cref="ArgumentException">count 不能大于 startIndex 到数组末尾的元素数</exception>
        /// <exception cref="OverflowException">count 超出范围</exception>
        public static T[] SubArray<T>(this T[] array, int startIndex, int count)
        {
            T[] result = new T[count];

            Array.Copy(array, startIndex, result, 0, count);

            return result;
        }

        /// <summary>
        /// 复制数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns>返回复制的数组副本</returns>
        /// <exception cref="NullReferenceException">数组不能为 null</exception>
        /// <exception cref="RankException">不能复制多维数组</exception>
        public static T[] Copy<T>(this T[] array)
        {
            T[] clone = new T[array.Length];

            array.CopyTo(clone, 0);

            return clone;
        }
    }
}
