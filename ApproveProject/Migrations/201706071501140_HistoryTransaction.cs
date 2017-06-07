namespace ApproveProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HistoryTransaction : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HistoryTransactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MoneyApprove = c.Int(nullable: false),
                        Log = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HistoryTransactions");
        }
    }
}
