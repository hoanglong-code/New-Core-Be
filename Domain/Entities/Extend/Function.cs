using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Extend
{
    public class Function : BaseEntity<int>
    {
        public required string Name { get; set; }
        public required string Code { get; set; }
        public int FunctionParentId { get; set; }
        public string? Url { get; set; }
        public string? Note { get; set; }
        public int? Location { get; set; }
        public string? Icon { get; set; }
        public virtual Function? Parent { get; set; }
        public virtual ICollection<Function>? Children { get; set; }
        public virtual ICollection<FunctionRole>? FunctionRole { get; set; }
    }
}
