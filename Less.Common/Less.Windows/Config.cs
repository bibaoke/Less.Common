//bibaoke.com

using System.Configuration;
using System.Net;
using Less.Text;
using System.Linq;
using System.Collections.Generic;
using Less.Collection;

namespace Less.Windows
{
    /// <summary>
    /// 提供获取配置的方法
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// 获取程序配置
        /// </summary>
        /// <param name="key">配置名</param>
        /// <returns></returns>
        public static string GetAppSetting(string key)
        {
            string key2 = key.Combine(".", Dns.GetHostName());

            return ConfigurationManager.AppSettings[key2].IsNull(ConfigurationManager.AppSettings[key]);
        }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetConnectionStrings()
        {
            return Config.GetConnectionStringSettings().Select(i => i.ConnectionString);
        }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <param name="name">连接字符串名</param>
        /// <returns></returns>
        public static string GetConnectionString(string name)
        {
            return Config.GetConnectionStringSetting(name).ConnectionString;
        }

        /// <summary>
        /// 获取连接字符串对象
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ConnectionStringSettings> GetConnectionStringSettings()
        {
            string host = Dns.GetHostName();

            IEnumerable<ConnectionStringSettings> settings = ConfigurationManager.ConnectionStrings.Cast<ConnectionStringSettings>();

            if (settings.IsNotEmpty())
                return settings.Where(i => i.Name.EndsWith(".".Combine(host)));
            else
                return settings;
        }

        /// <summary>
        /// 获取连接字符串对象
        /// </summary>
        /// <param name="name">连接字符串名</param>
        /// <returns></returns>
        public static ConnectionStringSettings GetConnectionStringSetting(string name)
        {
            string name2 = name.Combine(".", Dns.GetHostName());

            return ConfigurationManager.ConnectionStrings[name2].IsNull(ConfigurationManager.ConnectionStrings[name]);
        }
    }
}
