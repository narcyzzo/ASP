using Microsoft.AspNetCore.Identity;

namespace Bookstore.Models
{
    public class Role : IdentityRole<Guid>
    {
        public ICollection<AccountRole> AccountRoles { get; set; } = null!;
    }
}
