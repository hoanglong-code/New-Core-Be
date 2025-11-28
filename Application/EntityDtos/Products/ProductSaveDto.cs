using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EntityDtos.Products
{
    public class ProductSaveDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
        public string? Note { get; set; }
        public int? BrandId { get; set; }
        public decimal Price { get; set; }
    }
}

