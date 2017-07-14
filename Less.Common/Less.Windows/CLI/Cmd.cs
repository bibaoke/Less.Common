//bibaoke.com

using Less.Text;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System;

namespace Less.Windows
{
    /// <summary>
    /// 提供在命令提示符中执行命令的方法
    /// </summary>
    public static class Cmd
    {
        /// <summary>
        /// 输出的处理委托
        /// </summary>
        /// <param name="s">
        /// 所执行的命令返回的输出
        /// 需要等命令执行完毕才会返回输出
        /// 并不支持边执行边返回输出
        /// </param>
        public delegate void OutputDel(string s);

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="cmd">命令文本</param>
        public static void Exec(string cmd)
        {
            Cmd.Exec(cmd, s => { });
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="cmd">命令文本</param>
        /// <param name="output">输出处理</param>
        public static void Exec(string cmd, OutputDel output)
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
                //指示进程把输出写入流
                p.StartInfo.RedirectStandardOutput = true;
                //指示进程把错误写入流
                p.StartInfo.RedirectStandardError = true;

                //启动进程
                p.Start();

                //读取输出
                //并执行委托
                Cmd.Output(p.StandardOutput, output);

                //读取错误
                //并执行委托
                Cmd.Output(p.StandardError, output);
            }
        }

        /// <summary>
        /// 输出
        /// </summary>
        /// <param name="r">要输出的流</param>
        /// <param name="del">输出的处理委托</param>
        private static void Output(StreamReader r, OutputDel del)
        {
            //字符缓存
            List<char> buffer = new List<char>();

            //是否读取完毕
            while (!r.EndOfStream)
            {
                //读取一个字符
                char c = (char)r.Read();

                //如果遇到回车或换行
                //则输出一句
                //否则把字符加入缓存
                if (c == Symbol.EnterChar || c == Symbol.NewLineChar)
                {
                    if (buffer.Count > 0)
                    {
                        del(new string(buffer.ToArray()));

                        buffer.Clear();
                    }
                }
                else
                {
                    buffer.Add(c);
                }
            }

            //读取完毕之后
            //如果缓存中还有字符
            //输出缓存中的字符
            if (buffer.Count > 0)
                del(new string(buffer.ToArray()));
        }
    }
}
