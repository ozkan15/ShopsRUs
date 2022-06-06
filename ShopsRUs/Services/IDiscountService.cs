using ShopsRUs.DTO;
using ShopsRUs.Model;
using ShopsRUs.ViewModel;

namespace ShopsRUs.Services
{
    public interface IDiscountService
    {
        Task<bool> SaveUserInvoice(ShopUser user, BillModel bill);
        /// <summary>
        /// Applies percentage based discount for invoice items
        /// </summary>
        /// <param name="user"></param>
        /// <param name="outputModel"></param>
        /// <exception cref="ItemNotFoundException"></exception>
        void GetPercentageBasedDiscount(ShopUser user, BillModel bill);

        /// <summary>
        /// Applies percentage based discount for the invoice 
        /// </summary>
        /// <param name="bill"></param>
        void GetAmountBasedDiscount(BillModel bill);
    }
}
