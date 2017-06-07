namespace ApproveProject.Repository
{
    using System;
    using System.ComponentModel;
    using System.Data.Entity;
    using System.Linq;

    public class ApproveCreditContext : DbContext
    {
        // Your context has been configured to use a 'ApproveCreditContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'ApproverEx.Repository.ApproveCreditContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ApproveCreditContext' 
        // connection string in the application configuration file.
        public ApproveCreditContext()
            : base("name=ApproveDataContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<ApproveCredit> ApproveCredit { get; set; }
        public virtual DbSet<HistoryTransaction> HistoryTransaction { get; set; }
    }

    public class ApproveCredit
    {
        public int Id { get; set; }
        public int Seq { get; set; }
        public string Name { get; set; }
        public int Credit { get; set; }
    }
    public class HistoryTransaction
    {
        public int Id { get; set; }
        public int MoneyApprove { get; set; }
        public string Log { get; set; }
    }
}