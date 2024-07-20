using Adres.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adres.Api.DataSource.ModelConfig
{
    public class DocumentationFileConfig : IEntityTypeConfiguration<DocumentationFile>
    {
        public void Configure(EntityTypeBuilder<DocumentationFile> builder)
        {
            builder.Property(b => b.Id).IsRequired();
            builder.HasKey(a => a.Id);

            builder.Property(b => b.AcquisitionId).IsRequired();
            builder.HasIndex(b => b.AcquisitionId);

            builder.Property(a => a.Name).IsRequired();
            builder.Property(a => a.Url).IsRequired();
        }
    }
}
