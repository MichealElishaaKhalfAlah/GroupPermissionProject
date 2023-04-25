
using GroupPermissionsProject.Core.Entities;
using GroupPermissionsProject.Core.Identity;
using GroupPermissionsProject.Core.Interfaces;
using GroupPermissionsProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroupPermissionsProject.Infrastructure.Repositories
{
    public class UserGroupRepository : Repository<UserGroup>, IUserGroupRepository
    {
        private readonly GroupPermissionsProjectContext _dbContext;
        public UserGroupRepository(GroupPermissionsProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public UserGroup GetUserGroupById(int id)
        {
            UserGroup userGroup = _dbContext.UserGroups.Where(x => x.IsDeleted != true && x.ID == id).FirstOrDefault();
            return userGroup;
        }
    }
}
