//bibaoke.com

using System.Collections.Generic;

namespace Less.Collection
{
    /// <summary>
    /// HashSet 扩展方法
    /// </summary>
    public static class HashSetExtensions
    {
        /// <summary>
        /// 复制到数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hashSet"></param>
        /// <returns></returns>
        public static T[] ToArray<T>(this HashSet<T> hashSet)
        {
            T[] array = new T[hashSet.Count];

            hashSet.CopyTo(array);

            return array;
        }
    }
}
