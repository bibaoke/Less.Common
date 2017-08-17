//bibaoke.com

using System;
using System.Linq;
using Less.Text;
using Less.Collection;
using System.Collections.Specialized;

namespace Less
{
    /// <summary>
    /// 表示一个 url
    /// </summary>
    public class Url : Uri
    {
        /// <summary>
        /// url 的查询参数集合
        /// </summary>
        private NameValueCollection Parameters
        {
            get;
            set;
        }

        /// <summary>
        /// 创建 url 实例
        /// </summary>
        /// <param name="url"></param>
        public Url(string url) : base(url)
        {
            this.Parameters = new NameValueCollection();

            this.ParseQuery(this.Query);
        }

        private Url(Uri baseUri, string relativeUri) : base(baseUri, relativeUri)
        {
            this.Parameters = new NameValueCollection();

            this.ParseQuery(this.Query);
        }

        /// <summary>
        /// 从 string 到 Url 的隐式转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator Url(string value)
        {
            return value.IsNotNull() ? new Url(value) : null;
        }

        /// <summary>
        /// 从 Url 到 string 的隐式转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator string(Url value)
        {
            return value.IsNotNull() ? value.ToString() : null;
        }

        /// <summary>
        /// 设置 url 的查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns>返回一个新的 url 实例</returns>
        public Url SetQuery(string query)
        {
            this.ParseQuery(query);

            return new Url(
                this.Scheme.Combine("://", this.Host).CombineUrlPath(this.AbsolutePath).Combine("?").CombineUrlQuery(this.Parameters.List("&")));
        }

        /// <summary>
        /// 根据相对 url 创建新的 url
        /// </summary>
        /// <param name="relativeUrl">相对 url</param>
        /// <returns>返回一个新的 url 实例</returns>
        /// <exception cref="UriFormatException">url 格式无效</exception>
        public Url GetUrl(string relativeUrl)
        {
            return new Url(this, relativeUrl);
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
                        return false;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取 url 的域名
        /// </summary>
        /// <returns>返回 url 的域名 如果没有域名 返回 null</returns>
        public string GetDomain()
        {
            string[] names = this.Host.Split('.');

            if (names.Length >= 2)
            {
                if (names[names.Length - 2].In("com", "gov", "org", "net", "co"))
                {
                    if (names.Length >= 3)
                        names = names.SubArray(names.Length - 3);
                    else
                        return null;
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
            string[] conditions = query.TrimStart('?').Split('&');

            foreach (string i in conditions)
            {
                if (i.Length > 0)
                {
                    int index = i.IndexOf('=');

                    if (index > 0)
                    {
                        string name = i.Substring(0, index);

                        string value = "";

                        if (index < i.Length - 1)
                            value = i.Substring(index + 1);

                        this.Parameters.Add(name, value);
                    }
                }
            }
        }
    }
}
