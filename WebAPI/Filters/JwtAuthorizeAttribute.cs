using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPI.Authentication;
using Domain.Constants;

namespace WebAPI.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class JwtAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] allowedRoles;
        public JwtAuthorizeAttribute(params string[] roles)
        {
            this.allowedRoles = roles;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var tokenManager = context.HttpContext.RequestServices.GetService(typeof(IJwtTokenManager)) as IJwtTokenManager;
            ClaimsPrincipal claims = tokenManager.VerifyToken(token);
            if (tokenManager == null || claims == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            else
            {
                context.HttpContext.Items[USER_IDENTITY_CONST.USERNAME] = claims.Identity.Name;
                bool flagClaim = false;

                if (allowedRoles == null || allowedRoles.Length == 0)
                    flagClaim = true;
                else
                {
                    foreach (var item in allowedRoles)
                    {
                        var hasClaim = claims.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role
                                          && x.Value == item);
                        if (hasClaim != null)
                            flagClaim = true;
                    }
                }
                if (!flagClaim)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

            }
        }
    }
}
