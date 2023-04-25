using GroupPermissionsProject.Core.Entities;

namespace GroupPermissionsProject.Core.Interfaces
{
    public interface IUserGroupRepository : IRepository<UserGroup>
    {
        public UserGroup GetUserGroupById(int id);

    }
}
