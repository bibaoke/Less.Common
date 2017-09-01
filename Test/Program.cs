using Less;
using Less.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Net;
using System.Text;
using Less.Text;
using Less.MultiThread;
using System.Diagnostics;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
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
            Pool pool = new Pool(50);

            for (int i = 0; i < 100; i++)
                pool.Execute(i, j => Console.WriteLine(j));

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

            //
            Assert.IsTrue(new UriString("http://bibaoke.com/post/74").GetUriString("71") == "http://bibaoke.com/post/71");

            Assert.IsTrue(new UriString("http://bibaoke.com/post/74").GetUriString("/post/71") == "http://bibaoke.com/post/71");

            Assert.IsTrue(new UriString("http://bibaoke.com").GetUriString("post/71") == "http://bibaoke.com/post/71");

            //
            Assert.IsTrue(new UriString("http://bibaoke.com/icon").SetQuery("char=和") == "http://bibaoke.com/icon?char=和");

            Assert.IsTrue(
                new UriString("http://bibaoke.com/icon?char=和").SetQuery("color=white") == "http://bibaoke.com/icon?char=和&color=white");

            Assert.IsTrue(2.Pow(3) == 8);

            //
            Assert.IsTrue("نیک تم | قالب وردپرس, قالب اورجينال فارسي و قالب وردپرس شرکتی".HasUnicode());

            //
            Encoding gbk = MyEncoding.GBK;
        }
    }
}
