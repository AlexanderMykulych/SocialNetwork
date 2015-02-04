namespace Social.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImage : DbMigration
    {
        public override void Up()
        {

            AddColumn("dbo.UserProfile", "Image", c => c.Binary());
            AddColumn("dbo.UserProfile", "ImageType", c => c.String());
            
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfile", "ImageType");
            DropColumn("dbo.UserProfile", "Image");
        }
    }
}
