using GroupPermissionsProject.Core.Entities;
using System.Collections.Generic;

namespace GroupPermissionsProject.Core.Interfaces
{
    public interface IPermissionsRepository : IRepository<Permission>
    {
        public Permission GetPermissionById(int id);
        public List<Permission> GetPermissionsByGroupId(int GroupId);

    }
}
