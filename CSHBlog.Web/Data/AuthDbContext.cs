using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CSHBlog.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Seed Roles (User, Admin, SuperAdmin)
            var userRoleId = "20ef142a-1345-4601-88a0-750af911de21";
            var adminRoleId = "225a4712-57d6-4555-83e2-df7dc0059d06";
            var superAdminRoleId = "e6ed604a-49ff-45c2-9285-6a23beec29da";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                    {
                        Name = "User",
                        NormalizedName = "User",
                        Id = userRoleId,
                        ConcurrencyStamp = userRoleId
                    },
                new IdentityRole
                    {
                        Name = "Admin",
                        NormalizedName = "Admin",
                        Id = adminRoleId,
                        ConcurrencyStamp = adminRoleId
                    },
                new IdentityRole
                    {
                        Name = "SuperAdmin",
                        NormalizedName = "SuperAdmin",
                        Id = superAdminRoleId,
                        ConcurrencyStamp = superAdminRoleId
                    }
            };

            //Add to IdentityRole
            builder.Entity<IdentityRole>().HasData(roles);

            //Seed SuperAdminUser
            var superAdminId = "a9acf2a0-ce25-401b-aa08-3735699c6d05";

            var superAdminUser = new IdentityUser
            {
                UserName = "csh7183",
                NormalizedUserName = "csh7183".ToUpper(),
                Email = "superadmin@csh.com",
                NormalizedEmail = "superadmin@csh.com".ToUpper(),
                Id = superAdminId
            };

            //SetPassword
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "ratech1986");

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            //Add All roles to SuperAdminUser
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string> {
                RoleId = userRoleId,
                UserId = superAdminId
                },
                new IdentityUserRole<string> {
                RoleId = adminRoleId,
                UserId = superAdminId
                },
                new IdentityUserRole<string> {
                RoleId = superAdminRoleId,
                UserId = superAdminId
                },
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}
