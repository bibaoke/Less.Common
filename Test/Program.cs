using Less;
using Less.Collection;
using Less.MultiThread;
using Less.Text;
using Less.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //
            Uri uri = new Uri("https://www.baidu.com/s?ie=utf-8&f=8&rsv_bp=0&rsv_idx=1");

            Assert.IsTrue(uri.SchemaAndHost() == "https://www.baidu.com");

            //
            Kernel32.AddConsoleCtrlHandler(() =>
            {
                //
            });

            //
            RegEdit.UpdateWebBrowserVersion();

            //
            {
                Asyn.Exec(() =>
                {
                    Tcp.Listen("127.0.0.1", 10601, (stream) =>
                    {
                        string request = stream.ReadLine(Encoding.UTF8);

                        Console.WriteLine("request:" + request);

                        stream.Write(request.ToByteArray(Encoding.UTF8));
                    });
                });
            }

            {
                string response = Tcp.SendLine("127.0.0.1", 10601, "test", Encoding.UTF8);

                Console.WriteLine("response:" + response);

                response = Tcp.SendLine("127.0.0.1", 10601, "test", Encoding.UTF8);

                Console.WriteLine("response:" + response);
            }

            //
            Asyn.Exec(() =>
            {
                NamedPipe.Server("testPipe", (stream) =>
                {
                    string request = stream.ReadLine(Encoding.UTF8);

                    Console.WriteLine("request:" + request);

                    stream.WriteLine(request, Encoding.UTF8);
                });
            });

            {
                string response = NamedPipe.SendLine("testPipe", "hello", Encoding.UTF8);

                Console.WriteLine("response:" + response);

                response = NamedPipe.SendLine("testPipe", "hello", Encoding.UTF8);

                Console.WriteLine("response:" + response);
            }

            //
            Cmd.Exec("ping bibaoke.com");

            //
            Cmd.Exec("TestForm1.exe");

            //
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://265.com");

                using (WebResponse response = request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        byte[] data1 = stream.ToByteArray();

                        string text1 = data1.ToString(Encoding.UTF8);

                        WebClient client = new WebClient();

                        byte[] data2 = client.DownloadData("http://265.com");

                        string text2 = data2.ToString(Encoding.UTF8);

                        Assert.IsTrue(data1.Length == data2.Length);
                    }
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
            {
                pool.Execute(i, j => Console.WriteLine(j));
            }

            pool.Execute(() => Thread.Sleep(1000));

            pool.Wait();

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

            //
            Assert.IsTrue(new UriString("http://jianzhimao.com.com").GetDomain() == "com.com");

            Assert.IsTrue(new UriString("http://pconline.com.cn").GetDomain() == "pconline.com.cn");

            //
            Assert.IsTrue(2.Pow(3) == 8);

            //
            Assert.IsTrue("نیک تم | قالب وردپرس, قالب اورجينال فارسي و قالب وردپرس شرکتی".HasUnicode());

            //
            Encoding gbk = MyEncoding.GBK;

            //
            Assert.IsTrue("".SplitByWhiteSpace().Length == 0);

            Assert.IsTrue(" ".SplitByWhiteSpace().Length == 0);

            Assert.IsTrue(" test1  test2 ".SplitByWhiteSpace()[1] == "test2");

            //
            Assert.IsTrue(new int[] { 1, 2 }.ExtArray(new int[0]).Length == 2);

            //
            Assert.IsTrue("test".Repeat(2) == "testtest");

            //
            int[] testArray = new int[100];

            100.Each(index => testArray[index] = index);

            IEnumerable<int> enumerator = testArray.GetEnumerator(50);

            int count = 0;

            enumerator.Each((index, item) =>
            {
                Assert.IsTrue(item == index + 50);

                count++;
            });

            Assert.IsTrue(count == 50);

            //
            Assert.IsTrue("abcd".SubstringUnsafe(1, 2) == "bc");

            Assert.IsTrue("".SubstringUnsafe(0) == "");

            //
            file = Application.SetupDir.CombinePath("testReadBytes.txt");

            Assert.IsTrue(file.ReadString(Encoding.UTF8, true) == "abc");

            file.Write("abcd", Encoding.UTF8);

            Assert.IsTrue(file.ReadString(Encoding.UTF8, true) == "abcd");

            //
            Regex pattern = @"(?<order>order\s+by\s+.*?)\s*$".ToRegex(
                RegexOptions.Compiled |
                RegexOptions.ExplicitCapture |
                RegexOptions.IgnoreCase);

            Assert.IsTrue(
                pattern.Match("select * from user order by dept desc, id asc").GetValue("order") == "order by dept desc, id asc");

            Assert.IsTrue(
                pattern.Match("select * from user order by id ").GetValue("order") == "order by id");

            //
            Assert.IsTrue("90".IsInt());
            Assert.IsTrue("-90".IsInt());
            Assert.IsTrue("0".IsInt());
            Assert.IsTrue("-9".IsInt());
            Assert.IsTrue("-19".IsInt());
            Assert.IsTrue("09".IsInt());
            Assert.IsTrue("9".IsInt());
            Assert.IsFalse("1.9".IsInt());
            Assert.IsTrue(" 19".IsInt());
            Assert.IsTrue("19".IsInt());
            Assert.IsTrue("19 ".IsInt());
            Assert.IsFalse("1九".IsInt());

            //
            Assert.IsTrue("192.168.1.1".IsIpv4());
            Assert.IsFalse("192.168.1".IsIpv4());

            //
            Agent agent = new Agent("ping vaiying.com -t", AgentMode.Restart, TimeSpan.FromSeconds(2));

            agent.StartUp();

            Thread.Sleep(3000);

            agent.ShutDown();

            //
            DateTime startTime = DateTime.Now.AddSeconds(2);

            Agent notepad = new Agent("notepad", AgentMode.Singleton, startTime.Hour, startTime.Minute, startTime.Second);

            notepad.StartUp();

            Thread.Sleep(1000);

            notepad.ShutDown();

            //
            long l = 10;

            Assert.IsTrue(l.ToInt() == 10);

            l = int.MinValue;

            Assert.IsTrue(l.ToInt() == int.MinValue);

            //
            Syn.Wait(() =>
            {
                100.Each((index) =>
                {
                    Console.WriteLine(index);

                    Thread.Sleep(10);
                });

                return true;
            });

            //
            try
            {
                Syn.Wait(TimeSpan.FromSeconds(1), () =>
                {
                    return false;
                });
            }
            catch (TimeoutException ex)
            {
                Console.WriteLine(ex.Message);
            }

            //
            Process.GetCurrentProcess().Kill();
        }
    }
}
