namespace ApproveProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApproveCredits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Seq = c.Int(nullable: false),
                        Name = c.String(),
                        Credit = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ApproveCredits");
        }
    }
}
