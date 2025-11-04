using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enums.ConstantEnums;

namespace Domain.Entities.Extend
{
    public class Role : BaseEntity<int>
    {
        public required string Name { get; set; }
        public required string Code { get; set; }
        public TypeRole Type { get; set; }
        public string? Note { get; set; }
    }
}
