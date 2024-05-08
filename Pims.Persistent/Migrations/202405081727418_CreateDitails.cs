namespace Pims.Persistent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDitails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PurchesDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PurchesId = c.Int(nullable: false),
                        Quantity = c.Double(nullable: false),
                        Price = c.Double(nullable: false),
                        Unit = c.String(),
                        LineTotal = c.Double(nullable: false),
                        BookId = c.Int(nullable: false),
                        BookName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Purches", t => t.PurchesId, cascadeDelete: true)
                .Index(t => t.PurchesId);
            
            CreateTable(
                "dbo.Purches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(),
                        Date = c.DateTime(nullable: false),
                        AuthorId = c.Int(nullable: false),
                        TotalBill = c.Decimal(precision: 18, scale: 2),
                        Discount = c.Decimal(precision: 18, scale: 2),
                        DiscountPercentage = c.Decimal(precision: 18, scale: 2),
                        Tax = c.Decimal(precision: 18, scale: 2),
                        TaxPercentage = c.Decimal(precision: 18, scale: 2),
                        Payable = c.Decimal(precision: 18, scale: 2),
                        IsDelete = c.Boolean(nullable: false),
                        CreateBy = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateBy = c.String(),
                        UpdateDate = c.DateTime(),
                        DeleteBy = c.String(),
                        DeleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Authors", t => t.AuthorId, cascadeDelete: true)
                .Index(t => t.AuthorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchesDetails", "PurchesId", "dbo.Purches");
            DropForeignKey("dbo.Purches", "AuthorId", "dbo.Authors");
            DropIndex("dbo.PurchesDetails", new[] { "PurchesId" });
            DropIndex("dbo.Purches", new[] { "AuthorId" });
            DropTable("dbo.Purches");
            DropTable("dbo.PurchesDetails");
        }
    }
}
