using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LogApiHandler
{
    /// <summary>
    /// 日志数据，传入异步执行方法的数据
    /// 主要为提前获取CallerStackBoundaryDeclaringType和CallerStackTrace，避免Core（log4net源码）下追踪信息在异步线程内与期望不一致
    /// </summary>
    internal class LogEventDataAsync
    {
        public string Message { get; set; }
        /// <summary>
        /// 错误级别
        /// </summary>
        public string Level { get; set; }
        /// <summary>
        /// 日志来源
        /// </summary>
        public string LogSource { get; set; }
        /// <summary>
        /// 调用日志方法实例类型
        /// </summary>
        public Type CallerStackBoundaryDeclaringType { get; set; }
        /// <summary>
        /// StackTrace
        /// </summary>
        public StackTrace CallerStackTrace { get; set; }
        /// <summary>
        /// 不为空则发送邮件，多个接收人用英文逗号隔开
        /// </summary>
        public string Emails { get; set; }
    }
}
