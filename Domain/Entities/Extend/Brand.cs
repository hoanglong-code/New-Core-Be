using Domain.Entities.Base;

namespace Domain.Entities.Extend
{
    public class Brand : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string? Note { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        public Brand()
        {
            Products = new List<Product>();
        }
    }
}
