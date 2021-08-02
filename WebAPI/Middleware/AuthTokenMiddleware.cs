using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Common.Configuration;

namespace WebAPI.Middleware
{
    public class AuthTokenMiddleware : IMiddleware
    {
        
        private readonly GetAuthToken _getAuthToken;
       
        public AuthTokenMiddleware(IOptions<GetAuthToken> authToken)
        {
            _getAuthToken = authToken.Value; 
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            
           if(context != null && context.Request.Headers.ContainsKey("Guid"))
            {            
                var token = context.Request.Headers["Guid"].FirstOrDefault();

                if(!string.IsNullOrEmpty(token))
                {
                    if(string.Equals(token, _getAuthToken.Guid, StringComparison.Ordinal))
                    {
                        if (context.Request.Headers["Guid"] == _getAuthToken.Guid)
                        {
                            await next(context);
                        }
                    }
                    else
                    {
                        context.Response.StatusCode = 401;
                        return;
                    }
                }    
            }   
        }
    }
}
