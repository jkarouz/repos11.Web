using repos11.Repository.Entity;
using repos11.Repository.Entity.Function;
using repos11.Repository.Entity.UserManagement;
using repos11.Repository.UserManagement.Interfaces;
using System.Data;
using System.Linq;

namespace repos11.Repository.UserManagement
{
    public class GroupsRepository : Repository<Groups>, IGroupsRepository
    {
        public GroupsRepository(ApplicationDbContext context) : base(context)
        {

        }

        public IQueryable<string> GetRoles(long GroupId)
        {
            return (from p in _context.Roles
                    join q in _context.GroupRoles on p.Id equals q.RoleId
                    where q.GroupId == GroupId
                    select p.Name);
        }

        public IQueryable<string> GetRoleByUser(long UserId)
        {
            return (from p in _context.Roles
                    join q in _context.GroupRoles on p.Id equals q.RoleId
                    join r in _context.UserGroups on q.GroupId equals r.GroupId
                    where r.UserId == UserId
                    select p.Name);
        }

        public IQueryable<Groups> GetByUser(long UserId)
        {
            return (from p in _context.Groups
                    join q in _context.UserGroups on p.Id equals q.GroupId
                    where q.UserId == UserId
                    select p);
        }

        public IQueryable<FnGetSystemActionModel> GetRolePermissionByUser(long UserId)
        {
            return (from p in _context.Groups
                    join q in _context.UserGroups on p.Id equals q.GroupId
                    join r in _context.GroupRoles on q.GroupId equals r.GroupId
                    join s in _context.RolePermissions on r.RoleId equals s.RoleId
                    join t in _context.PermissionActions on s.PermissionId equals t.PermissionId
                    join u in _context.SystemActions on t.ActionId equals u.Id
                    where q.UserId == UserId
                    select new FnGetSystemActionModel
                    {
                        ActionId = t.ActionId,
                        Controller = u.Controller,
                        Action = u.Action
                    });
        }
    }
}
