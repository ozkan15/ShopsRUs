namespace ShopsRUs.DTO
{
    public class BillDetailModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}
