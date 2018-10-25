//bibaoke.com

using System.Runtime.InteropServices;

namespace Less.Windows
{
    /// <summary>
    /// User32 API
    /// </summary>
    public static class User32
    {
        /// <summary>
        /// Moves the cursor to the specified screen coordinates. If the new coordinates are not within the screen rectangle set by the most recent ClipCursor function call, the system automatically adjusts the coordinates so that the cursor stays within the rectangle
        /// https://docs.microsoft.com/zh-cn/windows/desktop/api/winuser/nf-winuser-setcursorpos
        /// </summary>
        /// <param name="x">The new x-coordinate of the cursor, in screen coordinates</param>
        /// <param name="y">The new y-coordinate of the cursor, in screen coordinates</param>
        /// <returns>
        /// Returns nonzero if successful or zero otherwise
        /// To get extended error information, call GetLastError
        /// </returns>
        [DllImport("user32.dll")]
        public static extern int SetCursorPos(int x, int y);
    }
}
