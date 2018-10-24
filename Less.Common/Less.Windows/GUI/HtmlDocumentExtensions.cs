//bibaoke.com

using System.Linq;
using System.Windows.Forms;
using Less.Text;

namespace Less.Windows
{
    /// <summary>
    /// HtmlDocument 扩展方法
    /// </summary>
    public static class HtmlDocumentExtensions
    {
        /// <summary>
        /// 获取整个 html 文档
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static string GetHtml(this HtmlDocument document)
        {
            return document.All.Cast<HtmlElement>().Where(i => i.Parent.IsNull()).Select(i => i.OuterHtml).Join();
        }
    }
}
