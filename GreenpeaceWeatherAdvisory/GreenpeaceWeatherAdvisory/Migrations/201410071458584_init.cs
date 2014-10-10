namespace GreenpeaceWeatherAdvisory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChikkaMessages",
                c => new
                    {
                        ChikkaMessageId = c.Int(nullable: false, identity: true),
                        Message = c.String(nullable: false),
                        Status = c.String(),
                        ContactId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ChikkaMessageId)
                .ForeignKey("dbo.ContactDetails", t => t.ContactId, cascadeDelete: true)
                .Index(t => t.ContactId);
            
            CreateTable(
                "dbo.ContactDetails",
                c => new
                    {
                        ContactDetailId = c.Int(nullable: false, identity: true),
                        MobileNumber = c.String(nullable: false),
                        FarmerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ContactDetailId)
                .ForeignKey("dbo.Farmers", t => t.FarmerId, cascadeDelete: true)
                .Index(t => t.FarmerId);
            
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        RegionId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RegionId);
            
            AddColumn("dbo.Farmers", "RegionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Farmers", "RegionId");
            AddForeignKey("dbo.Farmers", "RegionId", "dbo.Regions", "RegionId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChikkaMessages", "ContactId", "dbo.ContactDetails");
            DropForeignKey("dbo.ContactDetails", "FarmerId", "dbo.Farmers");
            DropForeignKey("dbo.Farmers", "RegionId", "dbo.Regions");
            DropIndex("dbo.Farmers", new[] { "RegionId" });
            DropIndex("dbo.ContactDetails", new[] { "FarmerId" });
            DropIndex("dbo.ChikkaMessages", new[] { "ContactId" });
            DropColumn("dbo.Farmers", "RegionId");
            DropTable("dbo.Regions");
            DropTable("dbo.ContactDetails");
            DropTable("dbo.ChikkaMessages");
        }
    }
}
