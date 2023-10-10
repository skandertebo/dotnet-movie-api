using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TP1.Models;
using TP1.Services;

namespace TP1.Middleware
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class AdminMiddleware : ActionFilterAttribute
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public AdminMiddleware(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var user = (User) context.HttpContext.Items["user"];
            if( user == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            } if (user.Status != User.StatusEnum.Admin)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
