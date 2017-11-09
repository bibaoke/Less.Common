//bibaoke.com

using Less.Text;
using System;

namespace Less.Windows
{
    /// <summary>
    /// 列出菜单
    /// </summary>
    public class Menu : Function
    {
        /// <summary>
        /// 描述
        /// </summary>
        public override string Description
        {
            get
            {
                return "列出菜单";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public override bool Execute(params string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("键入以下命令调用对应的程序：");
            Console.WriteLine();

            foreach (string i in ConsoleApp.Functions.Keys)
            {
                Console.WriteLine("{0}： {1}".FormatString(i, ConsoleApp.Functions[i].Description));
            }

            Console.WriteLine();

            return true;
        }
    }
}
