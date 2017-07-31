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
using Less.Text;

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

            //
            int number1 = "10".ToInt();

            string number2String = null;

            int? number2 = number2String.ToInt();

            Assert.IsTrue(number1 == 10);

            Assert.IsTrue(number2 == null);

            //
            byte[] data = "测试字符串".ToByteArray(Encoding.UTF8);

            Base64 base64 = new Base64(data);

            HexString hex = new HexString(data);

            Assert.IsTrue(base64.ToHexString() == hex.ToString());

            Assert.IsTrue(base64 == hex.ToBase64());
        }
    }
}
