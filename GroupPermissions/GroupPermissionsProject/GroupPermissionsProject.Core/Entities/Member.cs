using GroupPermissionsProject.Core.Identity;
using GroupPermissionsProject.Core.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupPermissionsProject.Core.Entities
{
    public partial class Member : BaseEntity
    {
        public Member()
        {
        }
        public string Name { get; set; }
        public string ProfilePic { get; set; }
        public string UserId { get; set; }
        public bool? IsActive { get; set; }
        public ApplicationUser User { get; set; }
        public bool? IsAdmin { get; set; }
    }
}
