namespace PartyRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImagePath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Equipments", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Equipments", "ImagePath");
        }
    }
}
