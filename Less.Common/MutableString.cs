//bibaoke.com

using System.Collections.Generic;

namespace Less
{
    /// <summary>
    /// 可变的字符串
    /// </summary>
    public class MutableString
    {
        private List<char> CharList
        {
            get;
            set;
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="value"></param>
        public MutableString(string value) : this(value.ToCharArray())
        {
            //
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="value"></param>
        public MutableString(char[] value)
        {
            this.CharList = new List<char>(value);
        }

        /// <summary>
        /// 输出字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return new string(this.CharList.ToArray());
        }

        /// <summary>
        /// 从 string 到 MutableString 的转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator MutableString(string value)
        {
            return new MutableString(value);
        }

        /// <summary>
        /// 从 MutableString 到 string 的转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator string(MutableString value)
        {
            return value.IsNull() ? null : value.ToString();
        }
    }
}
