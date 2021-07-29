using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity
{
    public class UserRole : IdentityUserRole<string>
    {
        public override string UserId { get; set; }

        //public ApplicationUser User { get; set; }

        public override string RoleId { get; set; }

        //public ApplicationRole Role { get; set; }
    }
}