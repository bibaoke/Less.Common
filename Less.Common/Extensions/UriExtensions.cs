//bibaoke.com

using System;

namespace Less
{
    /// <summary>
    /// Uri 的扩展方法
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// 获取 uri 的协议和主机部分
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string SchemaAndHost(this Uri uri)
        {
            return uri.Scheme + "://" + uri.Host;
        }
    }
}
