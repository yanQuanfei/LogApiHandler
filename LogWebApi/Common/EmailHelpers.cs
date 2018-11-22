using LogWebApi.Model;
using LogWebApi.Model.AppSettings;
using LogWebApi.Repository;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LogWebApi.Common
{
    public class EmailHelpers
    {
        private SendMailInfo _mailinfo;
        
        public EmailHelpers(IOptions<AppSettings> appsettings)
        {
            _mailinfo = appsettings.Value.SendMailInfo;
        }
        /// <summary>
        /// 异步发送邮件
        /// </summary>
        /// <param name="emails">email地址</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="content">邮件内容</param>
        public void SendMailAsync(string emails, string subject, string content)
        {
            Task.Factory.StartNew(() =>
            {
                SendEmail(emails, subject, content);
            });
        }
        /// <summary>
        /// 邮件发送方法
        /// </summary>
        /// <param name="emails">email地址</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="content">邮件内容</param>
        /// <returns></returns>
        public void SendEmail(string emails, string subject, string content)
        {
            string[] emailArray = emails.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            string fromSMTP = _mailinfo.SMTPServerName;        //邮件服务器
            string fromEmail = _mailinfo.SendEmailAdress;      //发送方邮件地址
            string fromEmailPwd = _mailinfo.SendEmailPwd;//发送方邮件地址密码
            string fromEmailName = _mailinfo.SiteName;   //发送方称呼
            try
            {
                //新建一个MailMessage对象
                MailMessage aMessage = new MailMessage();
                aMessage.From = new MailAddress(fromEmail, fromEmailName);
                foreach (var item in emailArray)
                {
                    aMessage.To.Add(item);
                }
                aMessage.Subject = subject;
                aMessage.Body = content;
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                aMessage.BodyEncoding = Encoding.GetEncoding("utf-8");
                aMessage.IsBodyHtml = true;
                aMessage.Priority = MailPriority.High;                
                aMessage.ReplyToList.Add(new MailAddress(fromEmail, fromEmailName));
                SmtpClient smtp = new SmtpClient();

                smtp.Host = fromSMTP;
                smtp.Timeout = 20000;
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(fromEmail, fromEmailPwd); //发邮件的EMIAL和密码
                smtp.Port = int.Parse(_mailinfo.SendEmailPort);                
                smtp.Send(aMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
