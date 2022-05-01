using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasIndex(e => e.TransactionId)
                .IsUnique();

            builder.Property(e => e.Status)
                .HasConversion(
            v => v.ToString(),
            v => (Status)Enum.Parse(typeof(Status), v));
        }
    }
}
