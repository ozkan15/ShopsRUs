using ShopsRUs.Enums;

namespace ShopsRUs.Model
{
    public abstract class Discount : EntitiyBase<int>
    {
        public string Name { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public DiscountType DiscountType { get; set; }
    }
}
