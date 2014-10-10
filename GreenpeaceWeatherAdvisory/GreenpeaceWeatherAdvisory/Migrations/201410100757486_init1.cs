namespace GreenpeaceWeatherAdvisory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Advisories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Advisories",
                c => new
                    {
                        AdvisoryID = c.Int(nullable: false, identity: true),
                        Message = c.String(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AdvisoryID);
            
        }
    }
}
