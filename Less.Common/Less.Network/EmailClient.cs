//bibaoke.com

using System.Net.Mail;
using System.Net;
using Less.Encrypt;

namespace Less.Network
{
    /// <summary>
    /// 电子邮件客户端
    /// </summary>
    public class EmailClient
    {
        private string UserName
        {
            get;
            set;
        }

        private SmtpClient SmtpClient
        {
            get;
            set;
        }

        /// <summary>
        /// 创建客户端
        /// </summary>
        /// <param name="host">主机</param>
        /// <param name="userName">登录名</param>
        /// <param name="password">密码</param>
        public EmailClient(string host, string userName, string password)
        {
            this.SmtpClient = this.GetSmtpClient(host, userName, password);

            this.UserName = userName;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="to">收件人</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件正文</param>
        public void Send(string to, string subject, string body)
        {
            this.Send(to, null, subject, body);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="to">收件人</param>
        /// <param name="toName">收件人显示名</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件正文</param>
        public void Send(string to, string toName, string subject, string body)
        {
            this.Send(null, to, toName, subject, body);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="fromName">发件人显示名</param>
        /// <param name="to">收件人</param>
        /// <param name="toName">收件人显示名</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件正文</param>
        public void Send(string fromName, string to, string toName, string subject, string body)
        {
            this.Send(this.UserName, fromName, to, toName, subject, body);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="from">发件人</param>
        /// <param name="fromName">发件人显示名</param>
        /// <param name="to">收件人</param>
        /// <param name="toName">收件人显示名</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件正文</param>
        public void Send(string from, string fromName, string to, string toName, string subject, string body)
        {
            MailMessage msg = this.GetMailMessage(from, fromName, to, toName, subject, body);

            this.SmtpClient.Send(msg);
        }

        private MailMessage GetMailMessage(string from, string fromName, string to, string toName, string subject, string body)
        {
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress(from, fromName);

            msg.To.Add(new MailAddress(to, toName));

            msg.Subject = subject;

            msg.Body = body;

            msg.IsBodyHtml = true;

            return msg;
        }

        private SmtpClient GetSmtpClient(string host, string userName, string password)
        {
            SmtpClient client = new SmtpClient(host);

            client.Credentials = new NetworkCredential(userName, password);

            return client;
        }
    }
}
