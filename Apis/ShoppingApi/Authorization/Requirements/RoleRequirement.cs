using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace ShoppingApi.Authorization.Requirements
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public List<string> Roles { get; set; }

        public RoleRequirement(params string[] roles)
        {
            Roles = new List<string>(roles);
        }
    }
}