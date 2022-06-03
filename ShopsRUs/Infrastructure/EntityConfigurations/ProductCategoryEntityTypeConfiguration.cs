using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopsRUs.Model;

namespace ShopsRUs.Infrastructure.EntityConfigurations
{
    public class ProductCategoryEntityTypeConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.HasData(ShopsRUsContextSeed.GetProductCategories());
            builder.Property(x => x.Name);
            builder.HasMany(x => x.Products).WithOne(y => y.ProductCategory).HasForeignKey(y => y.CategoryId);
        }
    }
}
