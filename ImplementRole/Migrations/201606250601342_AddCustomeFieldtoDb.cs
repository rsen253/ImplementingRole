namespace ImplementRole.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomeFieldtoDb : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Country_CountryId", "dbo.Countries");
            DropForeignKey("dbo.AspNetUsers", "State_StateId", "dbo.States");
            DropIndex("dbo.AspNetUsers", new[] { "Country_CountryId" });
            DropIndex("dbo.AspNetUsers", new[] { "State_StateId" });
            AddColumn("dbo.AspNetUsers", "CountryId", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "StateId", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "Country_CountryId");
            DropColumn("dbo.AspNetUsers", "State_StateId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "State_StateId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Country_CountryId", c => c.Int());
            DropColumn("dbo.AspNetUsers", "StateId");
            DropColumn("dbo.AspNetUsers", "CountryId");
            CreateIndex("dbo.AspNetUsers", "State_StateId");
            CreateIndex("dbo.AspNetUsers", "Country_CountryId");
            AddForeignKey("dbo.AspNetUsers", "State_StateId", "dbo.States", "StateId");
            AddForeignKey("dbo.AspNetUsers", "Country_CountryId", "dbo.Countries", "CountryId");
        }
    }
}
