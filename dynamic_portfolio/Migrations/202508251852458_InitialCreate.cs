namespace dynamic_portfolio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 256),
                        ImagePath = c.String(nullable: false, maxLength: 512),
                        ImageCss = c.String(maxLength: 256),
                        Alt = c.String(maxLength: 256),
                        GithubUrl = c.String(maxLength: 512),
                        LiveUrl = c.String(maxLength: 512),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Projects");
        }
    }
}
