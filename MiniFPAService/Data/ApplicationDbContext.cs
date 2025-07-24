using Microsoft.EntityFrameworkCore;
using MiniFPAService.Models;

namespace MiniFPAService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<FinancialRecord> FinancialRecords { get; set; }
    }
}


