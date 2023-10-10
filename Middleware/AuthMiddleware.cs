using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TP1.Context;
using TP1.Services;

namespace TP1.Middleware
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class AuthMiddleware : ActionFilterAttribute
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public AuthMiddleware(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var userService = scope.ServiceProvider.GetRequiredService<UserService>();
                var tokenFromHeader = context.HttpContext.Request.Headers["Authorization"].ToString();
                if (!string.IsNullOrEmpty(tokenFromHeader) && tokenFromHeader.StartsWith("Bearer "))
                {
                    var token = tokenFromHeader.Substring("Bearer ".Length).Trim();
                    var user = userService.GetUserByJwt(token);
                    if (user != null)
                        context.HttpContext.Items["user"] = user;
                    base.OnActionExecuting(context);
                }
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
