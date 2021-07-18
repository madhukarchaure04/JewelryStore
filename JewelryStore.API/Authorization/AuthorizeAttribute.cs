using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace JewelryStore.API.Authorization
{
    /// <summary>
    /// Custom implementation for Authorize attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //Checking if the user is valid based on key stored in HTTPContext by Authrozation process
            var customer = context.HttpContext.Items["User"];
            if (customer == null)
                //If user is not authorize to move further, returning status code 401.
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}
