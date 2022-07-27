namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDatePropertiesToRental : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rentals", "DateRented", c => c.DateTime(nullable: false));
            AddColumn("dbo.Rentals", "DateReturned", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rentals", "DateReturned");
            DropColumn("dbo.Rentals", "DateRented");
        }
    }
}
