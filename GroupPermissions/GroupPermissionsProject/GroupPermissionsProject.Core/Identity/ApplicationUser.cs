using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GroupPermissionsProject.Core.Identity
{
    public class ApplicationUser : IdentityUser
    {
        //public string FullName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ProvideName { get; set; }
        public bool IsDeleted { get; set; }
        [NotMapped]
        public string RoleID { get; set; }
    }
}
