using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace MiniFPAService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            EnsureDatabaseCreated();
        }

        private void EnsureDatabaseCreated()
        {
            var dbPath = "Data/financialrecords.db";
            if (!File.Exists(dbPath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(dbPath));
                File.Create(dbPath).Dispose();
            }
        }

        // Define your DbSets here, e.g.:
        // public DbSet<FinancialRecord> FinancialRecords { get; set; }
    }
}

