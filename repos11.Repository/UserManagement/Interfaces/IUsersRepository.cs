using repos11.Repository.Entity.Function;
using repos11.Repository.Entity.StoreProcedure;
using repos11.Repository.Entity.UserManagement;
using repos11.Repository.Interfaces;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Threading.Tasks;

namespace repos11.Repository.UserManagement.Interfaces
{
    public interface IUsersRepository : IRepository<Users>
    {
        Task<Users> GetByUserName(string username);
        IQueryable<string> GetRoles(long UserId);
        IQueryable<FnGetUsersResult> FnGetUsers(long UserId);
        Task<int> FnUsersCount(long UserId);
        Task<ObjectResult<SPGetUsersResult>> SPGetUsers(long UserId);
        Task<int> SPUsersCount(long UserId);
        Task<int> SPInsertUser(long UserId);
        IQueryable<FnGetSystemActionModel> GetRolePermission(long UserId);
    }
}
