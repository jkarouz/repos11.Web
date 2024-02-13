using repos11.Repository.Entity.Function;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace repos11.Repository.Entity
{
    public partial class ApplicationDbContext
    {
        void OnModelCreatingFunction(DbModelBuilder modelBuilder)
        {
            modelBuilder.ComplexType<FnGetUsersResult>();
        }

        [DbFunction(nameof(ApplicationDbContext), "FnGetUsers")]
        public IQueryable<FnGetUsersResult> FnGetUsers(long UserId)
        {
            var PUserId = new ObjectParameter("UserId", UserId);

            return ((IObjectContextAdapter)this).ObjectContext
                .CreateQuery<FnGetUsersResult>(string.Format("[{0}].{1}", nameof(ApplicationDbContext), "[FnGetUsers](@UserId)"), PUserId);
        }
    }
}
