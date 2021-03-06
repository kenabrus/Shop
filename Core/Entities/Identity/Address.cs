using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Identity
{
    public class Address : BaseEntity
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zippcode { get; set; }

        [Required]
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        
    }
}