using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : IdentityDbContext<
                                AppUser, 
                                AppRole, 
                                int, 
                                IdentityUserClaim<int>, 
                                AppUserRole, 
                                IdentityUserLogin<int>, 
                                IdentityRoleClaim<int>, 
                                IdentityUserToken<int>
                            >
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            builder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

        }

        //tega ne rabimo vec ker nam identity to provida
        //public DbSet<AppUser> Users { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}