using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopsRUs.Model;

namespace ShopsRUs.Infrastructure.EntityConfigurations
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(ShopsRUsContextSeed.GetProducts());
            builder.Property(x => x.ItemName);
            builder.Property(x => x.Price);
        }
    }
}
