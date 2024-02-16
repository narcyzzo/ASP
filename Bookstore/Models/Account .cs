using Microsoft.AspNetCore.Identity;

namespace Bookstore.Models
{
    public class Account : IdentityUser<Guid>
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = null;

        public ICollection<AccountRole> AccountRoles { get; set; } = null!;
    }
}
