//bibaoke.com

using System;
using System.Collections;
using System.Collections.Generic;

namespace Less.Collection
{
    /// <summary>
    /// 数组枚举器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ArrayEnumerator<T> : IEnumerator<T>, IEnumerable<T>
    {
        private T[] Array;

        private int StartIndex;

        private int Count;

        private int CurrentIndex;

        private int StopIndex;

        private int ArrayLength;

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="array">要枚举的数组</param>
        /// <param name="startIndex">起始索引</param>
        /// <exception cref="ArgumentOutOfRangeException">startIndex 不能小于零</exception>
        public ArrayEnumerator(T[] array, int startIndex) : this(array, startIndex, array.Length - startIndex)
        {
            //
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="array">要枚举的数组</param>
        /// <param name="startIndex">起始索引</param>
        /// <param name="count">枚举次数</param>
        /// <exception cref="ArgumentOutOfRangeException">startIndex 不能小于零</exception>
        /// <exception cref="ArgumentException">count 不能大于 startIndex 到数组末尾的元素数</exception>
        public ArrayEnumerator(T[] array, int startIndex, int count)
        {
            if (startIndex < 0)
            {
                throw new ArgumentOutOfRangeException("startIndex", startIndex, "startIndex 不能小于零");
            }

            if (count > array.Length - startIndex)
            {
                throw new ArgumentException("count 不能大于 startIndex 到数组末尾的元素数", "count");
            }

            this.Array = array;
            this.StartIndex = startIndex;
            this.Count = count;

            this.CurrentIndex = startIndex - 1;

            this.StopIndex = startIndex + count;

            this.ArrayLength = this.Array.Length;
        }

        /// <summary>
        /// 获取集合中位于枚举数当前位置的元素
        /// </summary>
        /// <exception cref="InvalidOperationException">在创建了枚举数后集合被修改了</exception>
        public T Current
        {
            get
            {
                if (this.Array.Length != this.ArrayLength)
                {
                    throw new InvalidOperationException("在创建了枚举数后集合被修改了");
                }

                return this.Array[CurrentIndex];
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }

        /// <summary>
        /// 不需要 没有非托管资源需要释放
        /// </summary>
        public void Dispose()
        {
            //
        }

        /// <summary>
        /// 将枚举数推进到集合的下一个元素
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            this.CurrentIndex++;

            if (this.CurrentIndex < this.StopIndex)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 将枚举数设置为其初始位置，该位置位于集合中第一个元素之前
        /// </summary>
        public void Reset()
        {
            this.CurrentIndex = this.StartIndex - 1;
        }

        /// <summary>
        /// 返回一个循环访问集合的枚举数
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }
    }
}
