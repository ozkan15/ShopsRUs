namespace ShopsRUs.Model
{
    public class PercentageDiscountExcludedProductCategories : EntitiyBase<int>
    {
        public int DiscountId { get; set; }
        public int ProductCategoryId { get; set; }
        public virtual PercentageDiscount PercentageDiscount { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
    }
}
