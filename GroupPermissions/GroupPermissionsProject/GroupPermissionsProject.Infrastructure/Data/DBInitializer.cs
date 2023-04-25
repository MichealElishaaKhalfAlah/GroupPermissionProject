using GroupPermissionsProject.Core.Identity;
using GroupPermissionsProject.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroupPermissionsProject.Infrastructure.Data
{
    public class DBInitializer 
    {
        private readonly GroupPermissionsProjectContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DBInitializer(GroupPermissionsProjectContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public static void SeedAdminUser(UserManager<ApplicationUser> userManager, GroupPermissionsProjectContext context)
        {
            if (context.Users.FirstOrDefault(u => u.Email == "admin@gmail.com") == null)
            {
                var defaultUser = new ApplicationUser { UserName = "admin@gmail.com", Email = "admin@gmail.com" ,PhoneNumber = "+2123456789"};
                userManager.CreateAsync(defaultUser, "Abc_123456").Wait();
                userManager.AddToRoleAsync(defaultUser, "Admin").Wait();
                context.Member.Add(new Core.Entities.Member { UserId = defaultUser.Id, ProfilePic = "", Name = "admin",IsActive = true });
                context.SaveChanges();
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Admin"
                };
                _ = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Customer").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Customer"
                };
                _ = roleManager.CreateAsync(role).Result;
            }
        }
    }
    }
