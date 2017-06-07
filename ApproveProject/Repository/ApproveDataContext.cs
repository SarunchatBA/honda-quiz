namespace ApproveProject.Repository
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data.Entity;
    using System.Linq;

    public class ApproveDBInitializer : DropCreateDatabaseAlways<ApproveCreditContext>
    {
        List<ApproveCredit> data = new List<ApproveCredit>();
        public ApproveDBInitializer()
        {
            data.Add(new ApproveCredit { Seq = 1, Name= "Manager 1", Credit = 100000 });
            data.Add(new ApproveCredit { Seq = 2, Name = "Manager 2", Credit = 200000 });
            data.Add(new ApproveCredit { Seq = 3, Name = "Manager 3", Credit = 300000 });
            data.Add(new ApproveCredit { Seq = 4, Name = "GM", Credit = 0 });
        }
        protected override void Seed(ApproveCreditContext context)
        {
            foreach (ApproveCredit p in data)
            {
                context.ApproveCredit.Add(p);
            }
            context.SaveChanges();
            base.Seed(context);
        }
    }


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
            Database.SetInitializer<ApproveCreditContext>(new ApproveDBInitializer());
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