using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace DiseaseMIS.BAL.Filters
{
    // Gautam Add - Attributes to authorize users
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {

        public class CustomClaimTypes
        {
            public const string Permission = "Application.Permission";
        }

        public AuthorizeAttribute()
        {

        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User.Identity;
            if (context.HttpContext.User.IsInRole("SibinAdmin"))
            {
                return;
            }

            if (user == null)
                context.Result = new JsonResult(new { message = "Unauthorized" })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };

            var _methodName = context.ActionDescriptor.DisplayName.ToLower().Split(".").Last();
            _methodName = _methodName.Split("(").First().Replace(" ", "");

            var hasClaim = context.HttpContext.User.
               Claims.Any(c => c.Type == CustomClaimTypes.Permission && c.Value.ToLower()
               .Contains(_methodName));
            if (!hasClaim)
            {
                context.Result = new UnauthorizedResult();
            }
        }

    }
}
