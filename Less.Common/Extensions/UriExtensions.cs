//bibaoke.com

using Less.Collection;
using Less.Text;
using System;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace Less
{
    /// <summary>
    /// Uri 的扩展方法
    /// </summary>
    public static class UriExtensions
    {
        private static Regex ExtensionPattern
        {
            get;
            set;
        }

        static UriExtensions()
        {
            UriExtensions.ExtensionPattern = @"\.\w+".ToRegex(RegexOptions.Compiled);
        }

        /// <summary>
        /// 获取 uri 的后缀
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string GetExtension(this Uri uri)
        {
            Match match = UriExtensions.ExtensionPattern.Match(uri.AbsolutePath);

            return match.Value;
        }

        /// <summary>
        /// 设置 uri 的查询参数
        /// 已经存在的查询参数会覆盖 否则会添加
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="query">
        /// 查询参数
        /// 格式 name1=value1&amp;name2=value2 或 ?name1=value1&amp;name2=value2
        /// </param>
        /// <returns>返回一个新的 Uri 实例</returns>
        /// <exception cref="NullReferenceException">query 不能为 null</exception>
        public static Uri SetQuery(this Uri uri, string query)
        {
            NameValueCollection parameters = new NameValueCollection();

            UriExtensions.ParseQuery(uri.Query, parameters);

            UriExtensions.ParseQuery(query, parameters);

            if (parameters.Count > 0)
            {
                return new Uri(uri.SchemaAndHost() + uri.AbsolutePath + "?" + parameters.List("&"));
            }
            else
            {
                return new Uri(uri.SchemaAndHost() + uri.AbsolutePath);
            }
        }

        /// <summary>
        /// 获取 uri 的域名
        /// </summary>
        /// <param name="uri"></param>
        /// <returns>返回 uri 的域名 如果没有域名 返回 null</returns>
        public static string GetDomain(this Uri uri)
        {
            string[] names = uri.Host.Split('.');

            if (names.Length >= 2)
            {
                if (names[names.Length - 2].In("com", "gov", "org", "net", "co"))
                {
                    if (names.Length >= 3)
                    {
                        if (names[names.Length - 2] == names[names.Length - 1])
                        {
                            names = names.SubArray(names.Length - 2);
                        }
                        else
                        {
                            names = names.SubArray(names.Length - 3);
                        }
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

        /// <summary>
        /// 判断目标 uri 是否为本实例的内部链接
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="targetUri"></param>
        /// <returns></returns>
        public static bool IsInternalLink(this Uri uri, Uri targetUri)
        {
            return uri.Host.CompareIgnoreCase(targetUri.Host);
        }

        /// <summary>
        /// 根据指定的相对链接获取新的 Uri 实例
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="relativeUri"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">relativeUri 不能为 null</exception>
        /// <exception cref="UriFormatException">relativeUri 无效</exception>
        public static Uri GetUri(this Uri uri, string relativeUri)
        {
            return new Uri(uri, relativeUri);
        }

        /// <summary>
        /// 获取 uri 的协议和主机部分
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string SchemaAndHost(this Uri uri)
        {
            return uri.Scheme + "://" + uri.Host;
        }

        /// <summary>
        /// 获取移除查询部分的链接
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string RemoveQuery(this Uri uri)
        {
            return uri.SchemaAndHost() + uri.AbsolutePath;
        }

        /// <summary>
        /// 获取 uri 的查询参数名称与值的集合
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static NameValueCollection ParseQuery(this Uri uri)
        {
            NameValueCollection parameters = new NameValueCollection();

            UriExtensions.ParseQuery(uri.Query, parameters);

            return parameters;
        }

        private static void ParseQuery(string query, NameValueCollection parameters)
        {
            string[] conditions = query.Trim().TrimStart('?').Split('&');

            foreach (string i in conditions)
            {
                if (i.Length > 0)
                {
                    string[] array = i.SplitByFirst('=');

                    if (array.Length > 1)
                    {
                        parameters.Add(array[0], array[1]);
                    }
                    else
                    {
                        parameters.Add(array[0], "");
                    }
                }
            }
        }
    }
}
