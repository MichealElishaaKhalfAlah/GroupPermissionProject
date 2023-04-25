using GroupPermissionsProject.Core.Identity;
using GroupPermissionsProject.Core.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GroupPermissionsProject.Core.Entities
{
    public partial class UserGroup : BaseEntity
    {
        public UserGroup()
        {
        }
        [Required]
        public string ArabicName { get; set; }
        [Required]
        public string EnglishName { get; set; }
    }
}
