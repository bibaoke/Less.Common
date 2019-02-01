//bibaoke.com

using Less.Text;
using System;

namespace Less
{
    /// <summary>
    /// int 的扩展方法
    /// </summary>
    public static class IntExtensions
    {
        /// <summary>
        /// 返回指定次幂
        /// </summary>
        /// <param name="i"></param>
        /// <param name="power">次幂</param>
        /// <returns></returns>
        public static int Pow(this int i, int power)
        {
            return (int)Math.Pow(i, power);
        }

        /// <summary>
        /// 是否与数组中的任意一项相等
        /// </summary>
        /// <param name="i"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool In(this int i, params int[] array)
        {
            foreach (int item in array)
            {
                if (i == item)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 是否偶数
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static bool IsEvenNumber(this int i)
        {
            return i != 0 && i % 2 == 0;
        }

        /// <summary>
        /// 以此实例为最大值 生成非负随机数 不包括最大值
        /// </summary>
        /// <returns></returns>
        public static int Random(this int i)
        {
            return RandomDef.Ins.Next(i);
        }

        /// <summary>
        /// 输出指定长度的字符串
        /// 数字长度不足在前面补零
        /// </summary>
        /// <param name="i"></param>
        /// <param name="length">字符串长度</param>
        /// <returns></returns>
        public static string ToString(this int i, int length)
        {
            string original = i.ToString();

            return "0".Repeat(length - original.Length) + original;
        }

        /// <summary>
        /// 是否在指定的范围之间
        /// </summary>
        /// <param name="i"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static bool Between(this int i, int begin, int end)
        {
            return i >= begin && i <= end;
        }
    }
}
