using LogApiHandler.Common;
using System;
using System.Diagnostics;

namespace LogApiHandler
{
    public class Logger
    {
        private readonly static Type declaringType = typeof(Logger);
        /// <summary>
        /// 日志写入实例
        /// </summary>
        private LogWriter _logWriter = null;
        /// <summary>
        /// 日志来源
        /// 默认为调用方法所在类
        /// </summary>
        private string _logSource = string.Empty;
        /// <summary>
        /// 私有构造函数
        /// </summary>
        private Logger()
        {
            _logWriter = LogWriter.GetLogWriter();
        }
        /// <summary>
        /// 私有构造函数
        /// </summary>
        /// <param name="logSource">日志来源</param>
        private Logger(string logSource):this()
        {
            _logSource = logSource;
        }        
        /// <summary>
        /// 获取Logger实例
        /// 默认日志来源为调用方法所在类：namespace.classname
        /// </summary>
        /// <param name="logSource">日志来源</param>
        /// <returns></returns>
        public static Logger GetLogger(string logSource=null)
        {            
            return new Logger(logSource);
        }
        /// <summary>
        /// Trace
        /// </summary>
        /// <param name="message">日志内容</param>
        /// <param name="emails">是否发送邮件,不为空则发送邮件，多个接收人用英文分号;隔开</param>
        public void Trace(string message, string emails = null)
        {
            WriterToTargets(message, LogLevel.Trace, emails);
        }
        /// <summary>
        /// Trace
        /// </summary>
        /// <param name="ex">异常信息</param>
        /// <param name="emails">是否发送邮件,不为空则发送邮件，多个接收人用英文分号;隔开</param>
        public void Trace(Exception ex, string emails = null)
        {
            
            WriterToTargets(ex.ToString(), LogLevel.Trace, emails);
        }
        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="message">日志内容</param>
        /// <param name="emails">是否发送邮件,不为空则发送邮件，多个接收人用英文分号;隔开</param>
        public void Debug(string message, string emails = null)
        {
            WriterToTargets(message, LogLevel.Debug, emails);
        }
        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="ex">异常信息</param>
        /// <param name="emails">是否发送邮件,不为空则发送邮件，多个接收人用英文分号;隔开</param>
        public void Debug(Exception ex, string emails = null)
        {
            WriterToTargets(ex.ToString(), LogLevel.Debug, emails);
        }
        /// <summary>
        /// Info
        /// </summary>
        /// <param name="message">日志内容</param>
        /// <param name="emails">是否发送邮件,不为空则发送邮件，多个接收人用英文分号;隔开</param>
        public void Info(string message, string emails = null)
        {
            WriterToTargets(message, LogLevel.Info, emails);
        }
        /// <summary>
        /// Info
        /// </summary>
        /// <param name="ex">异常信息</param>
        /// <param name="emails">是否发送邮件,不为空则发送邮件，多个接收人用英文分号;隔开</param>
        public void Info(Exception ex, string emails = null)
        {
            WriterToTargets(ex.ToString(), LogLevel.Info, emails);
        }
        /// <summary>
        /// Warn
        /// </summary>
        /// <param name="message">日志内容</param>
        /// <param name="emails">是否发送邮件,不为空则发送邮件，多个接收人用英文分号;隔开</param>
        public void Warn(string message, string emails = null)
        {
            WriterToTargets(message, LogLevel.Warn, emails);
        }
        /// <summary>
        /// Warn
        /// </summary>
        /// <param name="ex">异常信息</param>
        /// <param name="emails">是否发送邮件,不为空则发送邮件，多个接收人用英文分号;隔开</param>
        public void Warn(Exception ex, string emails = null)
        {
            WriterToTargets(ex.ToString(), LogLevel.Warn, emails);
        }
        /// <summary>
        /// Error
        /// </summary>
        /// <param name="message">日志内容</param>
        /// <param name="emails">是否发送邮件,不为空则发送邮件，多个接收人用英文分号;隔开</param>
        public void Error(string message, string emails = null)
        {
            WriterToTargets(message, LogLevel.Error, emails);
        }
        /// <summary>
        /// Error
        /// </summary>
        /// <param name="ex">异常信息</param>
        /// <param name="emails">是否发送邮件,不为空则发送邮件，多个接收人用英文分号;隔开</param>
        public void Error(Exception ex, string emails = null)
        {
            WriterToTargets(ex.ToString(), LogLevel.Error, emails);
        }
        /// <summary>
        /// Fatal
        /// </summary>
        /// <param name="message">日志内容</param>
        /// <param name="emails">是否发送邮件,不为空则发送邮件，多个接收人用英文分号;隔开</param>
        public void Fatal(string message, string emails = null)
        {
            WriterToTargets(message, LogLevel.Fatal, emails);
        }
        /// <summary>
        /// Fatal
        /// </summary>
        /// <param name="ex">异常信息</param>
        /// <param name="emails">是否发送邮件,不为空则发送邮件，多个接收人用英文分号;隔开</param>
        public void Fatal(Exception ex, string emails = null)
        {
            WriterToTargets(ex.ToString(), LogLevel.Fatal, emails);
        }
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="level">级别</param>
        /// <param name="emails">是否发送邮件,不为空则发送邮件，多个接收人用英文分号;隔开</param>
        private void WriterToTargets(string message, LogLevel level,string emails=null)
        {
            try
            {
                LogEventDataAsync leda = new LogEventDataAsync
                {
                    LogSource = _logSource,
                    Level = level.Name,
                    CallerStackBoundaryDeclaringType = GetType(),//获取当前实例
                    CallerStackTrace = new StackTrace(true),//获取当前StackTrace
                    Message = message,
                    Emails = emails
                };

                AsyncHelpers.StartAsyncTask(_logWriter.Writer, leda);//执行异步写日志
            }
            catch
            {
            }            
        }
    }    
}
