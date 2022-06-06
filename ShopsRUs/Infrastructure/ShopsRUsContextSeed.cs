using ShopsRUs.Enums;
using ShopsRUs.Model;
using Microsoft.EntityFrameworkCore;

namespace ShopsRUs.Infrastructure
{
    public class ShopsRUsContextSeed
    {
        public static IEnumerable<Discount> GetDiscounts()
        {
            string csvItems = Path.Combine(Directory.GetCurrentDirectory(), "Setup", "Discounts.csv");
            return File.ReadAllLines(csvItems)
                .Skip(1)
                .Select<string, Discount>(s =>
                 {
                     try
                     {
                         var columns = s.Split(',');

                         if ((DiscountType)int.Parse(columns[2]) == DiscountType.Percentage)
                         {
                             return new PercentageDiscount
                             {
                                 Id = int.Parse(columns[0]),
                                 Name = columns[1],
                                 DiscountType = DiscountType.Percentage,
                                 DiscountPercentage = int.Parse(columns[3]),
                                 UserType = (UserType)int.Parse(columns[6]),
                                 RequiredYearsForUser = int.Parse(columns[7]),
                             };
                         }
                         else
                         {
                             return new AmountDiscount
                             {
                                 Id = int.Parse(columns[0]),
                                 Name = columns[1],
                                 DiscountAmount = int.Parse(columns[4]),
                                 DiscountableAmount = decimal.Parse(columns[5]),
                                 DiscountType = DiscountType.AmountBased
                             };
                         }
                     }
                     catch
                     {
                         return null;
                     }
                 })
                .Where(s => s != null);
        }

        public static IEnumerable<PercentageDiscountExcludedProductCategories> GetExcludedCategories()
        {
            string csvItems = Path.Combine(Directory.GetCurrentDirectory(), "Setup", "ExcludedProductCategoriesForDiscount.csv");

            return File.ReadAllLines(csvItems)
                .Skip(1)
                .Select(s =>
                {
                    try
                    {
                        var columns = s.Split(',');

                        return new PercentageDiscountExcludedProductCategories
                        {
                            Id = int.Parse(columns[0]),
                            ProductCategoryId = int.Parse(columns[1]),
                            DiscountId = int.Parse(columns[2])
                        };
                    }
                    catch
                    {
                        return null;
                    }
                })
                .Where(s => s != null);
        }

        public static IEnumerable<ProductCategory> GetProductCategories()
        {
            string csvItems = Path.Combine(Directory.GetCurrentDirectory(), "Setup", "ProductCategories.csv");
            return File.ReadAllLines(csvItems)
                .Skip(1)
                .Select(s =>
                {
                    try
                    {
                        var columns = s.Split(',');

                        return new ProductCategory
                        {
                            Id = int.Parse(columns[0]),
                            Name = columns[1]
                        };
                    }
                    catch
                    {
                        return null;
                    }
                })
                .Where(s => s != null);
        }

        public static IEnumerable<Product> GetProducts()
        {
            string csvItems = Path.Combine(Directory.GetCurrentDirectory(), "Setup", "Products.csv");
            return File.ReadAllLines(csvItems)
                .Skip(1)
                .Select(s =>
                {
                    try
                    {
                        var columns = s.Split(',');

                        return new Product
                        {
                            Id = int.Parse(columns[0]),
                            ItemName = columns[1],
                            Price = decimal.Parse(columns[2]),
                            CategoryId = int.Parse(columns[3]),
                        };
                    }
                    catch
                    {
                        return null;
                    }
                })
                .Where(s => s != null);
        }

        public static IEnumerable<ShopUser> GetUsers()
        {
            string csvItems = Path.Combine(Directory.GetCurrentDirectory(), "Setup", "Users.csv");

            return File.ReadAllLines(csvItems)
                .Skip(1)
                .Select<string, ShopUser>(s =>
                {
                    try
                    {
                        var columns = s.Split(',');

                        if ((UserType)int.Parse(columns[4]) == UserType.Employee)
                        {
                            return new Employee
                            {
                                Id = int.Parse(columns[0]),
                                Name = columns[1],
                                Surname = columns[2],
                                Address = columns[3],
                                RegistrationDate = DateTime.Parse(columns[5]),
                                Title = columns[7],
                                UserType = UserType.Employee,
                            };
                        }
                        else if ((UserType)int.Parse(columns[4]) == UserType.Affiliate)
                        {
                            return new Affiliate
                            {
                                Id = int.Parse(columns[0]),
                                Name = columns[1],
                                Surname = columns[2],
                                Address = columns[3],
                                RegistrationDate = DateTime.Parse(columns[5]),
                                AffiliateName = columns[6],
                                UserType = UserType.Affiliate
                            };
                        }
                        else
                        {
                            return new Customer
                            {
                                Id = int.Parse(columns[0]),
                                Name = columns[1],
                                Surname = columns[2],
                                Address = columns[3],
                                RegistrationDate = DateTime.Parse(columns[5]),
                                UserType = UserType.Customer
                            };
                        }
                    }
                    catch
                    {
                        return null;
                    }
                })
                .Where(s => s != null);
        }
    }
}
