using repos11.Repository.Entity;
using repos11.Repository.Entity.Function;
using repos11.Repository.Entity.Systems;
using repos11.Repository.Systems.Interfaces;
using System.Linq;

namespace repos11.Repository.Systems
{
    public class SystemActionsRepository : Repository<SystemActions>, ISystemActionsRepository
    {
        public SystemActionsRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
