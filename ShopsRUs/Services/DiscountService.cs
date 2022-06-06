using ShopsRUs.DTO;
using ShopsRUs.Infrastructure;
using ShopsRUs.Infrastructure.Exceptions;
using ShopsRUs.Model;
using ShopsRUs.ViewModel;

namespace ShopsRUs.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly ShopsRUsContext _shopsRUsContext;
        public DiscountService(ShopsRUsContext shopsRUsContext)
        {
            _shopsRUsContext = shopsRUsContext;
        }

        public void GetAmountBasedDiscount(BillModel outputModel)
        {
            var amountBasedDiscount = _shopsRUsContext.AmountDiscounts.First();
            var amountBasedDiscountAmount = ((int)outputModel.AmountTotal / (int)amountBasedDiscount.DiscountableAmount) * amountBasedDiscount.DiscountAmount;

            if (amountBasedDiscountAmount > 0)
            {
                outputModel.Discounts.Add(new DiscountModel
                {
                    DiscountId = amountBasedDiscount.Id,
                    DiscountAmount = amountBasedDiscountAmount,
                    DiscountName = amountBasedDiscount.Name
                });
                outputModel.DiscountTotal += amountBasedDiscountAmount;
            }
        }

        public void GetPercentageBasedDiscount(ShopUser user, BillModel outputModel)
        {
            var percentageDiscount = _shopsRUsContext.PercentageDiscounts.FirstOrDefault(s => s.UserType == user.UserType);
            var discountAvailibilityStartDate = DateTime.Now.AddYears(-1 * percentageDiscount.RequiredYearsForUser);

            var excludedProductCategories = percentageDiscount.ExcludedProductCategories.Select(epc => epc.ProductCategoryId);
            var percentageDiscountAmount = 0m;
            var discountApplied = false;

            outputModel.BillDetails.ForEach(s =>
            {
                var dbProduct = _shopsRUsContext.Products.FirstOrDefault(pr => pr.Id == s.ProductId);

                if (dbProduct == null) throw new ItemNotFoundException($"product with Id {s.ProductId} not found");

                s.UnitPrice = dbProduct.Price;
                s.Amount = dbProduct.Price * s.Quantity;
                s.ProductName = dbProduct.ItemName;

                if (!excludedProductCategories.Contains(dbProduct.CategoryId) && discountAvailibilityStartDate > user.RegistrationDate)
                {
                    s.DiscountAmount = (dbProduct.Price * s.Quantity) * (decimal)percentageDiscount.DiscountPercentage / 100;
                    percentageDiscountAmount += s.DiscountAmount;
                    discountApplied = true;
                }
            });

            if (discountApplied)
            {
                outputModel.Discounts.Add(new DiscountModel { DiscountId = percentageDiscount.Id, DiscountAmount = percentageDiscountAmount, DiscountName = percentageDiscount.Name });
                outputModel.DiscountTotal += percentageDiscountAmount;
            }
        }

        public async Task<bool> SaveUserInvoice(ShopUser user, BillModel outputModel)
        {
            var billDiscountIds = outputModel.Discounts.Select(s => s.DiscountId);
            var allDiscounts = _shopsRUsContext.Discounts.ToList();
            var billDiscounts = allDiscounts.Where(d => billDiscountIds.Contains(d.Id)).ToList();

            var invoice = await _shopsRUsContext.Invoices.AddAsync(new Invoice
            {
                AmoutTotal = outputModel.AmountTotal,
                AmountNet = outputModel.AmountNet,
                Date = DateTime.Now,
                User = user,
                Discounts = billDiscounts,
                InvoiceDetails = outputModel.BillDetails.Select(s => new InvoiceDetail
                {
                    Amount = s.Amount,
                    ProductId = s.ProductId,
                    Quantity = s.Quantity
                }).ToList()
            });

            var result = await _shopsRUsContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
