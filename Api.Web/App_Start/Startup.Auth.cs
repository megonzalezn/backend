using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Web
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
                context.Response.Headers.Add("Access-Control-Allow-Methods", new[] { "GET, POST, PUT, DELETE, OPTIONS" });
                context.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "Content-Type, Authorization, ETag, Last-Modified, If-None-Match" });


                if (context.Request.Method == "OPTIONS")
                {
                    context.Response.StatusCode = 200;
                }
                else
                {
#pragma warning disable S3216 // "ConfigureAwait(false)" should be used
                    await next();
#pragma warning restore S3216 // "ConfigureAwait(false)" should be used
                }
            });
        }
    }
}