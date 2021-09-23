using AngularWithOtherAPI.API.Data.Models;
using AngularWithOtherAPI.API.Data.Repositories;
using AngularWithOtherAPI.API.Data.Repositories.IRepositories;
using AngularWithOtherAPI.API.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularWithOtherAPI.API
{
    public class Startup
    {
        public string ConnectionString { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = configuration.GetConnectionString("StudentDBConnectionString");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddCors();
            services.AddDbContext<StudentAppDbContext>(options => options.UseSqlServer(ConnectionString));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<IStandardRepository, StandardRepository>();
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<ILogsRespository, LogsRespository>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AngularWithOtherAPI.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AngularWithOtherAPI.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());

            app.UseAuthorization();
            // Exception handle using middleware
            app.ConfigurationBuildInExceptionHandler(loggerFactory);
            //app.ConfigurationCustomInExceptionHandler();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
