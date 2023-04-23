using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;

namespace WebAppMovie.Filters
{
    public class APIKeyAuthFilterAttribute : Attribute, IAuthorizationFilter
    {
        private const string ApiKeyHeader = "ApiKey";
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeader, out var clientApiKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

           var config = context.HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;
           if (config != null)
            {
                var apiKey = config.GetValue<string>($"ApiKey:anotherclient");

                if (apiKey != clientApiKey)
                {
                    context.Result = new UnauthorizedResult();
                }
            }
            else
            {
                context.Result = new UnauthorizedResult();
                return;
            }


        }
    }
}
