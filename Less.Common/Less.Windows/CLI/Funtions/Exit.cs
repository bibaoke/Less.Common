//bibaoke.com

using System.Diagnostics;

namespace Less.Windows
{
    /// <summary>
    /// 退出程序
    /// </summary>
    public class Exit : Function
    {
        /// <summary>
        /// 描述
        /// </summary>
        public override string Description
        {
            get
            {
                return "退出";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public override bool Execute(params string[] args)
        {
            Process.GetCurrentProcess().Kill();

            return false;
        }
    }
}
