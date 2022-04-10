using GNB.IBM.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace GNB.IBM.Infrastructure.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ConversionRate> ConversionRates { get; set; }
        public DbSet<ProductTransaction> ProductTransactions { get; set; }
    }
}
