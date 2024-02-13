using repos11.Repository.Entity.Function;
using repos11.Repository.Entity.UserManagement;
using repos11.Repository.Interfaces;
using System.Linq;

namespace repos11.Repository.UserManagement.Interfaces
{
    public interface IGroupsRepository : IRepository<Groups>
    {
        IQueryable<string> GetRoles(long GroupId);
        IQueryable<string> GetRoleByUser(long UserId);
        IQueryable<FnGetSystemActionModel> GetRolePermissionByUser(long UserId);
        IQueryable<Groups> GetByUser(long UserId);
    }
}
