using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopsRUs.DTO;
using ShopsRUs.Infrastructure;
using ShopsRUs.Infrastructure.Exceptions;
using ShopsRUs.Model;
using ShopsRUs.ViewModel;

namespace ShopsRUs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly ShopsRUsContext _shopsRUsContext;

        public DiscountController(ShopsRUsContext shopsRUsContext)
        {
            _shopsRUsContext = shopsRUsContext;
        }

        [HttpPost(Name = "CalculateDiscount")]
        public ActionResult<BillModel> Get(BillInputModel bill)
        {
            var user = _shopsRUsContext.Set<ShopUser>().FirstOrDefault(s => s.Id == bill.UserId);

            if (user == null) return NotFound("user not found");

            if ((bill.BillDetails?.Count ?? 0) == 0) return NotFound("products not found");

            if (bill.BillDetails.Any(s => s.Quantity <= 0)) return BadRequest("product quantities must be bigger than 0");

            var productIdList = bill.BillDetails.Select(s => s.ProductId).ToList();
            var quantities = bill.BillDetails.ToDictionary(s => s.ProductId);

            var outputModel = new BillModel
            {
                BillDetails = bill.BillDetails.Select(bd => new BillDetailModel { ProductId = bd.ProductId, Quantity = bd.Quantity }).ToList(),
                User = new UserModel
                {
                    Id = user.Id,
                    FullName = $"{user.Name} {user.Surname}"
                },
                Discounts = new List<DiscountModel>()
            };


            var amountTotal = _shopsRUsContext.Products
                .Where(s => productIdList.Contains(s.Id))
                .ToList()
                .Aggregate(0m, (acc, next) => acc + next.Price * quantities[next.Id].Quantity);
            outputModel.AmountTotal = amountTotal;

            var percentageDiscount = _shopsRUsContext.PercentageDiscounts.FirstOrDefault(s => s.UserType == user.UserType);
            var excludedProductCategories = percentageDiscount.ExcludedProductCategories.Select(epc => epc.ProductCategoryId);
            var percentageDiscountAmount = 0m;

            try
            {
                outputModel.BillDetails.ForEach(s =>
                {
                    var dbProduct = _shopsRUsContext.Products.FirstOrDefault(pr => pr.Id == s.ProductId);

                    if (dbProduct == null) throw new ItemNotFoundException($"product with Id {s.ProductId} not found");

                    if (!excludedProductCategories.Contains(dbProduct.CategoryId))
                    {
                        s.UnitPrice = dbProduct.Price;
                        s.Amount = dbProduct.Price * s.Quantity;
                        s.ProductName = dbProduct.ItemName;
                        s.DiscountAmount = (dbProduct.Price * s.Quantity) * (decimal)percentageDiscount.DiscountPercentage / 100;
                        percentageDiscountAmount += s.DiscountAmount;
                    }
                });
            }
            catch (ItemNotFoundException ex)
            {
                return NotFound(ex.Message);
            }


            outputModel.Discounts.Add(new DiscountModel { DiscountAmount = percentageDiscountAmount, DiscountName = percentageDiscount.Name });

            var amountBasedDiscount = _shopsRUsContext.AmountDiscounts.First();
            var amountBasedDiscountAmount = ((int)amountTotal / (int)amountBasedDiscount.DiscountableAmount) * amountBasedDiscount.DiscountAmount;

            outputModel.Discounts.Add(new DiscountModel { DiscountAmount = amountBasedDiscountAmount, DiscountName = amountBasedDiscount.Name });

            outputModel.DiscountTotal = percentageDiscountAmount + amountBasedDiscountAmount;
            outputModel.AmountNet = outputModel.AmountTotal - outputModel.DiscountTotal;

            return Ok(outputModel);
        }
    }
}
