using Domain.Entities.Base;

namespace Domain.Entities.Extend
{
    public class Product : BaseEntity<int>
    {
        public required string Name { get; set; }
        public required string Code { get; set; }
        public string? Note { get; set; }
        public int? BrandId { get; set; }
        public decimal Price { get; set; }
        public virtual Brand? Brand { get; set; }
    }
}
