//bibaoke.com

using Less.Text;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Less
{
    /// <summary>
    /// 表示十六进制字符串
    /// </summary>
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
        /// 创建实例
        /// </summary>
        /// <param name="stringValue">Hex 字符串</param>
        /// <exception cref="ArgumentNullException">Hex 字符串不能为空引用</exception>
        /// <exception cref="FormatException">Hex 字符串格式不正确</exception>
        public HexString(string stringValue)
        {
            if (stringValue.IsNull())
                throw new ArgumentNullException();

            stringValue = stringValue.TrimStart("0x").Clear("-");

            List<byte> list = new List<byte>(stringValue.Length / 2);

            for (int i = 0; i < stringValue.Length; i = i + 2)
                list.Add(byte.Parse(stringValue.Substring(i, 2), NumberStyles.HexNumber));

            this.ByteArrayValue = list.ToArray();
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="byteArrayValue">字节序列</param>
        /// <exception cref="ArgumentNullException">字节序列不能为空引用</exception>
        public HexString(byte[] byteArrayValue)
        {
            if (byteArrayValue.IsNull())
                throw new ArgumentNullException();

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
                if (l.ByteArrayValue.Length == r.ByteArrayValue.Length)
                {
                    for (int i = 0; i < l.ByteArrayValue.Length; i++)
                    {
                        if (l.ByteArrayValue[i] != r.ByteArrayValue[i])
                            return false;
                    }

                    return true;
                }
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
        /// 重载
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is HexString)
                return this == (HexString)obj;

            return false;
        }

        /// <summary>
        /// 重载
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int hash = 0;

            for (int i = 0; i < 4; i++)
            {
                if (this.ByteArrayValue.Length > i)
                    hash |= this.ByteArrayValue[i] << 8 * i;
                else
                    hash |= 0 << 8 * i;
            }

            return hash;
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
        public static implicit operator byte[](HexString value)
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
        /// 输出十六进制字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.ToString(true);
        }

        /// <summary>
        /// 输出十六进制字符串
        /// </summary>
        /// <param name="prefix0x">0x前缀的格式</param>
        /// <returns></returns>
        public string ToString(bool prefix0x)
        {
            string result = BitConverter.ToString(this.ByteArrayValue);

            if (prefix0x)
                result = "0x".Combine(result.Clear("-"));

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
        /// 输出 Base-64 字符串
        /// </summary>
        /// <returns></returns>
        public Base64 ToBase64()
        {
            return this.ByteArrayValue;
        }
    }
}
