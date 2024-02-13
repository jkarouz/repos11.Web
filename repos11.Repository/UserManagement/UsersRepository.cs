using repos11.Repository.Entity;
using repos11.Repository.Entity.Function;
using repos11.Repository.Entity.StoreProcedure;
using repos11.Repository.Entity.UserManagement;
using repos11.Repository.UserManagement.Interfaces;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace repos11.Repository.UserManagement
{
    public class UsersRepository : Repository<Users>, IUsersRepository
    {
        public UsersRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<Users> GetByUserName(string username)
        {
            return await _context.Users.Where(w => w.UserName == username).FirstOrDefaultAsync();
        }

        public IQueryable<string> GetRoles(long UserId)
        {
            return (from p in _context.Roles
                    join q in _context.UserRoles on p.Id equals q.RoleId
                    where q.UserId == UserId
                    select p.Name);
        }

        public IQueryable<FnGetUsersResult> FnGetUsers(long UserId)
        {
            return _context.FnGetUsers(UserId);
        }

        public async Task<int> FnUsersCount(long UserId)
        {
            var name = "AA";
            var sSQL = $"SELECT [dbo].[FnUsersCount]({UserId}, '{name}')";

            return await _context.Database.SqlQuery<int>(sSQL).FirstOrDefaultAsync();
        }

        public async Task<ObjectResult<SPGetUsersResult>> SPGetUsers(long UserId)
        {
            return await _context.SPGetUsers(UserId);
        }

        public async Task<int> SPUsersCount(long UserId)
        {
            var PUserId = new SqlParameter("@UserId", UserId);

            var returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int);
            returnValue.Direction = ParameterDirection.Output;

            var outParam = new SqlParameter("@Count", SqlDbType.Int);
            outParam.Direction = ParameterDirection.Output;

            var sql = "EXEC @ReturnValue = [dbo].[SPUsersCount] @UserId, @Count OUT";
            var data = _context.Database.SqlQuery<object>(sql, returnValue, PUserId, outParam);

            // Read the results so that the output variables are accessible
            var item = await data.FirstOrDefaultAsync();

            var returnCodeValue = (int)returnValue.Value;
            var outParamValue = (int)outParam.Value;

            return outParamValue;
        }

        public async Task<int> SPInsertUser(long UserId)
        {
            return await _context.SPInsertUser(UserId);
        }

        public IQueryable<FnGetSystemActionModel> GetRolePermission(long UserId)
        {
            return (from p in _context.Users
                    join r in _context.UserRoles on p.Id equals r.UserId
                    join s in _context.RolePermissions on r.RoleId equals s.RoleId
                    join t in _context.PermissionActions on s.PermissionId equals t.PermissionId
                    join u in _context.SystemActions on t.ActionId equals u.Id
                    where p.Id == UserId
                    select new FnGetSystemActionModel
                    {
                        ActionId = t.ActionId,
                        Controller = u.Controller,
                        Action = u.Action
                    });
        }
    }
}
