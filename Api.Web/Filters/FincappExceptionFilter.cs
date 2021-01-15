using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace Api.Web.Filters
{
    public class FincappExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            ErrorMessage errorMessage;

            if (context.Exception is HttpResponseException)
            {
                context.Response = ((HttpResponseException)context.Exception).Response;
                errorMessage = new ErrorMessage
                {
                    StatusCode = (int)context.Response.StatusCode,
                    StatusDescription = context.Response.StatusCode.ToString(),
                };
            }
            base.OnException(context);
        }
    }

    public class ErrorMessage
    {
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public string CustomMessage { get; set; }
        public string StackTrace { get; set; }
    }
}