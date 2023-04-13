using DocumentStorageMVC.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocumentStorageMVC.Data
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(x => x.Id).IsRequired();

            builder.Property(x => x.Title).IsRequired().HasMaxLength(30);
            
            builder.Property(x => x.Date).IsRequired();

            builder.Property(x => x.Author).IsRequired();

            builder.Property(x => x.DocumentType).IsRequired();

            builder.Property(x => x.Path).IsRequired();
        }
    }
}
