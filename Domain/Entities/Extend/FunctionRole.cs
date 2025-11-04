using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Extend
{
    public class FunctionRole : BaseEntity<int>
    {
        public int RoleId { get; set; }
        public int FunctionId { get; set; }
        public required string ActiveKey { get; set; }
        public virtual Role? Role { get; set; }
        public virtual Function? Function { get; set; }
    }
}
