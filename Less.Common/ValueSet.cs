//bibaoke.com

namespace Less
{
    public class ValueSet<T1, T2, T3, T4, T5, T6, T7, T8> : ValueSet<T1, T2, T3, T4, T5, T6, T7>
    {
        public T8 Value8
        {
            get;
            set;
        }

        public ValueSet(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8) : base(value1, value2, value3, value4, value5, value6, value7)
        {
            this.Value8 = value8;
        }
    }

    public class ValueSet<T1, T2, T3, T4, T5, T6, T7> : ValueSet<T1, T2, T3, T4, T5, T6>
    {
        public T7 Value7
        {
            get;
            set;
        }

        public ValueSet(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7) : base(value1, value2, value3, value4, value5, value6)
        {
            this.Value7 = value7;
        }
    }

    public class ValueSet<T1, T2, T3, T4, T5, T6> : ValueSet<T1, T2, T3, T4, T5>
    {
        public T6 Value6
        {
            get;
            set;
        }

        public ValueSet(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6) : base(value1, value2, value3, value4, value5)
        {
            this.Value6 = value6;
        }
    }

    public class ValueSet<T1, T2, T3, T4, T5> : ValueSet<T1, T2, T3, T4>
    {
        public T5 Value5
        {
            get;
            set;
        }

        public ValueSet(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5) : base(value1, value2, value3, value4)
        {
            this.Value5 = value5;
        }
    }

    public class ValueSet<T1, T2, T3, T4> : ValueSet<T1, T2, T3>
    {
        public T4 Value4
        {
            get;
            set;
        }

        public ValueSet(T1 value1, T2 value2, T3 value3, T4 value4) : base(value1, value2, value3)
        {
            this.Value4 = value4;
        }
    }

    public class ValueSet<T1, T2, T3> : ValueSet<T1, T2>
    {
        public T3 Value3
        {
            get;
            set;
        }

        public ValueSet(T1 value1, T2 value2, T3 value3) : base(value1, value2)
        {
            this.Value3 = value3;
        }
    }

    public class ValueSet<T1, T2>
    {
        public T1 Value1
        {
            get;
            set;
        }

        public T2 Value2
        {
            get;
            set;
        }

        public ValueSet(T1 val1, T2 val2)
        {
            this.Value1 = val1;
            this.Value2 = val2;
        }
    }
}
