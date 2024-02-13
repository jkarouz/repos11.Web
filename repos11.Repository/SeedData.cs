using repos11.Repository.Entity;
using repos11.Repository.Entity.UserManagement;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace repos11.Repository
{
    public static class SeedData
    {
        public static void InitialUserManagement(ApplicationDbContext context)
        {
            InitialDataUsers(context);
            InitialDataRoles(context);
            InitialDataUserRoles(context);
            InitialDataPermission(context);
            InitialDataRolePermission(context);
            InitialDataPermissionAction(context);
        }

        private static void InitialDataUsers(ApplicationDbContext context)
        {
            var items = new List<Users>() {
                new Users { UserName = "admin", Name = "Administrator", Email = "admin@mail.com", IsActive = true, CreatedBy = 0, CreatedDate = DateTime.Now },
                new Users { UserName = "admin.master", Name = "Master Administrator", Email = "admin.master@mail.com", IsActive = true, CreatedBy = 0, CreatedDate = DateTime.Now },
            };

            foreach (var item in items)
            {
                var entity = context.Users.Where(w => w.UserName.Trim().ToLower() == item.UserName.Trim().ToLower()).FirstOrDefault();
                if (entity == null)
                {
                    context.Users.Add(item);
                    context.Entry(item).State = EntityState.Added;
                    context.SaveChanges();
                }
            }
        }

        private static void InitialDataRoles(ApplicationDbContext context)
        {
            var items = new List<Roles>() {
                new Roles { Name = "Administrator", Description = "Role for all permission", IsActive = true, CreatedBy = 0, CreatedDate = DateTime.Now },
                new Roles { Name = "Master Administrator", Description = "Role for master permission", IsActive = true, CreatedBy = 0, CreatedDate = DateTime.Now },
            };

            foreach (var item in items)
            {
                var entity = context.Roles.Where(w => w.Name.Trim().ToLower() == item.Name.Trim().ToLower()).FirstOrDefault();
                if (entity == null)
                {
                    context.Roles.Add(item);
                    context.Entry(item).State = EntityState.Added;
                    context.SaveChanges();
                }
            }
        }

        private static void InitialDataUserRoles(ApplicationDbContext context)
        {
            var items = new List<UserRoles>() {
                new UserRoles { UserId = 1, RoleId = 1 },
                new UserRoles { UserId = 2, RoleId = 2 },
            };

            foreach (var item in items)
            {
                var entity = context.UserRoles.Where(w => w.UserId == item.UserId && w.RoleId == item.RoleId).FirstOrDefault();
                if (entity == null)
                {
                    context.UserRoles.Add(item);
                    context.Entry(item).State = EntityState.Added;
                    context.SaveChanges();
                }
            }
        }

        private static void InitialDataPermission(ApplicationDbContext context)
        {
            var items = new List<PermissionCategory>() {
                new PermissionCategory { Name = "User Management", Description = "Category for permission user management", IsActive = true, CreatedBy = 0, CreatedDate = DateTime.Now },
                new PermissionCategory { Name = "Master Management", Description = "Category for permission user management", IsActive = true, CreatedBy = 0, CreatedDate = DateTime.Now },
                new PermissionCategory { Name = "General", Description = "Category for permission general", IsActive = true, CreatedBy = 0, CreatedDate = DateTime.Now },
            };

            foreach (var item in items)
            {
                var entity = context.PermissionCategory.Where(w => w.Name.Trim().ToLower() == item.Name.Trim().ToLower()).FirstOrDefault();
                if (entity == null)
                {
                    context.PermissionCategory.Add(item);
                    context.Entry(item).State = EntityState.Added;
                    context.SaveChanges();
                }
            }

            var items2 = new List<Permissions>() {
                new Permissions { Name = "Home", CategoryId = 3, Description = "view home", IsActive = true, CreatedBy = 0, CreatedDate = DateTime.Now },
                new Permissions { Name = "Manage Users", CategoryId = 1, Description = "manage data users", IsActive = true, CreatedBy = 0, CreatedDate = DateTime.Now },
                new Permissions { Name = "Manage Groups", CategoryId = 1, Description = "manage data groups", IsActive = true, CreatedBy = 0, CreatedDate = DateTime.Now },
                new Permissions { Name = "Manage Roles", CategoryId = 1, Description = "manage data roles", IsActive = true, CreatedBy = 0, CreatedDate = DateTime.Now },
                new Permissions { Name = "Manage Permission", CategoryId = 1, Description = "manage data permission", IsActive = true, CreatedBy = 0, CreatedDate = DateTime.Now },

                new Permissions { Name = "Manage Master Example", CategoryId = 2, Description = "manage data master example", IsActive = true, CreatedBy = 0, CreatedDate = DateTime.Now },
            };

            foreach (var item in items2)
            {
                var entity = context.Permissions.Where(w => w.Name.Trim().ToLower() == item.Name.Trim().ToLower()).FirstOrDefault();
                if (entity == null)
                {
                    context.Permissions.Add(item);
                    context.Entry(item).State = EntityState.Added;
                    context.SaveChanges();
                }
            }
        }

        private static void InitialDataRolePermission(ApplicationDbContext context)
        {
            var items = new List<RolePermissions>() {
                new RolePermissions {RoleId = 1, PermissionId = 1},
                new RolePermissions {RoleId = 1, PermissionId = 2},
                new RolePermissions {RoleId = 1, PermissionId = 3},
                new RolePermissions {RoleId = 1, PermissionId = 4},
                new RolePermissions {RoleId = 1, PermissionId = 5},
                new RolePermissions {RoleId = 1, PermissionId = 6},

                new RolePermissions {RoleId = 2, PermissionId = 1},
                new RolePermissions {RoleId = 2, PermissionId = 6},
            };

            foreach (var item in items)
            {
                var entity = context.RolePermissions.Where(w => w.RoleId == item.RoleId && w.PermissionId == item.PermissionId).FirstOrDefault();
                if (entity == null)
                {
                    context.RolePermissions.Add(item);
                    context.Entry(item).State = EntityState.Added;
                    context.SaveChanges();
                }
            }
        }

        private static void InitialDataPermissionAction(ApplicationDbContext context)
        {
            var items = new List<PermissionActions>() {
                //Home
                new PermissionActions { PermissionId = 1, ActionId = 1},

                //manage users
                new PermissionActions { PermissionId = 2, ActionId = 2},
                new PermissionActions { PermissionId = 2, ActionId = 3},
            };

            foreach (var item in items)
            {
                var entity = context.PermissionActions.Where(w => w.PermissionId == item.PermissionId && w.ActionId == item.ActionId).FirstOrDefault();
                if (entity == null)
                {
                    context.PermissionActions.Add(item);
                    context.Entry(item).State = EntityState.Added;
                    context.SaveChanges();
                }
            }
        }
    }
}
