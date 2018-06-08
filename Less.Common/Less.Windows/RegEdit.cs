//bibaoke.com

using Less.Text;
using Microsoft.Win32;
using System.Diagnostics;

namespace Less.Windows
{
    /// <summary>
    /// 修改注册表
    /// </summary>
    public static class RegEdit
    {
        /// <summary>
        /// 更新 WebBrowser 使用最新的 IE 版本
        /// </summary>
        public static void UpdateWebBrowserVersion()
        {
            int mainVersion = 0;

            using (RegistryKey ie = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer"))
            {
                if (ie.IsNotNull())
                {
                    object svcVersion = ie.GetValue("svcVersion");

                    if (svcVersion.IsNotNull())
                    {
                        mainVersion = svcVersion.ToString().Split('.')[0].ToInt();
                    }
                    else
                    {
                        object version = ie.GetValue("Version");

                        if (version.IsNotNull())
                        {
                            mainVersion = version.ToString().Split('.')[0].ToInt();
                        }
                    }
                }
            }

            if (mainVersion > 0)
            {
                using (RegistryKey wb = Registry.LocalMachine.OpenSubKey(
                    @"SOFTWARE\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION", true))
                {
                    if (wb.IsNotNull())
                    {
                        string name = Process.GetCurrentProcess().ProcessName + ".exe";

                        int oldVersion = (int)wb.GetValue(name);

                        mainVersion = mainVersion * 1000;

                        if (mainVersion > oldVersion)
                        {
                            wb.SetValue(name, mainVersion);
                        }
                    }
                }
            }
        }
    }
}
