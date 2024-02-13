using CodeFirstStoreFunctions;
using repos11.Repository.Entity.Systems;
using repos11.Repository.Entity.UserManagement;
using Serilog;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace repos11.Repository.Entity
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("name=ApplicationDatabase")
        {
        }

        public DbSet<SystemActions> SystemActions { get; set; }
        public DbSet<SystemParamaters> SystemParamaters { get; set; }

        public DbSet<Users> Users { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<PermissionCategory> PermissionCategory { get; set; }
        public DbSet<Permissions> Permissions { get; set; }

        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<UserGroups> UserGroups { get; set; }
        public DbSet<GroupRoles> GroupRoles { get; set; }
        public DbSet<RolePermissions> RolePermissions { get; set; }
        public DbSet<PermissionActions> PermissionActions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Add(new FunctionsConvention<ApplicationDbContext>("dbo"));
            Database.Log = Log.Logger.Debug;

            modelBuilder.Entity<UserRoles>().HasKey(key => new { key.UserId, key.RoleId });
            modelBuilder.Entity<UserGroups>().HasKey(key => new { key.UserId, key.GroupId });
            modelBuilder.Entity<GroupRoles>().HasKey(key => new { key.GroupId, key.RoleId });
            modelBuilder.Entity<RolePermissions>().HasKey(key => new { key.RoleId, key.PermissionId });
            modelBuilder.Entity<PermissionActions>().HasKey(key => new { key.PermissionId, key.ActionId });

            OnModelCreatingFunction(modelBuilder);
            OnModelCreatingStoreProcedure(modelBuilder);
        }
    }
}
