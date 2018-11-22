namespace LogWebApi.Model.AppSettings
{
    public class AppSettings
    {
        public SendMailInfo SendMailInfo { get; set; }
    }
    public class SendMailInfo
    {
        public string SMTPServerName { get; set; }
        public string SendEmailAdress { get; set; }
        public string SendEmailPwd { get; set; }
        public string SiteName { get; set; }
        public string SendEmailPort { get; set; }
    }
}
