//bibaoke.com

using Less.Text;
using System.Collections.Generic;

namespace Less
{
    /// <summary>
    /// 动态字符串
    /// </summary>
    public class DynamicString
    {
        /// <summary>
        /// 字符串长度
        /// </summary>
        public int Length
        {
            get;
            private set;
        }

        /// <summary>
        /// 包含数据的列表
        /// </summary>
        protected List<string> List
        {
            get;
            set;
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        public DynamicString()
        {
            this.List = new List<string>();
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="value"></param>
        public DynamicString(string value) : this()
        {
            this.Append(value);
        }

        /// <summary>
        /// 是空白字符串
        /// </summary>
        /// <returns></returns>
        public bool IsWhiteSpace()
        {
            return this.ToString().IsWhiteSpace();
        }

        /// <summary>
        /// 不是空白字符串
        /// </summary>
        /// <returns></returns>
        public bool IsNotWhiteSpace()
        {
            return !this.IsWhiteSpace();
        }

        /// <summary>
        /// 是空字符串
        /// </summary>
        public bool IsEmpty()
        {
            return this.ToString().IsEmpty();
        }

        /// <summary>
        /// 不是空字符串
        /// </summary>
        /// <returns></returns>
        public bool IsNotEmpty()
        {
            return !this.IsEmpty();
        }

        /// <summary>
        /// 输出字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this.List.Count == 1)
            {
                return this.List[0];
            }
            else
            {
                string concat = string.Concat(this.List.ToArray());

                this.List.Clear();

                this.Length = 0;

                this.Append(concat);

                return concat;
            }
        }

        /// <summary>
        /// 从 string 隐式转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator DynamicString(string value)
        {
            return value.IsNotNull() ? new DynamicString(value) : null;
        }

        /// <summary>
        /// 隐式转换成 string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator string(DynamicString value)
        {
            return value.IsNotNull() ? value.ToString() : null;
        }

        /// <summary>
        /// 连接操作
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static DynamicString operator +(DynamicString l, DynamicString r)
        {
            return l.Append(r);
        }

        /// <summary>
        /// 比较两个实例是否相等
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator ==(DynamicString l, DynamicString r)
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
        public static bool operator !=(DynamicString l, DynamicString r)
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
            if (obj is DynamicString)
            {
                return this == (DynamicString)obj;
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
        /// 在指定位置插入字符串 会修改此实例
        /// </summary>
        /// <param name="startIndex">插入索引</param>
        /// <param name="value">要插入的字符串</param>
        /// <returns>返回此实例</returns>
        public DynamicString Insert(int startIndex, string value)
        {
            int i = 0;
            int index = 0;

            this.SplitList(startIndex, ref i, ref index);

            while (i < this.List.Count)
            {
                if (startIndex == index)
                {
                    this.List.Insert(i, value);

                    break;
                }

                index += this.List[i].Length;

                i++;
            }

            return this;
        }

        /// <summary>
        /// 移除指定位置的字符串 会修改此实例
        /// </summary>
        /// <param name="startIndex">起始索引</param>
        /// <returns>返回此实例</returns>
        public DynamicString Remove(int startIndex)
        {
            return this.Remove(startIndex, this.Length - startIndex);
        }

        /// <summary>
        /// 移除指定位置的字符串 会修改此实例
        /// </summary>
        /// <param name="startIndex">起始索引</param>
        /// <param name="length">长度</param>
        /// <returns>返回此实例</returns>
        public DynamicString Remove(int startIndex, int length)
        {
            int i = 0;
            int index = 0;

            this.SplitList(startIndex, length, ref i, ref index);

            int next = 0;

            int stopIndex = startIndex + length;

            while (i < this.List.Count)
            {
                next = index + this.List[i].Length;

                if (index >= startIndex)
                {
                    if (next <= stopIndex)
                    {
                        this.List.RemoveAt(i);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    i++;
                }

                index = next;
            }

            return this;
        }

        /// <summary>
        /// 截取子字符串 不会修改此实例
        /// </summary>
        /// <param name="startIndex">起始索引</param>
        /// <returns></returns>
        public string Substring(int startIndex)
        {
            return this.Substring(startIndex, this.Length - startIndex);
        }

        /// <summary>
        /// 截取子字符串 不会修改此实例
        /// </summary>
        /// <param name="startIndex">起始索引</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public string Substring(int startIndex, int length)
        {
            if (this.List.Count == 1)
            {
                return this.List[0].Substring(startIndex, length);
            }
            else
            {
                DynamicString result = new DynamicString();

                int i = 0;
                int index = 0;

                this.SplitList(startIndex, length, ref i, ref index);

                int next = 0;

                int stopIndex = startIndex + length;

                while (i < this.List.Count)
                {
                    next = index + this.List[i].Length;

                    if (index >= startIndex)
                    {
                        if (next <= stopIndex)
                        {
                            result.Append(this.List[i]);
                        }
                        else
                        {
                            break;
                        }
                    }

                    index = next;

                    i++;
                }

                return result;
            }
        }

        /// <summary>
        /// 换行
        /// </summary>
        /// <returns></returns>
        public DynamicString AppendLine()
        {
            return this.Append(Symbol.NewLine);
        }

        /// <summary>
        /// 拼接值并换行
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DynamicString AppendLine(object value)
        {
            return this.AppendLine(value.ToString());
        }

        /// <summary>
        /// 拼接值并换行
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DynamicString AppendLine(string value)
        {
            return this.Append(value).Append(Symbol.NewLine);
        }

        /// <summary>
        /// 拼接值并换行
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DynamicString AppendLine(DynamicString value)
        {
            return this.Append(value).Append(Symbol.NewLine);
        }

        /// <summary>
        /// 拼接值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DynamicString Append(object value)
        {
            return this.Append(value.ToString());
        }

        /// <summary>
        /// 拼接值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DynamicString Append(string value)
        {
            this.List.Add(value);

            this.Length += value.Length;

            return this;
        }

        /// <summary>
        /// 拼接值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DynamicString Append(DynamicString value)
        {
            this.List.AddRange(value.List);

            this.Length += value.Length;

            return this;
        }

        private void SplitList(int startIndex, int length, ref int i, ref int index)
        {
            this.SplitList(startIndex, ref i, ref index);

            int iCopy = i;
            int indexCopy = index;

            this.SplitList(startIndex + length, ref iCopy, ref indexCopy);
        }

        private void SplitList(int startIndex, ref int i, ref int index)
        {
            int next = 0;

            while (i < this.List.Count)
            {
                next = index + this.List[i].Length;

                if (startIndex == index || startIndex == next)
                {
                    break;
                }

                if (startIndex > index && startIndex < next)
                {
                    string origin = this.List[i];

                    int split = startIndex - index;

                    this.List[i] = origin.Substring(split);

                    this.List.Insert(i, origin.Substring(0, split));

                    break;
                }

                index = next;

                i++;
            }
        }
    }
}
