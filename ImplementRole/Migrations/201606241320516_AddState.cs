namespace ImplementRole.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddState : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.States",
                c => new
                    {
                        StateId = c.Int(nullable: false, identity: true),
                        CountryId = c.Int(nullable: false),
                        StateName = c.String(),
                        Country_CountryId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.StateId)
                .ForeignKey("dbo.Countries", t => t.Country_CountryId)
                .Index(t => t.Country_CountryId);
            
            AddColumn("dbo.AspNetUsers", "StateId", c => c.String());
            AddColumn("dbo.AspNetUsers", "State_StateId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "State_StateId");
            AddForeignKey("dbo.AspNetUsers", "State_StateId", "dbo.States", "StateId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "State_StateId", "dbo.States");
            DropForeignKey("dbo.States", "Country_CountryId", "dbo.Countries");
            DropIndex("dbo.States", new[] { "Country_CountryId" });
            DropIndex("dbo.AspNetUsers", new[] { "State_StateId" });
            DropColumn("dbo.AspNetUsers", "State_StateId");
            DropColumn("dbo.AspNetUsers", "StateId");
            DropTable("dbo.States");
        }
    }
}
