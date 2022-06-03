using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ShopsRUs.Infrastructure.EntityConfigurations;
using ShopsRUs.Model;

namespace ShopsRUs.Infrastructure
{
    public class ShopsRUsContext : DbContext
    {
        public ShopsRUsContext(DbContextOptions<ShopsRUsContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Affiliate> Affiliates { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<PercentageDiscount> PercentageDiscounts { get; set; }
        public DbSet<AmountDiscount> AmountDiscounts { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DiscountEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PercentageDiscountEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AmountDiscountEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceDetailEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductCategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AffiliateEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PercentageDiscountExcludedProductCategoriesEntityTypeConfiguration());
        }
    }


    public class ShopsRUsContextDesignFactory : IDesignTimeDbContextFactory<ShopsRUsContext>
    {
        public ShopsRUsContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            var optionsbuilder = new DbContextOptionsBuilder<ShopsRUsContext>();

            optionsbuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"), sqlServerOptionsAction: o => o.MigrationsAssembly("ShopsRUs"));

            return new ShopsRUsContext(optionsbuilder.Options);

        }
    }
}
