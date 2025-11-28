using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EntityDtos.Functions
{
    public class FunctionSaveDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
        public int? FunctionParentId { get; set; }
        public string? Url { get; set; }
        public string? Note { get; set; }
        public int? Location { get; set; }
        public string? Icon { get; set; }
    }
}

