using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogWebApi.Model
{
    public class QueryLogModel
    {
        private int _pageindex = 1;
        private int _pagesize = 20;
        public int PageIndex
        {
            get { return _pageindex; }
            set { _pageindex = value; }
        }
        public int PageSize
        {
            get { return _pagesize; }
            set { _pagesize = value; }
        }
        public string Level { get; set; }
        public string LogSource { get; set; }
        public string Message { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
