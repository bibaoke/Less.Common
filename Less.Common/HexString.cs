//bibaoke.com

using Less.Text;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Less
{
    /// <summary>
    /// 表示 Hex 字符串
    /// </summary>
    [JsonConverter(typeof(HexStringJsonConverter))]
    public class HexString
    {
        /// <summary>
        /// 字节值
        /// </summary>
        private byte[] ByteArrayValue
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置指定索引的字节
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public byte this[int index]
        {
            get
            {
                return this.ByteArrayValue[index];
            }
            set
            {
                this.ByteArrayValue[index] = value;
            }
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="stringValue">Hex 字符串</param>
        /// <exception cref="ArgumentNullException">Hex 字符串不能为 null</exception>
        /// <exception cref="FormatException">Hex 字符串格式不正确</exception>
        public HexString(string stringValue)
        {
            if (stringValue.IsNull())
            {
                throw new ArgumentNullException("stringValue", "Hex 字符串不能为 null");
            }

            stringValue = stringValue.TrimStart("0x").Clear("-");

            List<byte> list = new List<byte>(stringValue.Length / 2);

            for (int i = 0; i < stringValue.Length; i = i + 2)
            {
                list.Add(byte.Parse(stringValue.SubstringUnsafe(i, 2), NumberStyles.HexNumber));
            }

            this.ByteArrayValue = list.ToArray();
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="byteArrayValue">字节序列</param>
        /// <exception cref="ArgumentNullException">字节序列不能为 null</exception>
        public HexString(byte[] byteArrayValue)
        {
            if (byteArrayValue.IsNull())
            {
                throw new ArgumentNullException("byteArrayValue", "字节序列不能为 null");
            }

            this.ByteArrayValue = byteArrayValue;
        }

        /// <summary>
        /// 重载操作符
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator ==(HexString l, HexString r)
        {
            if (l.IsNotNull() && r.IsNotNull())
            {
                return l.ToString() == r.ToString();
            }

            return l.IsNull() && r.IsNull();
        }

        /// <summary>
        /// 重载操作符
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator !=(HexString l, HexString r)
        {
            return !(l == r);
        }

        /// <summary>
        /// 比较两个 HexString 是否相等
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is HexString)
            {
                return this == (HexString)obj;
            }

            return false;
        }

        /// <summary>
        /// 哈希
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        /// <summary>
        /// 从 byte[] 到 HexString 的转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator HexString(byte[] value)
        {
            return value.IsNotNull() ? new HexString(value) : null;
        }

        /// <summary>
        /// 从 HexString 到 byte[] 的转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator byte[] (HexString value)
        {
            return value.IsNotNull() ? value.ToByteArray() : null;
        }

        /// <summary>
        /// 从 string 到 HexString 的转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator HexString(string value)
        {
            return new HexString(value);
        }

        /// <summary>
        /// 从 HexString 到 string 的转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator string(HexString value)
        {
            return value.IsNull() ? null : value.ToString();
        }

        /// <summary>
        /// 输出 Hex 字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.ToString(true);
        }

        /// <summary>
        /// 输出 Hex 字符串
        /// </summary>
        /// <param name="prefix0x">0x前缀的格式</param>
        /// <returns></returns>
        public string ToString(bool prefix0x)
        {
            string result = BitConverter.ToString(this.ByteArrayValue);

            if (prefix0x)
            {
                result = "0x" + result.Clear("-");
            }

            return result;
        }

        /// <summary>
        /// 输出字节数组
        /// </summary>
        /// <returns></returns>
        public byte[] ToByteArray()
        {
            return this.ByteArrayValue;
        }

        /// <summary>
        /// 输出 Base64 字符串
        /// </summary>
        /// <returns></returns>
        public Base64 ToBase64()
        {
            return this.ByteArrayValue;
        }
    }
}
