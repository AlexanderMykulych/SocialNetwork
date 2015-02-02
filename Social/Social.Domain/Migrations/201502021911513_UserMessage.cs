namespace Social.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserMessage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserMessages",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserMessages");
        }
    }
}
