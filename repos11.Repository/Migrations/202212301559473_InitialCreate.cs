using System;
using System.Data.Entity.Migrations;

namespace repos11.Repository.Migrations
{
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GroupRoles",
                c => new
                {
                    GroupId = c.Long(nullable: false),
                    RoleId = c.Long(nullable: false),
                })
                .PrimaryKey(t => new { t.GroupId, t.RoleId });

            CreateTable(
                "dbo.Groups",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 200),
                    Description = c.String(maxLength: 4000),
                    IsActive = c.Boolean(nullable: false),
                    CreatedBy = c.Int(nullable: false),
                    CreatedDate = c.DateTime(nullable: false),
                    UpdatedBy = c.Int(),
                    UpdatedDate = c.DateTime(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.PermissionActions",
                c => new
                {
                    PermissionId = c.Long(nullable: false),
                    ActionId = c.Long(nullable: false),
                })
                .PrimaryKey(t => new { t.PermissionId, t.ActionId });

            CreateTable(
                "dbo.PermissionCategory",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 200),
                    Description = c.String(maxLength: 4000),
                    IsActive = c.Boolean(nullable: false),
                    CreatedBy = c.Int(nullable: false),
                    CreatedDate = c.DateTime(nullable: false),
                    UpdatedBy = c.Int(),
                    UpdatedDate = c.DateTime(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Permissions",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    CategoryId = c.Long(nullable: false),
                    Name = c.String(nullable: false, maxLength: 200),
                    Description = c.String(maxLength: 4000),
                    IsActive = c.Boolean(nullable: false),
                    CreatedBy = c.Int(nullable: false),
                    CreatedDate = c.DateTime(nullable: false),
                    UpdatedBy = c.Int(),
                    UpdatedDate = c.DateTime(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.RolePermissions",
                c => new
                {
                    RoleId = c.Long(nullable: false),
                    PermissionId = c.Long(nullable: false),
                })
                .PrimaryKey(t => new { t.RoleId, t.PermissionId });

            CreateTable(
                "dbo.Roles",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 200),
                    Description = c.String(maxLength: 4000),
                    IsActive = c.Boolean(nullable: false),
                    CreatedBy = c.Int(nullable: false),
                    CreatedDate = c.DateTime(nullable: false),
                    UpdatedBy = c.Int(),
                    UpdatedDate = c.DateTime(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.SystemActions",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    Controller = c.String(nullable: false, maxLength: 200),
                    Action = c.String(nullable: false, maxLength: 200),
                    Description = c.String(maxLength: 4000),
                    IsActive = c.Boolean(nullable: false),
                    CreatedBy = c.Int(nullable: false),
                    CreatedDate = c.DateTime(nullable: false),
                    UpdatedBy = c.Int(),
                    UpdatedDate = c.DateTime(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.SystemParamaters",
                c => new
                {
                    Code = c.String(nullable: false, maxLength: 100),
                    Value1 = c.String(nullable: false, maxLength: 200),
                    Value2 = c.String(maxLength: 200),
                    Description = c.String(maxLength: 4000),
                })
                .PrimaryKey(t => t.Code);

            CreateTable(
                "dbo.UserGroups",
                c => new
                {
                    UserId = c.Long(nullable: false),
                    GroupId = c.Long(nullable: false),
                })
                .PrimaryKey(t => new { t.UserId, t.GroupId });

            CreateTable(
                "dbo.UserRoles",
                c => new
                {
                    UserId = c.Long(nullable: false),
                    RoleId = c.Long(nullable: false),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId });

            CreateTable(
                "dbo.Users",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    UserName = c.String(nullable: false, maxLength: 100),
                    Name = c.String(nullable: false, maxLength: 200),
                    Email = c.String(nullable: false, maxLength: 100),
                    PhoneNumber = c.String(maxLength: 100),
                    EmployeeCode = c.String(maxLength: 100),
                    UserCode = c.String(maxLength: 100),
                    IsActive = c.Boolean(nullable: false),
                    CreatedBy = c.Int(nullable: false),
                    CreatedDate = c.DateTime(nullable: false),
                    UpdatedBy = c.Int(),
                    UpdatedDate = c.DateTime(),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.UserRoles");
            DropTable("dbo.UserGroups");
            DropTable("dbo.SystemParamaters");
            DropTable("dbo.SystemActions");
            DropTable("dbo.Roles");
            DropTable("dbo.RolePermissions");
            DropTable("dbo.Permissions");
            DropTable("dbo.PermissionCategory");
            DropTable("dbo.PermissionActions");
            DropTable("dbo.Groups");
            DropTable("dbo.GroupRoles");
        }
    }
}
