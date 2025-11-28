using Domain.Entities.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enums.ConstantEnums;

namespace Application.EntityDtos.Roles
{
    public class RoleSaveDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
        public TypeRole Type { get; set; }
        public string? Note { get; set; }
        public ICollection<FunctionRole>? FunctionRole { get; set; }
    }
}

