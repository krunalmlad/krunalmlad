using AngularWithOtherAPI.API.Data.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AngularWithOtherAPI.API.Exceptions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigurationBuildInExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var logger = loggerFactory.CreateLogger("ConfigurationBuildInExceptionHandler");
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var contextRequest = context.Features.Get<IHttpRequestFeature>();
                    if(contextFeature != null)
                    {
                        var errorVMString = new ErrorViewModel()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message,
                            Path = contextRequest.Path
                        }.ToString();
                        logger.LogInformation(errorVMString);
                        await context.Response.WriteAsync(errorVMString);
                    }
                });
            });
        }

        public static void ConfigurationCustomInExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
