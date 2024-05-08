namespace Pims.Persistent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createSeller1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sellers", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sellers", "Name");
        }
    }
}
