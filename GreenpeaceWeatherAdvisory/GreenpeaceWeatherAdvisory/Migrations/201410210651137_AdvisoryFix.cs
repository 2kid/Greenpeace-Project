namespace GreenpeaceWeatherAdvisory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdvisoryFix : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChikkaSendRequestVMs",
                c => new
                    {
                        RegionId = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.RegionId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ChikkaSendRequestVMs");
        }
    }
}
