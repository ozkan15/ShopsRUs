using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopsRUs.Model;

namespace ShopsRUs.Infrastructure.EntityConfigurations
{
    public class InvoiceEntityTypeConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.Property(x => x.AmoutTotal);
            builder.Property(x => x.AmountNet);
            builder.Property(x => x.Date);
        }
    }
}
