using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity
{
    public class UserRole : IdentityUserRole<int>
    {
        public override int UserId { get; set; }

        //public ApplicationUser User { get; set; }

        public override int RoleId { get; set; }

        //public ApplicationRole Role { get; set; }
    }
}