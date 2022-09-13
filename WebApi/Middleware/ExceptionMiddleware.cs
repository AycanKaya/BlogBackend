
using Application.Wrappers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Serilog;
using WebApi.Model;

namespace WebApi.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        static readonly ILogger Log = Serilog.Log.ForContext<ExceptionMiddleware>();
        public ExceptionMiddleware(RequestDelegate next)
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
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var now = DateTime.UtcNow;
            
          
           Log.Error(ex.Message);
         
            var message = HttpStatusCode.InternalServerError.ToString();
            // var m = "Please ask your service with this code : "+ex.GetType().GUID ;

            var responseBaseModel = new ResponseBase() { Succeeded = false, 
                                                         Message = message.ToString(), 
                                                         StatusCode = httpContext.Response.StatusCode,
                                                         Error = ex?.Message.ToString()

            };

            ;
            var result = JsonSerializer.Serialize(responseBaseModel);

          //   Log.Error<ResponseBase>(result,responseBaseModel);
            return httpContext.Response.WriteAsync(result);

       /*     var responseModel = new ErrorResultModel() { Succeeded = false, Message = m.ToString(), StatusCode
            = httpContext.Response.StatusCode};
            var result = JsonSerializer.Serialize(responseModel);
            return httpContext.Response.WriteAsync(result);

            */
         /*   return httpContext.Response.WriteAsync(new ErrorResultModel()
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = ex.Message
            }.ToString()); */
        }
    }
}
