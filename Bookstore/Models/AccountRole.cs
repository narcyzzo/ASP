using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Bookstore.Models
{
    public class AccountRole: IdentityUserRole<Guid>
    {
        public Account Account { get; set; } = null!;
        public Role Role { get; set; } = null!;
    }
}
