using Adres.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adres.Api.DataSource.ModelConfig
{
    public class AcquisitionConfig : IEntityTypeConfiguration<Acquisition>
    {
        public void Configure(EntityTypeBuilder<Acquisition> builder)
        {
            builder.Property(b => b.Id).IsRequired();
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Budget).IsRequired();
            builder.Property(a => a.Unit).IsRequired();
            builder.Property(a => a.Type).IsRequired();
            builder.Property(a => a.Quantity).IsRequired();
            builder.Property(a => a.UnitValue).IsRequired();
            builder.Property(a => a.TotalValue).IsRequired();
            builder.Property(a => a.AcquisitionDate).IsRequired();
            builder.Property(a => a.Supplier).IsRequired();

            builder
                .HasMany(h => h.Documentation)
                .WithOne()
                .HasForeignKey(r => r.AcquisitionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
