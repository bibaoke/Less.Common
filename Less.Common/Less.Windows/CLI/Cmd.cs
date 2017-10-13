//bibaoke.com

using Less.Text;
using System.Diagnostics;
using System.IO;
using System;

namespace Less.Windows
{
    /// <summary>
    /// 提供在命令提示符中执行命令的方法
    /// </summary>
    public static class Cmd
    {
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="cmd">命令文本</param>
        public static void Exec(string cmd)
        {
            Cmd.Exec(cmd, c => Console.Write(c));
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="cmd">命令文本</param>
        /// <param name="action">输出处理</param>
        public static void Exec(string cmd, Action<char> action)
        {
            //找出可执行文件和参数的分隔符的位置和长度
            ValueSet<int, int> result = cmd.IndexOfWhiteSpace();

            //创建进程
            using (Process p = new Process())
            {
                //如果没有参数
                if (result.IsNull())
                {
                    p.StartInfo.FileName = cmd;
                }
                //如果有参数
                else
                {
                    p.StartInfo.FileName = cmd.Substring(0, result.Value1);
                    p.StartInfo.Arguments = cmd.Substring(result.Value1 + result.Value2);
                }

                //不使用 shell 启动进程
                p.StartInfo.UseShellExecute = false;
                //重定向输出
                p.StartInfo.RedirectStandardOutput = true;
                //重定向错误
                p.StartInfo.RedirectStandardError = true;

                //启动进程
                p.Start();

                if (action.IsNotNull())
                {
                    //读取输出
                    Cmd.Output(p.StandardOutput, action);

                    //读取错误
                    Cmd.Output(p.StandardError, action);
                }
            }
        }

        private static void Output(StreamReader r, Action<char> action)
        {
            int result = r.Read();

            while (result > -1)
            {
                action((char)result);

                result = r.Read();
            }
        }
    }
}
