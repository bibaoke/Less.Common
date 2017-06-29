using Less;
using Less.Text;
using Less.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //
            DynamicString a = "a";

            Assert.IsTrue(a.Append("b").ToString() == "ab");

            //
            Cmd.Exec("ping bibaoke.com", (s) =>
            {
                Console.WriteLine(s);
            });

            //
            Cmd.Exec("Form.exe");
        }
    }
}
