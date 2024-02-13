using System;
using System.Data.Entity.Migrations;

namespace repos11.Repository.Migrations
{
    public partial class AddFuncAndSP : DbMigration
    {
        string sSQL_FnGetUsers = @"CREATE FUNCTION [dbo].[FnGetUsers]
(	
	@UserId INT
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT Name, UserName, Email FROM dbo.Users WHERE Id = @UserId
)";

        string sSQL_SPGetUsers = @"CREATE PROCEDURE [dbo].[SPGetUsers]
(
	@UserId INT
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Name, UserName, Email FROM dbo.Users WHERE Id = @UserId
END";

        string sSQL_SPInsertUser = @"CREATE PROCEDURE [dbo].[SPInsertUser]
(
	@UserId INT
)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT dbo.Users VALUES ('test','test','test@mail.com', NULL, NULL, NULL, 1, 1, GETDATE(), 1, GETDATE())
END";

        string sSQL_FnUserCount = @"CREATE FUNCTION [dbo].[FnUsersCount]
(
	@UserId INT,
	@Name NVARCHAR(200)
)
RETURNS INT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Count INT;

	-- Add the T-SQL statements to compute the return value here
	SELECT @Count = COUNT(*) FROM dbo.Users

	-- Return the result of the function
	RETURN @Count

END";

        string sSQL_SPUsersCount = @"CREATE PROCEDURE [dbo].[SPUsersCount]
    @UserId INT,
    @Count INT OUTPUT
AS
BEGIN
    SELECT @Count=COUNT(*) FROM dbo.Users
    
	RETURN ISNULL(@Count/NULLIF(@UserId, 0), 0)
END";

        public override void Up()
        {
            Sql(sSQL_FnGetUsers);
            Sql(sSQL_FnUserCount);
            Sql(sSQL_SPGetUsers);
            Sql(sSQL_SPInsertUser);
            Sql(sSQL_SPUsersCount);
        }

        public override void Down()
        {
            Sql("DROP FUNCTION [dbo].[FnGetUsers]");
            Sql("DROP FUNCTION [dbo].[FnUsersCount]");
            Sql("DROP PROCEDURE [dbo].[SPGetUsers]");
            Sql("DROP PROCEDURE [dbo].[SPInsertUser]");
            Sql("DROP PROCEDURE [dbo].[SPUsersCount]");
        }
    }
}
