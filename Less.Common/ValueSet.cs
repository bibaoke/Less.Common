//bibaoke.com

namespace Less
{
    /// <summary>
    /// 值的集合
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="T5"></typeparam>
    /// <typeparam name="T6"></typeparam>
    /// <typeparam name="T7"></typeparam>
    /// <typeparam name="T8"></typeparam>
    public class ValueSet<T1, T2, T3, T4, T5, T6, T7, T8> : ValueSet<T1, T2, T3, T4, T5, T6, T7>
    {
        /// <summary>
        /// 
        /// </summary>
        public T8 Value8
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="value3"></param>
        /// <param name="value4"></param>
        /// <param name="value5"></param>
        /// <param name="value6"></param>
        /// <param name="value7"></param>
        /// <param name="value8"></param>
        public ValueSet(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8) : base(value1, value2, value3, value4, value5, value6, value7)
        {
            this.Value8 = value8;
        }
    }

    /// <summary>
    /// 值的集合
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="T5"></typeparam>
    /// <typeparam name="T6"></typeparam>
    /// <typeparam name="T7"></typeparam>
    public class ValueSet<T1, T2, T3, T4, T5, T6, T7> : ValueSet<T1, T2, T3, T4, T5, T6>
    {
        /// <summary>
        /// 
        /// </summary>
        public T7 Value7
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="value3"></param>
        /// <param name="value4"></param>
        /// <param name="value5"></param>
        /// <param name="value6"></param>
        /// <param name="value7"></param>
        public ValueSet(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7) : base(value1, value2, value3, value4, value5, value6)
        {
            this.Value7 = value7;
        }
    }

    /// <summary>
    /// 值的集合
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="T5"></typeparam>
    /// <typeparam name="T6"></typeparam>
    public class ValueSet<T1, T2, T3, T4, T5, T6> : ValueSet<T1, T2, T3, T4, T5>
    {
        /// <summary>
        /// 
        /// </summary>
        public T6 Value6
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="value3"></param>
        /// <param name="value4"></param>
        /// <param name="value5"></param>
        /// <param name="value6"></param>
        public ValueSet(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6) : base(value1, value2, value3, value4, value5)
        {
            this.Value6 = value6;
        }
    }

    /// <summary>
    /// 值的集合
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="T5"></typeparam>
    public class ValueSet<T1, T2, T3, T4, T5> : ValueSet<T1, T2, T3, T4>
    {
        /// <summary>
        /// 
        /// </summary>
        public T5 Value5
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="value3"></param>
        /// <param name="value4"></param>
        /// <param name="value5"></param>
        public ValueSet(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5) : base(value1, value2, value3, value4)
        {
            this.Value5 = value5;
        }
    }

    /// <summary>
    /// 值的集合
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    public class ValueSet<T1, T2, T3, T4> : ValueSet<T1, T2, T3>
    {
        /// <summary>
        /// 
        /// </summary>
        public T4 Value4
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="value3"></param>
        /// <param name="value4"></param>
        public ValueSet(T1 value1, T2 value2, T3 value3, T4 value4) : base(value1, value2, value3)
        {
            this.Value4 = value4;
        }
    }

    /// <summary>
    /// 值的集合
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    public class ValueSet<T1, T2, T3> : ValueSet<T1, T2>
    {
        /// <summary>
        /// 
        /// </summary>
        public T3 Value3
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="value3"></param>
        public ValueSet(T1 value1, T2 value2, T3 value3) : base(value1, value2)
        {
            this.Value3 = value3;
        }
    }

    /// <summary>
    /// 值的集合
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public class ValueSet<T1, T2>
    {
        /// <summary>
        /// 
        /// </summary>
        public T1 Value1
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public T2 Value2
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        public ValueSet(T1 val1, T2 val2)
        {
            this.Value1 = val1;
            this.Value2 = val2;
        }
    }
}
