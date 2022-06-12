namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
        INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'd014438d-d19e-470b-9e0d-0a68c7561f62', N'admin@vidly', 0, N'ABBo1lltbjddJYkK6PysDqDAD0VX5QwTVpIcLW8WkMFEup9EamADdGf/DEouqYp05A==', N'6a795d59-97c4-4721-9e7f-8fcd9a45b0c9', NULL, 0, 0, NULL, 1, 0, N'admin@vidly')
        INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'f618f303-9a59-4237-89ff-447247b4e36e', N'guest@vidly', 0, N'AKL9VpIjggo/xzQEk8xzieRoY7lfxy1AeO2sz9DgQqR1/XtMWJsHAdfj8fu1DG9ffw==', N'3ba713f1-ff5c-4e4a-bc1b-c951114b5574', NULL, 0, 0, NULL, 1, 0, N'guest@vidly')

        INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'c24243b5-6a8a-4f9a-95b3-bffe7593c2b2', N'CanManageMovies')
        
        INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd014438d-d19e-470b-9e0d-0a68c7561f62', N'c24243b5-6a8a-4f9a-95b3-bffe7593c2b2') 
        ");
        }
        
        public override void Down()
        {
        }
    }
}
