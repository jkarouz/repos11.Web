using repos11.Repository.Entity.StoreProcedure;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace repos11.Repository.Entity
{
    public partial class ApplicationDbContext
    {
        void OnModelCreatingStoreProcedure(DbModelBuilder modelBuilder)
        {
            modelBuilder.ComplexType<SPGetUsersResult>();
        }

        public async Task<int> SPInsertUser(long UserId)
        {
            var PUserId = new SqlParameter("UserId", UserId);

            return await ((IObjectContextAdapter)this).ObjectContext.
                ExecuteStoreCommandAsync("[dbo].[SPInsertUser] @UserId", PUserId);
        }

        public async Task<ObjectResult<SPGetUsersResult>> SPGetUsers(long UserId)
        {
            var PUserId = new SqlParameter("UserId", UserId);

            return await ((IObjectContextAdapter)this).ObjectContext.
                ExecuteStoreQueryAsync<SPGetUsersResult>("[dbo].[SPGetUsers] @UserId", PUserId);
        }
    }
}
