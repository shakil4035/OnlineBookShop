namespace Pims.Persistent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createSeller : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sellers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdNo = c.String(),
                        Gender = c.String(),
                        JoinjngDate = c.DateTime(nullable: false),
                        BirithDate = c.DateTime(nullable: false),
                        MeritialStatus = c.String(),
                        Email = c.String(),
                        PhoneNo = c.String(),
                        Address = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                        CreateBy = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateBy = c.String(),
                        UpdateDate = c.DateTime(),
                        DeleteBy = c.String(),
                        DeleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sellers");
        }
    }
}
