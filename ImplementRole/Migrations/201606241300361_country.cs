namespace ImplementRole.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class country : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryId = c.String(nullable: false, maxLength: 128),
                        CountryName = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CountryId);
            
            AddColumn("dbo.AspNetUsers", "CountryId", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "CountryId");
            AddForeignKey("dbo.AspNetUsers", "CountryId", "dbo.Countries", "CountryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "CountryId", "dbo.Countries");
            DropIndex("dbo.AspNetUsers", new[] { "CountryId" });
            DropColumn("dbo.AspNetUsers", "CountryId");
            DropTable("dbo.Countries");
        }
    }
}
