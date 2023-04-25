using GroupPermissionsProject.Core.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupPermissionsProject.Core.Shared
{
    public abstract class BaseEntity
    {
        public int ID { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedByNameId { get; set; }
        public string ModifiedByNameId { get; set; }
        public ApplicationUser CreatedByName { get; set; }
        public ApplicationUser ModifiedByName { get; set; }
    }
}
