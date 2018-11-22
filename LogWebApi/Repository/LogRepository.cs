using LogWebApi.Context;
using LogWebApi.Model;
using LogWebApi.Model.AppSettings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogWebApi.Repository
{
    public class LogRepository : IRepository<LogEventData>
    {
        private readonly MongoContext _context = null;
        public LogRepository(IOptions<DBSettings> settings)
        {
            _context = new MongoContext(settings);
        }


        public async Task Add(LogEventData item)
        {
            await _context.LogEventDatas.InsertOneAsync(item);
        }
        public async Task<IEnumerable<LogEventData>> GetList(QueryLogModel model)
        {
            var builder = Builders<LogEventData>.Filter;
            FilterDefinition<LogEventData> filter = builder.Empty;
            if (!string.IsNullOrEmpty(model.Level))
            {
                filter = builder.Eq("Level", model.Level);
            }
            if (!string.IsNullOrEmpty(model.LogSource))
            {
                filter = filter & builder.Eq("LogSource", model.LogSource);
            }
            if (!string.IsNullOrEmpty(model.Message))
            {
                filter = filter & builder.Regex("Message", new BsonRegularExpression(new Regex(model.Message)));
            }
            if (DateTime.MinValue != model.StartTime)
            {
                filter = filter & builder.Gte("Date", model.StartTime);
            }
            if(DateTime.MinValue != model.EndTime)
            {
                filter = filter & builder.Lte("Date", model.EndTime);
            }
            return await _context.LogEventDatas.Find(filter)
                 .SortByDescending(log => log.Date)
                 .Skip((model.PageIndex - 1) * model.PageSize)
                 .Limit(model.PageSize).ToListAsync();
        }
        #region 未实现方法
        public async Task<LogEventData> Get(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<LogEventData>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(string id, string body)
        {
            throw new NotImplementedException();
        } 
        #endregion
    }
}
