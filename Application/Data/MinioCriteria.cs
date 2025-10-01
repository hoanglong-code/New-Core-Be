using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data
{
    public class MinioCriteria : BaseCriteria
    {
        public string BucketName { get; set; } = "";
        public string Prefix { get; set; } = "";
    }
}
