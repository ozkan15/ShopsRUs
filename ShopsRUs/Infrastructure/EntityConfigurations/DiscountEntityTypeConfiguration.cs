using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopsRUs.Enums;
using ShopsRUs.Model;

namespace ShopsRUs.Infrastructure.EntityConfigurations
{
    public class DiscountEntityTypeConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {

            builder.Property(x => x.Name);
            builder.HasMany(src => src.Invoices).WithMany(dest => dest.Discounts);

            builder.HasDiscriminator(e => e.DiscountType)
                .HasValue<PercentageDiscount>(DiscountType.Percentage)
                .HasValue<AmountDiscount>(DiscountType.AmountBased);
        }
    }

    public class PercentageDiscountEntityTypeConfiguration : IEntityTypeConfiguration<PercentageDiscount>
    {
        public void Configure(EntityTypeBuilder<PercentageDiscount> builder)
        {
            builder.HasData(ShopsRUsContextSeed.GetDiscounts().Where(s => s.DiscountType == DiscountType.Percentage));
            builder.Property(x => x.DiscountPercentage);
            builder.Property(x => x.UserType);
            builder.Property(x => x.RequiredYearsForUser);
        }
    }

    public class AmountDiscountEntityTypeConfiguration : IEntityTypeConfiguration<AmountDiscount>
    {
        public void Configure(EntityTypeBuilder<AmountDiscount> builder)
        {
            builder.HasData(ShopsRUsContextSeed.GetDiscounts().Where(s => s.DiscountType == DiscountType.AmountBased));
            builder.Property(x => x.DiscountAmount);
            builder.Property(x => x.DiscountableAmount);
        }
    }

    public class PercentageDiscountExcludedProductCategoriesEntityTypeConfiguration : IEntityTypeConfiguration<PercentageDiscountExcludedProductCategories>
    {
        public void Configure(EntityTypeBuilder<PercentageDiscountExcludedProductCategories> builder)
        {
            builder.HasData(ShopsRUsContextSeed.GetExcludedCategories());
            builder.HasOne(src => src.ProductCategory).WithMany(dest => dest.ExcludedDiscounts).HasForeignKey(dest => dest.ProductCategoryId);
            builder.HasOne(src => src.PercentageDiscount).WithMany(dest => dest.ExcludedProductCategories).HasForeignKey(dest => dest.DiscountId);
        }
    }
}
