using Application.IReponsitories.Base;
using Domain.Entities.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IReponsitories.Abstractions
{
    public interface IFunctionRepository : IAsyncGenericRepository<Function, int>
    {
    }
}
