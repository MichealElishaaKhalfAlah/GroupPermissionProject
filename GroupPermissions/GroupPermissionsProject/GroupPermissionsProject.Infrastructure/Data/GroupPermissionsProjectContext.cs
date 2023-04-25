
using GroupPermissionsProject.Core.Entities;
using GroupPermissionsProject.Core.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupPermissionsProject.Infrastructure.Data
{
    public class GroupPermissionsProjectContext: IdentityDbContext<ApplicationUser>
    {
        public GroupPermissionsProjectContext()
        {

        }

        public GroupPermissionsProjectContext(DbContextOptions<GroupPermissionsProjectContext> options)
           : base(options)
        {


        }
        //contextes
        #region "contextes"    
        public DbSet<Member> Member { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region "Seed Data"           
            modelBuilder.Entity<IdentityRole>().HasData(
               new { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
               new { Id = "2", Name = "Customer", NormalizedName = "CUSTOMER" }
           // new { Id = "3", Name = "ServiceCenter", NormalizedName = "ServiceCenter" }
           );
           #endregion
        }
    }
}