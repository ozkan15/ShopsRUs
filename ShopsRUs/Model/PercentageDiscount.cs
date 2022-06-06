using ShopsRUs.Enums;

namespace ShopsRUs.Model
{
    public class PercentageDiscount : Discount
    {
        public double DiscountPercentage { get; set; }
        public UserType UserType { get; set; }
        public virtual ICollection<PercentageDiscountExcludedProductCategories> ExcludedProductCategories { get; set; }
        public int RequiredYearsForUser { get; set; }
    }
}
