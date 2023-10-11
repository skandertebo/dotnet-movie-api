using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TP1.Models;
using TP1.ResponseExceptions;
using TP1.Services;

namespace TP1.Middleware
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class AdminMiddleware : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var user = (User) context.HttpContext.Items["user"];
            if( user == null)
            {
                throw new UnauthorizedException("Invalid User");
            } if (user.Status != User.StatusEnum.Admin)
            {
                throw new UnauthorizedException("User must be an admin");
            }
        }
    }
}
