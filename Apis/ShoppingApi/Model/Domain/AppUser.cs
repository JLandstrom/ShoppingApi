using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ShoppingApi.Model.Domain
{
    public class AppUser : IdentityUser
    {
        public string Role { get; set; }
    }
}
