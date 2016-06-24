namespace ImplementRole.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAutoincrement : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.States", "Country_CountryId", "dbo.Countries");
            DropForeignKey("dbo.AspNetUsers", "CountryId", "dbo.Countries");
            DropPrimaryKey("dbo.Countries");
            AlterColumn("dbo.Countries", "CountryId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Countries", "CountryId");
            AddForeignKey("dbo.States", "Country_CountryId", "dbo.Countries", "CountryId");
            AddForeignKey("dbo.AspNetUsers", "CountryId", "dbo.Countries", "CountryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.States", "Country_CountryId", "dbo.Countries");
            DropPrimaryKey("dbo.Countries");
            AlterColumn("dbo.Countries", "CountryId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Countries", "CountryId");
            AddForeignKey("dbo.AspNetUsers", "CountryId", "dbo.Countries", "CountryId");
            AddForeignKey("dbo.States", "Country_CountryId", "dbo.Countries", "CountryId");
        }
    }
}
