//bibaoke.com

using System;
using System.Linq;
using Less.Text;
using Less.Collection;
using System.Collections.Specialized;

namespace Less
{
    /// <summary>
    /// 表示 uri 字符串
    /// </summary>
    public class UriString : Uri
    {
        /// <summary>
        /// uri 的查询参数集合
        /// </summary>
        private NameValueCollection Parameters
        {
            get;
            set;
        }

        /// <summary>
        /// 创建 UriString 实例
        /// </summary>
        /// <param name="uri"></param>
        public UriString(Uri uri) : this(uri.OriginalString)
        {
            //
        }

        /// <summary>
        /// 创建 UriString 实例
        /// </summary>
        /// <param name="uri"></param>
        public UriString(string uri) : base(uri)
        {
            this.Init();
        }

        /// <summary>
        /// 创建 UriString 实例
        /// </summary>
        /// <param name="baseUri"></param>
        /// <param name="relativeUri"></param>
        public UriString(Uri baseUri, string relativeUri) : base(baseUri, relativeUri)
        {
            this.Init();
        }

        /// <summary>
        /// 从 string 到 UriString 的隐式转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator UriString(string value)
        {
            return value.IsNotNull() ? new UriString(value) : null;
        }

        /// <summary>
        /// 从 UriString 到 string 的隐式转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator string(UriString value)
        {
            return value.IsNotNull() ? value.ToString() : null;
        }

        /// <summary>
        /// 设置 uri 的查询参数
        /// 已经存在的查询参数会覆盖 否则会添加
        /// </summary>
        /// <param name="query">
        /// 查询参数
        /// 格式 name1=value1&amp;name2=value2 或 ?name1=value1&amp;name2=value2
        /// </param>
        /// <returns>返回一个新的 UriString 实例</returns>
        public UriString SetQuery(string query)
        {
            if (query.IsNotNull())
            {
                this.ParseQuery(query);
            }

            query = this.Parameters.List("&");

            if (query.IsNotEmpty())
            {
                return new UriString(
                    this.Scheme + "://" + this.Host + this.AbsolutePath + "?" + this.Parameters.List("&"));
            }
            else
            {
                return new UriString(this.Scheme + "://" + this.Host + this.AbsolutePath);
            }
        }

        /// <summary>
        /// 根据相对 uri 创建新的 uri
        /// </summary>
        /// <param name="relativeUri">相对 uri</param>
        /// <returns>返回一个新的 UriString 实例</returns>
        /// <exception cref="UriFormatException">uri 格式无效</exception>
        public UriString GetUriString(string relativeUri)
        {
            return new UriString(this, relativeUri);
        }

        /// <summary>
        /// 判断 Host 是否 ip 地址
        /// </summary>
        /// <returns></returns>
        public bool IsIpHost()
        {
            string[] numbers = this.Host.Split('.');

            if (numbers.Length == 4)
            {
                foreach (string i in numbers)
                {
                    byte value;

                    if (!byte.TryParse(i, out value))
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取 uri 的域名
        /// </summary>
        /// <returns>返回 uri 的域名 如果没有域名 返回 null</returns>
        public string GetDomain()
        {
            string[] names = this.Host.Split('.');

            if (names.Length >= 2)
            {
                if (names[names.Length - 2].In("com", "gov", "org", "net", "co"))
                {
                    if (names.Length >= 3)
                    {
                        names = names.SubArray(names.Length - 3);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    names = names.SubArray(names.Length - 2);
                }
            }
            else
            {
                return null;
            }

            return names.Join(".");
        }

        private void ParseQuery(string query)
        {
            string[] conditions = query.Trim().TrimStart('?').Split('&');

            foreach (string i in conditions)
            {
                if (i.Length > 0)
                {
                    string[] array = i.SplitByFirst('=');

                    if (array.Length > 1)
                    {
                        this.Parameters.Add(array[0], array[1]);
                    }
                    else
                    {
                        this.Parameters.Add(array[0], "");
                    }
                }
            }
        }

        private void Init()
        {
            this.Parameters = new NameValueCollection();

            this.ParseQuery(this.Query);
        }
    }
}
