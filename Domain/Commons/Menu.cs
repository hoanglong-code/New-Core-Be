using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commons
{
    public class Menu
    {
        public int MenuId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int MenuParent { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public string ActiveKey { get; set; }
        public int? Status { get; set; }
        public bool? IsParamRoute { get; set; }
        public List<Menu>? ListMenus { get; set; }
    }
}
