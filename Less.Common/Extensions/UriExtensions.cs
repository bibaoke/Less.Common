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
        /// 判断目标 Uri 是否为本实例的内部链接
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="targetUri"></param>
        /// <returns></returns>
        public static bool IsInternalLink(this Uri uri, Uri targetUri)
        {
            return uri.Host == targetUri.Host;
        }

        /// <summary>
        /// 根据指定的相对链接获取新的 Uri 实例
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="relativeUri"></param>
        /// <returns></returns>
        public static Uri GetUri(this Uri uri, string relativeUri)
        {
            return new Uri(uri, relativeUri);
        }

        /// <summary>
        /// 获取 Uri 的协议和主机部分
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string SchemaAndHost(this Uri uri)
        {
            return uri.Scheme + "://" + uri.Host;
        }
    }
}
