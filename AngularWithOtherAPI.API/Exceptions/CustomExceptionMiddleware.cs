using AngularWithOtherAPI.API.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AngularWithOtherAPI.API.Exceptions
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";

            var response = new ErrorViewModel()
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = "Internal server error from the custom middleware",
                Path = "path-goes-here"
            };
            return httpContext.Response.WriteAsync(response.ToString());
        }
    }
}
