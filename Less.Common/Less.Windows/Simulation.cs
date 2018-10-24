//bibaoke.com

using System.Runtime.InteropServices;

namespace Less.Windows
{
    /// <summary>
    /// 模拟操作
    /// </summary>
    public static class Simulation
    {
        /// <summary>
        /// 设置鼠标位置
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        public static extern int SetCursorPos(int x, int y);
    }
}
