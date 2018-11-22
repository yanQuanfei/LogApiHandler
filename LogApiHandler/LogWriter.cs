using LogApiHandler.Common;
using LogApiHandler.Core;
using System;

namespace LogApiHandler
{
    /// <summary>
    /// LogWriter
    /// </summary>
    internal class LogWriter
    {
        /// <summary>
        /// 私有构造函数
        /// </summary>
        private LogWriter() { }
        /// <summary>
        /// 获取LogWriter实例
        /// </summary>
        /// <returns></returns>
        public static LogWriter GetLogWriter()
        {            
            return new LogWriter();
        }        
        
        public void Writer(object logEventDataAsync)
        {
            var led = GetLoggingEventData((LogEventDataAsync)logEventDataAsync);
            var level = LogLevel.FromString(led.Level);
            string logapi = level.LogApi;
            RequestHelpers.DoPost<LogEventData>(logapi, led);//MessagePack进行数据压缩，减小传输数据
        }
        /// <summary>
        /// 获取日志数据
        /// </summary>
        /// <param name="logEventDataAsync"></param>
        /// <returns></returns>
        private LogEventData GetLoggingEventData(LogEventDataAsync logEventDataAsync)
        {
            LocationInfo locationInfo = new LocationInfo(logEventDataAsync.CallerStackBoundaryDeclaringType, logEventDataAsync.CallerStackTrace);
            LogEventData logData = new LogEventData
            {
                Message = logEventDataAsync.Message,
                Date = DateTime.Now,
                Level = logEventDataAsync.Level,
                LogSource = string.IsNullOrEmpty(logEventDataAsync.LogSource) ? locationInfo.ClassName : logEventDataAsync.LogSource,
                ClassName = locationInfo.ClassName,
                MethodName = locationInfo.MethodName,
                LineNumber = locationInfo.LineNumber,
                FileName = locationInfo.FileName,
                IP = "NA",
                Emails = logEventDataAsync.Emails,
                FullInfo=locationInfo.FullInfo
            };
            return logData;
        }
    }
}
