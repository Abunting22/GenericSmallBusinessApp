using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GenericSmallBusinessApp.Server.AuthenticationAndAuthorization
{
    public class IdAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var currentId = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            if (currentId == null)
            {
                context.Result = new StatusCodeResult(401);
                return;
            }

            var idFromRoute = context.RouteData.Values["id"]?.ToString();

            if (idFromRoute != currentId)
            {
                context.Result = new StatusCodeResult(403);
                return;
            }
        }
    }
}
