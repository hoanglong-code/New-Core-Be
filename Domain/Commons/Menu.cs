using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commons
{
    public class Menu
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int? FunctionParentId { get; set; }
        public string? Url { get; set; }
        public int? Location { get; set; }
        public string? Icon { get; set; }
        public required string ActiveKey { get; set; }
        public List<Menu>? Childrens { get; set; }
    }
}
