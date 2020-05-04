namespace MyShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomersCorrected : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "City", c => c.String());
            DropColumn("dbo.Customers", "Cirty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "Cirty", c => c.String());
            DropColumn("dbo.Customers", "City");
        }
    }
}
