//bibaoke.com

using System.Drawing;
using System.Windows.Forms;

namespace Less.Windows
{
    /// <summary>
    /// HtmlElement 的扩展方法
    /// </summary>
    public static class HtmlElementExtensions
    {
        /// <summary>
        /// 获取元素相对于屏幕的中心位置
        /// </summary>
        /// <param name="e"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        public static Point GetScreenCenter(this HtmlElement e, Form form)
        {
            return form.PointToScreen(e.GetOffsetCenter());
        }

        /// <summary>
        /// 获取元素相对于屏幕的位置
        /// </summary>
        /// <param name="e"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        public static Point GetScreen(this HtmlElement e, Form form)
        {
            return form.PointToScreen(e.GetOffset());
        }

        /// <summary>
        /// 获取元素相对于文档的中心位置
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static Point GetOffsetCenter(this HtmlElement e)
        {
            Point p = e.GetOffset();

            p.X += e.OffsetRectangle.Width / 2;
            p.Y += e.OffsetRectangle.Height / 2;

            return p;
        }

        /// <summary>
        /// 获取元素相对于文档的位置
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static Point GetOffset(this HtmlElement e)
        {
            //get element pos
            Point pos = new Point(e.OffsetRectangle.Left, e.OffsetRectangle.Top);

            //get the parents pos
            HtmlElement tempEl = e.OffsetParent;

            while (tempEl != null)
            {
                pos.X += tempEl.OffsetRectangle.Left;
                pos.Y += tempEl.OffsetRectangle.Top;

                tempEl = tempEl.OffsetParent;
            }

            return pos;
        }
    }
}
