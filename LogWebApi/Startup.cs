using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogWebApi.Extensions;
using LogWebApi.Model;
using LogWebApi.Model.AppSettings;
using LogWebApi.Repository;
using MessagePack.Resolvers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using WebApiContrib.Core.Formatter.MessagePack;

namespace LogWebApiTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMvcCore().AddMessagePackFormatters();
            services.AddMvc().AddMessagePackFormatters();

            #region 注入config和数据访问信息
            services.Configure<DBSettings>(Configuration.GetSection("ConnectionStrings"));//数据库连接信息
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));//其他配置信息
            
            services.AddTransient<IRepository<LogEventData>, LogRepository>();//数据访问
            #endregion
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,IOptions<DBSettings> settings)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;//在应用的根 (http://localhost:<port>/) 处提供 Swagger UI
            });

            app.ConfigureExceptionHandler(settings);

            app.UseMvc();
        }
    }
}
