using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopsRUs.Model;

namespace ShopsRUs.Infrastructure.EntityConfigurations
{
    public class InvoiceDetailEntityTypeConfiguration : IEntityTypeConfiguration<InvoiceDetail>
    {
        public void Configure(EntityTypeBuilder<InvoiceDetail> builder)
        {
            builder.HasOne(x => x.Product).WithMany(y => y.InvoiceDetails).HasForeignKey(y => y.ProductId);
            builder.HasOne(x => x.Invoice).WithMany(y => y.InvoiceDetails).HasForeignKey(y => y.InvoiceId);
            builder.Property(x => x.Amount);
            builder.Property(x => x.Quantity);
        }
    }
}
