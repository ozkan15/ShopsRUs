namespace ShopsRUs.Model
{
    public class InvoiceDetail : EntitiyBase<int>
    {
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual Product Product { get; set; }
    }
}
