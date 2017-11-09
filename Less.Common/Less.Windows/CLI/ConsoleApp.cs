//bibaoke.com

using System;
using System.Collections.Generic;
using Less.Text;
using Less.Collection;

namespace Less.Windows
{
    /// <summary>
    /// 控制台应用
    /// </summary>
    public static class ConsoleApp
    {
        /// <summary>
        /// 功能集合
        /// </summary>
        internal static Dictionary<string, Function> Functions
        {
            get;
            private set;
        }

        static ConsoleApp()
        {
            ConsoleApp.Functions = new Dictionary<string, Function>(StringComparer.OrdinalIgnoreCase);

            ConsoleApp.AddFuntion(new Menu());
            ConsoleApp.AddFuntion(new Exit());
        }

        /// <summary>
        /// 启动并执行命令
        /// </summary>
        /// <param name="function">要执行的命令</param>
        /// <param name="args">命令参数</param>
        public static void Start(string function, params string[] args)
        {
            ConsoleApp.Functions[function].Execute(args);
        }

        /// <summary>
        /// 启动
        /// </summary>
        public static void Start()
        {
            ConsoleApp.Functions["menu"].Execute(null);

            while (true)
            {
                string command = Console.ReadLine();

                string[] array = command.SplitByWhiteSpace();

                if (array.Length > 0)
                {
                    Function function;

                    if (ConsoleApp.Functions.TryGetValue(array[0], out function))
                    {
                        if (!function.Execute(array.SubArray(1)))
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("输入错误");
                    }
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// 添加功能
        /// </summary>
        /// <param name="function"></param>
        public static void AddFuntion(Function function)
        {
            ConsoleApp.Functions.Add(function.Name, function);
        }
    }
}
