//bibaoke.com

namespace Less
{
    /// <summary>
    /// 值类型的一个引用包装
    /// 用来隐式转换到值类型或可空的值类型
    /// </summary>
    public class Class<T> where T : struct
    {
        /// <summary>
        /// 值
        /// </summary>
        private T Value
        {
            get;
            set;
        }

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private Class(T t)
        {
            this.Value = t;
        }

        /// <summary>
        /// 从 Class&lt;T&gt; 到 T? 的隐式转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator T? (Class<T> value)
        {
            return value.IsNotNull() ? value.Value : default(T?);
        }

        /// <summary>
        /// 从 T? 到 Class&lt;T&gt; 的隐式转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator Class<T>(T? value)
        {
            return value.IsNotNull() ? new Class<T>(value.Value) : null;
        }

        /// <summary>
        /// 从 Class&lt;T&gt; 到 T 的隐式转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator T(Class<T> value)
        {
            return value.IsNotNull() ? value.Value : default(T);
        }

        /// <summary>
        /// 从 T 到 Class&lt;T&gt; 的隐式转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator Class<T>(T value)
        {
            return new Class<T>(value);
        }
    }
}
