using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace WebAPI.Authorize
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method)]
    public class CustomAuthorization : Attribute, IAuthorizationFilter
    {
        public const string ValidUserKey = "isValidKey";

        public void OnAuthorization(AuthorizationFilterContext context)
        {     
            if (context?.HttpContext?.Items!=null)
            {
                bool isAuthorized = context.HttpContext.Items.ContainsKey(ValidUserKey)
                    && context.HttpContext.Items[ValidUserKey] is bool 
                    && (bool)context.HttpContext.Items[ValidUserKey];
                
                if (!isAuthorized)
                {
                    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                }     
            }        
        }
    }


}
