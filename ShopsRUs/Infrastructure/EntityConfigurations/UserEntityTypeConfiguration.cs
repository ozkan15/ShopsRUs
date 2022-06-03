using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopsRUs.Enums;
using ShopsRUs.Model;

namespace ShopsRUs.Infrastructure.EntityConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<ShopUser>
    {
        public void Configure(EntityTypeBuilder<ShopUser> builder)
        {

            builder.Property(x => x.Name)
                .HasMaxLength(100);
            builder.Property(x => x.Surname)
                .HasMaxLength(100);
            builder.Property(x => x.Address)
                .HasMaxLength(500);
            builder.Property(x => x.RegistrationDate);
            builder.HasMany(src => src.Invoices).WithOne(dest => dest.User).HasForeignKey(x => x.UserId).IsRequired();

            builder.HasDiscriminator(e => e.UserType)
                .HasValue<Customer>(UserType.Customer)
                .HasValue<Affiliate>(UserType.Affiliate)
                .HasValue<Employee>(UserType.Employee);
        }
    }

    public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasData(ShopsRUsContextSeed.GetUsers().Where(s => s.UserType == UserType.Customer));
        }
    }

    public class AffiliateEntityTypeConfiguration : IEntityTypeConfiguration<Affiliate>
    {
        public void Configure(EntityTypeBuilder<Affiliate> builder)
        {
            builder.HasData(ShopsRUsContextSeed.GetUsers().Where(s => s.UserType == UserType.Affiliate));
        }
    }

    public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasData(ShopsRUsContextSeed.GetUsers().Where(s => s.UserType == UserType.Employee));
        }
    }
}
