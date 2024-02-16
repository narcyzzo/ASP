using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bookstore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Bookstore.Data
{
    public class BookstoreContext  : IdentityDbContext<Account, Role, Guid,
                               IdentityUserClaim<Guid>, AccountRole, IdentityUserLogin<Guid>,
                               IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public BookstoreContext (DbContextOptions<BookstoreContext> options)
            : base(options)
        {
        }

        public DbSet<Bookstore.Models.Author> Author { get; set; } = default!;
        public DbSet<Bookstore.Models.Book> Book { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author) 
                .WithMany(a => a.Books) 
                .HasForeignKey(b => b.AuthorId);


            modelBuilder.Entity<Account>()
                .HasMany(account => account.AccountRoles)
                .WithOne(accountRole => accountRole.Account)
                .HasForeignKey(account => account.UserId)
                .IsRequired();

            modelBuilder.Entity<Role>()
                .HasMany(role => role.AccountRoles)
                .WithOne(accountRole => accountRole.Role)
                .HasForeignKey(account => account.RoleId)
                .IsRequired();
        }

    }
}
