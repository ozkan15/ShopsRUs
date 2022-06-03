namespace ShopsRUs.Model
{
    public class Product : EntitiyBase<int>
    {
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public int CategoryId { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
