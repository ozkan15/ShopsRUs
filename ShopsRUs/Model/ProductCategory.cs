namespace ShopsRUs.Model
{
    public class ProductCategory : EntitiyBase<int>
    {
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<PercentageDiscountExcludedProductCategories> ExcludedDiscounts { get; set; }
    }
}
