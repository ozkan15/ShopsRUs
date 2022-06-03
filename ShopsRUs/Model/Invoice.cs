namespace ShopsRUs.Model
{
    public class Invoice : EntitiyBase<int>
    {
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public decimal AmountNet { get; set; }
        public decimal AmoutTotal { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual ICollection<Discount> Discounts { get; set; }
        public virtual ShopUser User { get; set; }
    }
}
