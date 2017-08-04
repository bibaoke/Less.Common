//bibaoke.com

using System.IO;
using Less.Collection;
using System;

namespace Less.Windows
{
    /// <summary>
    /// 缓冲器
    /// </summary>
    public class Buffer
    {
        /// <summary>
        /// 初始容量
        /// </summary>
        private int Capacity
        {
            get;
            set;
        }

        /// <summary>
        /// 空间
        /// </summary>
        private byte[] Space
        {
            get;
            set;
        }

        /// <summary>
        /// 当前写入位置
        /// </summary>
        private int Position
        {
            get;
            set;
        }

        /// <summary>
        /// 创建缓冲器
        /// </summary>
        /// <param name="capacity">初始容量</param>
        /// <exception cref="OverflowException">capacity 不能为负数</exception>
        /// <exception cref="OutOfMemoryException">内存不足</exception>
        public Buffer(int capacity)
        {
            this.Capacity = capacity;

            this.Space = new byte[this.Capacity];
        }

        /// <summary>
        /// 缓冲 从源数据流读取数据 缓存之后写入到目标数据流
        /// </summary>
        /// <param name="from">源数据流</param>
        /// <param name="to">目标数据流</param>
        /// <exception cref="NullReferenceException">from 不能为 null 且 to 不能为 null</exception>
        /// <exception cref="OutOfMemoryException">内存不足</exception>
        /// <exception cref="OverflowException">数据过多</exception>
        /// <exception cref="IOException">from 读取错误 或 to 写入错误</exception>
        /// <exception cref="NotSupportedException">from 不支持读取 或 to 不支持写入</exception>
        /// <exception cref="ObjectDisposedException">from 已关闭 或 to 已关闭</exception>
        public void Buff(Stream from, Stream to)
        {
            this.Buff(from, this.Capacity, to, this.Capacity);
        }

        /// <summary>
        /// 缓冲 从源数据流读取数据 缓存之后写入到目标数据流
        /// </summary>
        /// <param name="from">源数据流</param>
        /// <param name="fromOnce">每次读取的字节数</param>
        /// <param name="to">目标数据流</param>
        /// <param name="toOnce">缓存到达或超过此字节数则写入目标流</param>
        /// <exception cref="NullReferenceException">from 不能为 null 且 to 不能为 null</exception>
        /// <exception cref="OutOfMemoryException">内存不足</exception>
        /// <exception cref="OverflowException">数据过多</exception>
        /// <exception cref="ArgumentOutOfRangeException">fromOnce 不能为负数</exception>
        /// <exception cref="IOException">from 读取错误 或 to 写入错误</exception>
        /// <exception cref="NotSupportedException">from 不支持读取 或 to 不支持写入</exception>
        /// <exception cref="ObjectDisposedException">from 已关闭 或 to 已关闭</exception>
        public void Buff(Stream from, int fromOnce, Stream to, int toOnce)
        {
            this.Buff(from, fromOnce, data => to.Write(data), toOnce);
        }

        /// <summary>
        /// 读取流并执行委托
        /// </summary>
        /// <param name="s">要读取的流</param>
        /// <param name="action">要执行的委托</param>
        /// <exception cref="NullReferenceException">s 不能为 null 且 action 不能为 null</exception>
        /// <exception cref="OutOfMemoryException">内存不足</exception>
        /// <exception cref="IOException">读取错误</exception>
        /// <exception cref="NotSupportedException">s 不支持读取</exception>
        /// <exception cref="ObjectDisposedException">s 已关闭</exception>
        public void Buff(Stream s, Action<byte[]> action)
        {
            this.Buff(s, this.Capacity, action, this.Capacity);
        }

        /// <summary>
        /// 读取流并执行委托
        /// </summary>
        /// <param name="s">要读取的流</param>
        /// <param name="fromOnce">每次读取的字节数</param>
        /// <param name="action">要执行的委托</param>
        /// <param name="toOnce">缓存到达或超过此字节数则执行委托</param>
        /// <exception cref="NullReferenceException">s 不能为 null 且 action 不能为 null</exception>
        /// <exception cref="OutOfMemoryException">内存不足</exception>
        /// <exception cref="OverflowException">数据过多</exception>
        /// <exception cref="ArgumentOutOfRangeException">fromOnce 不能为负数</exception>
        /// <exception cref="IOException">读取错误</exception>
        /// <exception cref="NotSupportedException">s 不支持读取</exception>
        /// <exception cref="ObjectDisposedException">s 已关闭</exception>
        public void Buff(Stream s, int fromOnce, Action<byte[]> action, int toOnce)
        {
            while (true)
            {
                //检查存储空间
                this.CheckSpace(fromOnce);

                //从数据流中读取数据
                //返回实际读取字节数
                int read = s.Read(this.Space, this.Position, fromOnce);

                //读取成功
                if (read > 0)
                {
                    //提升位置
                    this.Position += read;

                    //读取数据长度达到设定写入长度
                    if (this.Position >= toOnce)
                    {
                        //执行委托
                        action(this.ToByteArray());

                        //重置缓冲器
                        this.Reset();
                    }
                }
                //读取结束
                else
                {
                    //执行委托
                    action(this.ToByteArray());

                    //重置缓冲器
                    this.Reset();

                    //退出
                    break;
                }
            }
        }

        /// <summary>
        /// 读取流并写入缓存
        /// </summary>
        /// <param name="s">要读取的流</param>
        /// <exception cref="NullReferenceException">s 不能为 null</exception>
        /// <exception cref="OutOfMemoryException">内存不足</exception>
        /// <exception cref="OverflowException">数据过多</exception>
        /// <exception cref="IOException">读取错误</exception>
        /// <exception cref="NotSupportedException">s 不支持读取</exception>
        /// <exception cref="ObjectDisposedException">s 已关闭</exception>
        public void Buff(Stream s)
        {
            this.Buff(s, this.Capacity);
        }

        /// <summary>
        /// 读取流并写入缓存
        /// </summary>
        /// <param name="s">要读取的流</param>
        /// <param name="once">每次读取的字节数</param>
        /// <exception cref="NullReferenceException">s 不能为 null</exception>
        /// <exception cref="OutOfMemoryException">内存不足</exception>
        /// <exception cref="OverflowException">数据过多</exception>
        /// <exception cref="ArgumentOutOfRangeException">once 不能为负数</exception>
        /// <exception cref="IOException">读取错误</exception>
        /// <exception cref="NotSupportedException">s 不支持读取</exception>
        /// <exception cref="ObjectDisposedException">s 已关闭</exception>
        public void Buff(Stream s, int once)
        {
            this.Buff((space, position, fOnce) =>
            {
                return s.Read(space, position, fOnce);
            }, once);
        }

        /// <summary>
        /// 读取数据写入缓存
        /// </summary>
        /// <param name="func">读取数据委托</param>
        /// <param name="once">每次读取的字节数</param>
        /// <exception cref="NullReferenceException">func 不能为 null</exception>
        /// <exception cref="OutOfMemoryException">内存不足</exception>
        /// <exception cref="OverflowException">数据过多</exception>
        public void Buff(Func<byte[], int, int, int> func, int once)
        {
            while (true)
            {
                //检查存储空间
                this.CheckSpace(once);

                //返回实际读取字节数
                int read = func(this.Space, this.Position, once);

                //读取成功 提升位置
                if (read > 0)
                    this.Position += read;
                //读取结束 退出
                else
                    break;
            }
        }

        /// <summary>
        /// 输出字节数组
        /// </summary>
        /// <returns></returns>
        public byte[] ToByteArray()
        {
            return this.Space.SubArray(0, this.Position);
        }

        /// <summary>
        /// 重置缓冲器
        /// </summary>
        private void Reset()
        {
            //重置存储空间
            this.Space = new byte[this.Capacity];

            //重置当前位置
            this.Position = 0;
        }

        /// <summary>
        /// 检查存储空间 空间不足则扩展
        /// </summary>
        /// <param name="write">将要写入的数据长度</param>
        /// <exception cref="OutOfMemoryException">内存不足</exception>
        /// <exception cref="OverflowException">数组超过了最大长度</exception>
        private void CheckSpace(int write)
        {
            //如果空间不足
            if (this.Position + write > this.Space.Length)
            {
                //空间扩展一倍
                this.Space = this.Space.ExtArray(this.Space.Length * 2);

                //递归调用 确保空间足够
                this.CheckSpace(write);
            }
        }
    }
}