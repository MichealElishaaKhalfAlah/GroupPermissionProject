using GroupPermissionsProject.Core.Entities;
using GroupPermissionsProject.Core.Interfaces;
using GroupPermissionsProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GroupPermissionsProject.Infrastructure.Repositories
{
    public class PermissionsRepository : Repository<Permission>, IPermissionsRepository
    {
        private readonly GroupPermissionsProjectContext _dbContext;
        public PermissionsRepository(GroupPermissionsProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Permission GetPermissionById(int id)
        {
            Permission permission = _dbContext.Permissions.Where(x => x.IsDeleted != true && x.ID == id).FirstOrDefault();
            return permission;
        }

        public List<Permission> GetPermissionsByGroupId(int GroupId)
        {
            List<Permission> permissions = _dbContext.Permissions.Where(x => x.IsDeleted != true && GroupId == 0 || x.userGroupID == GroupId).Include(x=>x.userGroup).ToList();
            return permissions;
        }
    }
}
