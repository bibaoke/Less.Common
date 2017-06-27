using Less;
using Less.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            DynamicString a = "a";

            Assert.IsTrue(a.Append("b").ToString() == "ab");
        }
    }
}
