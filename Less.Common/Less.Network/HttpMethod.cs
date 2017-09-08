//bibaoke.com

using Less.Text;

namespace Less.Network
{
    /// <summary>
    /// http 请求方式
    /// </summary>
    public class HttpMethod : StringEnum
    {
        /// <summary>
        /// GET
        /// </summary>
        public static HttpMethod Get
        {
            get;
            private set;
        }

        /// <summary>
        /// POST
        /// </summary>
        public static HttpMethod Post
        {
            get;
            private set;
        }

        static HttpMethod()
        {
            HttpMethod.Get = "GET";
            HttpMethod.Post = "POST";
        }

        private HttpMethod(string value) : base(value)
        {
            //
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator ==(HttpMethod l, HttpMethod r)
        {
            return l.Value.CompareTrimAndIgnoreCase(r.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator !=(HttpMethod l, HttpMethod r)
        {
            return !(l == r);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is HttpMethod)
            {
                return this == (HttpMethod)obj;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 从 string 到 HttpMethod 的隐式转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator HttpMethod(string value)
        {
            return new HttpMethod(value);
        }

        /// <summary>
        /// 从 HttpMethod 到 string 的隐式转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator string(HttpMethod value)
        {
            return value.ToString();
        }
    }
}
