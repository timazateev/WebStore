using Microsoft.AspNetCore.Identity;

namespace WebStoreDomain.Entities.Identity
{
    public class Role : IdentityRole
    {
        public const string Administrator = "Administrators";
        public const string Users = "Users";
    }
}
