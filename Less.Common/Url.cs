using System;
using System.Linq;
using Less.Text;
using Less.Collection;

namespace Less
{
    /// <summary>
    /// 表示一个 url
    /// </summary>
    public class Url : Uri
    {
        /// <summary>
        /// 创建 url 实例
        /// </summary>
        /// <param name="url"></param>
        public Url(string url) : base(url)
        {
            //
        }

        private Url(Uri baseUri, string relativeUri) : base(baseUri, relativeUri)
        {
            //
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
        /// 根据相对 url 创建新的 url
        /// </summary>
        /// <param name="relativeUrl">相对 url</param>
        /// <returns></returns>
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
    }
}
