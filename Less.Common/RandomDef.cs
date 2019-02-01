//bibaoke.com

using System;

namespace Less
{
    /// <summary>
    /// 默认种子值的 Random
    /// </summary>
    public static class RandomDef
    {
        /// <summary>
        /// 实例
        /// </summary>
        public static Random Ins
        {
            get;
            private set;
        }

        static RandomDef()
        {
            RandomDef.Ins = new Random();
        }
    }
}
