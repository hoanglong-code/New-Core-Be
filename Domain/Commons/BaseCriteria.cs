using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commons
{
    public class BaseCriteria
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 20;
        public string? Sorts { get; set; } = null;
        public string? Orders { get; set; } = string.Empty;
        public string? QueryString { get; set; } = string.Empty;
    }
    public class BaseCriteriaWithDate : BaseCriteria
    {
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
    }
}
