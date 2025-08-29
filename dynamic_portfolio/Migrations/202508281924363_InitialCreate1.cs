namespace dynamic_portfolio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Role = c.String(nullable: false, maxLength: 256),
                        ProfileImagePath = c.String(maxLength: 512),
                        AboutImagePath = c.String(maxLength: 512),
                        ResumePath = c.String(maxLength: 512),
                        AboutDescription = c.String(),
                        ExperienceYears = c.String(maxLength: 256),
                        Education = c.String(maxLength: 256),
                        LinkedInUrl = c.String(maxLength: 512),
                        GithubUrl = c.String(maxLength: 512),
                        Email = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Profiles");
        }
    }
}
