namespace Social.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Image : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Image", c => c.Binary());
            AddColumn("dbo.Users", "ImageType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "ImageType");
            DropColumn("dbo.Users", "Image");
        }
    }
}
