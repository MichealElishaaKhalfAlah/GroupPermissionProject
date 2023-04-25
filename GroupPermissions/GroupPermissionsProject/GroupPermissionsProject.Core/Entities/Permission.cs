using GroupPermissionsProject.Core.Identity;
using GroupPermissionsProject.Core.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupPermissionsProject.Core.Entities
{
    public partial class Permission : BaseEntity
    {
        public Permission()
        {
        }
        public int userGroupID { get; set; }
        public UserGroup userGroup { get; set; }
        public int PageId { get; set; }
        public bool? CanView { get; set; }
        public bool? CanAdd { get; set; }
        public bool? CanEdit { get; set; }
        public bool? CanDelete { get; set; }
    }
}
