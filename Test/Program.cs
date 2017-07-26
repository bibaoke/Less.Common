using Less;
using Less.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Net;
using System.Text;
using Less.Text;
using Less.MultiThread;
using Less.Collection;
using System.Threading;

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
            Cmd.Exec("ping bibaoke.com");

            //
            Cmd.Exec("Form.exe");

            //
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.265.com");

            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    byte[] data1 = stream.ToByteArray();

                    string text1 = data1.ToString(Encoding.UTF8);

                    WebClient client = new WebClient();

                    byte[] data2 = client.DownloadData("http://www.265.com");

                    string text2 = data2.ToString(Encoding.UTF8);

                    Assert.IsTrue(data1.Length == data2.Length);
                }
            }

            //
            string testString = "test string";

            string file = Application.SetupDir.CombinePath(Guid.NewGuid());

            file.Write(testString, Encoding.UTF8);

            Assert.IsTrue(file.ReadString(Encoding.UTF8) == testString);

            file.DeleteFile();

            //
            int[] testNumbers = new int[100];

            testNumbers.Length.Each((index) => testNumbers[index] = index);

            Pool.Exec(50, testNumbers, (i) => Console.WriteLine(i));
        }
    }
}
