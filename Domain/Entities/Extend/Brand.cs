using Domain.Entities.Base;

namespace Domain.Entities.Extend
{
    public class Brand : BaseEntity<int>
    {
        public required string Name { get; set; }
        public required string Code { get; set; }
        public string? Note { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        public Brand()
        {
            Products = new List<Product>();
        }
    }
}
