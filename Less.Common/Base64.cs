//bibaoke.com

using Less.Text;
using Newtonsoft.Json;
using System;

namespace Less
{
    /// <summary>
    /// 表示 Base64 字符串
    /// </summary>
    [JsonConverter(typeof(Base64JsonConverter))]
    public class Base64
    {
        /// <summary>
        /// 字符串值
        /// </summary>
        private string StringValue
        {
            get;
            set;
        }

        /// <summary>
        /// 字节值
        /// </summary>
        private byte[] ByteArrayValue
        {
            get;
            set;
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="stringValue">Base64 字符串</param>
        /// <exception cref="ArgumentNullException">Base64 字符串不能为 null</exception>
        /// <exception cref="FormatException">Base64 字符串格式不正确</exception>
        public Base64(string stringValue)
        {
            this.StringValue = stringValue;

            this.ByteArrayValue = Convert.FromBase64String(this.ConvertToStandardString(stringValue));
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="byteArrayValue">字节序列</param>
        /// <exception cref="ArgumentNullException">字节序列不能为 null</exception>
        public Base64(byte[] byteArrayValue)
        {
            this.StringValue = Convert.ToBase64String(byteArrayValue);

            this.ByteArrayValue = byteArrayValue;
        }

        /// <summary>
        /// 从 string 到 Base64 的隐式转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator Base64(string value)
        {
            return value.IsNotNull() ? new Base64(value) : null;
        }

        /// <summary>
        /// 从 Base64 到 string 的隐式转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator string(Base64 value)
        {
            return value.IsNotNull() ? value.ToString() : null;
        }

        /// <summary>
        /// 从 byte[] 到 Base64 的隐式转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator Base64(byte[] value)
        {
            return value.IsNotNull() ? new Base64(value) : null;
        }

        /// <summary>
        /// 从 Base64 到 byte[] 的隐式转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator byte[] (Base64 value)
        {
            return value.IsNotNull() ? value.ToByteArray() : null;
        }

        /// <summary>
        /// 比较两个实例是否相等
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator ==(Base64 l, Base64 r)
        {
            if (l.IsNotNull() && r.IsNotNull())
            {
                return l.ToString() == r.ToString();
            }

            return l.IsNull() && r.IsNull();
        }

        /// <summary>
        /// 比较两个实例是否不相等
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator !=(Base64 l, Base64 r)
        {
            return !(l == r);
        }

        /// <summary>
        /// 比较两个实例是否相等
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is Base64)
            {
                return this == (Base64)obj;
            }

            return false;
        }

        /// <summary>
        /// 重写获取哈希码方法
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        /// <summary>
        /// 输出标准 Base64 字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.ConvertToStandardString(this.StringValue);
        }

        /// <summary>
        /// 输出适用于 Url 的 Base64 字符串
        /// </summary>
        /// <returns></returns>
        public string ToUrlString()
        {
            return this.ConvertToUrlString(this.StringValue);
        }

        /// <summary>
        /// 输出字节序列
        /// </summary>
        /// <returns></returns>
        public byte[] ToByteArray()
        {
            return this.ByteArrayValue;
        }

        /// <summary>
        /// 输出 Hex 字符串
        /// </summary>
        /// <returns></returns>
        public HexString ToHexString()
        {
            return this.ByteArrayValue.ToHexString();
        }

        /// <summary>
        /// 转换成标准 Base64 字符串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private string ConvertToStandardString(string s)
        {
            return s.Replace("_", "+").Replace("-", "/") + "=".Repeat(s.Length % 4);
        }

        /// <summary>
        /// 转换成适用于 Url 的 Base64 字符串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private string ConvertToUrlString(string s)
        {
            return s.Replace("+", "_").Replace("/", "-").TrimEnd('=');
        }
    }
}
