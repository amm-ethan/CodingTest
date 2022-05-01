using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Configurations;

namespace Repository
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        }

        public DbSet<Transaction>? Transactions { get; set; }
        public DbSet<ImportDetail>? ImportDetails { get; set; }
    }
}