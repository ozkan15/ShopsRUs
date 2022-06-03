using ShopsRUs.DTO;

namespace ShopsRUs.ViewModel
{
    public class BillModel
    {
        public UserModel User { get; set; }
        public List<BillDetailModel> BillDetails { get; set; }

        public List<DiscountModel> Discounts { get; set; }

        public decimal AmountTotal { get; set; }

        public decimal DiscountTotal { get; set; }
        public decimal AmountNet { get; set; }
    }
}
