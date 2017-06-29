using System;
using System.Windows.Forms;

namespace Less.Windows
{
    /// <summary>
    /// Control 扩展方法
    /// </summary>
    public static class ControlExtensions
    {
        /// <summary>
        /// 使用控件的线程执行
        /// </summary>
        /// <param name="c"></param>
        /// <param name="action"></param>
        public static void Invoke(this Control c, Action action)
        {
            c.Invoke(action);
        }
    }
}
