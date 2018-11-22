using LogWebApi.Common;
using LogWebApi.Extensions;
using LogWebApi.Model;
using LogWebApi.Model.AppSettings;
using LogWebApi.Repository;
using MessagePack.Resolvers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class LogController : ControllerBase
    {
        private readonly LogRepository _logRepository;
        IOptions<AppSettings> _appsettings;        
        public LogController(IRepository<LogEventData> logRepository,IOptions<AppSettings> appsettings)
        {
            _logRepository = (LogRepository)logRepository;
            _appsettings = appsettings;
        }

        [Route("trace")]
        [HttpPost]
        public void Trace([FromBody] LogEventData value)
        {
            Add(value);
        }
        [Route("debug")]
        [HttpPost]
        public void Debug([FromBody] LogEventData value)
        {
            Add(value);

        }
        [Route("info")]
        [HttpPost]
        public void Info([FromBody] LogEventData value)
        {
            Add(value);
        }
        [Route("warn")]
        [HttpPost]
        public void Warn([FromBody] LogEventData value)
        {
            Add(value);
        }
        [Route("error")]
        [HttpPost]
        public void Error([FromBody] LogEventData value)
        {
            Add(value);
        }
        [Route("fatal")]
        [HttpPost]
        public void Fatal([FromBody] LogEventData value)
        {
            Add(value);
        }
        private async void Add(LogEventData data)
        {
            if (data != null)
            {
                await _logRepository.Add(data);
                if (!string.IsNullOrEmpty(data.Emails))
                {
                    new EmailHelpers(_appsettings).SendMailAsync(data.Emails, "监测邮件", data.ToString());
                }
            }
        }

        [HttpGet("getlist")]
        public async Task<ResponseModel<IEnumerable<LogEventData>>> GetList([FromQuery] QueryLogModel model)
        {
            ResponseModel<IEnumerable<LogEventData>> resp = new ResponseModel<IEnumerable<LogEventData>>();
            resp.Data = await _logRepository.GetList(model);
            return resp;
        }
    }
}