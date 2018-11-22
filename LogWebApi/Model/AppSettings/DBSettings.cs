using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogWebApi.Model.AppSettings
{
    /// <summary>
    /// 数据库配置信息
    /// </summary>
    public class DBSettings
    {
        /// <summary>
        /// mongodb connectionstring
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// mongodb database
        /// </summary>
        public string Database { get; set; }
        public string LogCollection { get; set; }
    }
}
