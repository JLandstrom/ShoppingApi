using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using ShoppingApi.Authorization.Requirements;
using ShoppingApi.Utils;

namespace ShoppingApi.Authorization.Handlers
{
    public class RoleAuthorizationHandler : AuthorizationHandler<RoleRequirement>
    {
        private readonly IConfiguration _config;

        public RoleAuthorizationHandler(IConfiguration config)
        {
            _config = config;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == Constants.Strings.JwtClaimIdentifiers.Role &&
                                            c.Issuer == _config["Jwt:Issuer"]))
            {
                return Task.CompletedTask;
            }

            var role = context.User.FindFirst(c =>
                c.Type == Constants.Strings.JwtClaimIdentifiers.Role && c.Issuer == _config["Jwt:Issuer"])?.Value;

            if (role != null && requirement.Roles.Contains(role))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}