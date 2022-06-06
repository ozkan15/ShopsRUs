using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopsRUs.DTO;
using ShopsRUs.Infrastructure;
using ShopsRUs.Infrastructure.Exceptions;
using ShopsRUs.Model;
using ShopsRUs.Services;
using ShopsRUs.ViewModel;

namespace ShopsRUs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly ShopsRUsContext _shopsRUsContext;
        private readonly IDiscountService _discountService;

        public DiscountController(ShopsRUsContext shopsRUsContext, IDiscountService discountService)
        {
            _shopsRUsContext = shopsRUsContext;
            _discountService = discountService;
        }

        [HttpPost(Name = "CalculateDiscount")]
        public async Task<ActionResult<BillModel>> Post(BillInputModel bill)
        {
            var user = _shopsRUsContext.Set<ShopUser>().FirstOrDefault(s => s.Id == bill.UserId);

            if (user == null) return BadRequest("user not found");

            if ((bill.BillDetails?.Count ?? 0) == 0) return BadRequest("product list cant be empty");

            if (bill.BillDetails.Any(s => s.Quantity <= 0)) return BadRequest("product quantities must be bigger than 0");

            var productIdList = bill.BillDetails.Select(s => s.ProductId).ToList();

            var productSet = new HashSet<int>();
            foreach (var productId in productIdList)
            {
                if (productSet.Contains(productId)) return BadRequest("duplicate product found");

                productSet.Add(productId);
            }

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

            try
            {
                _discountService.GetPercentageBasedDiscount(user, outputModel);
            }
            catch (ItemNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }

            _discountService.GetAmountBasedDiscount(outputModel);

            outputModel.AmountNet = outputModel.AmountTotal - outputModel.DiscountTotal;


            var isSuccessful = await _discountService.SaveUserInvoice(user, outputModel);

            if (!isSuccessful) return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(outputModel);
        }
    }
}
