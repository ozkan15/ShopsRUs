using ShopsRUs.Enums;

namespace ShopsRUs.Model
{
    public abstract class ShopUser : EntitiyBase<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public DateTime RegistrationDate { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public UserType UserType { get; set; }
    }
}
