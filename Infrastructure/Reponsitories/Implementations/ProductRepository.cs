using Application.Contexts.Abstractions;
using Application.IReponsitories.Abstractions;
using Domain.Entities.Extend;
using Infrastructure.Configurations;
using Infrastructure.Reponsitories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reponsitories.Implementations
{
    public class ProductRepository : AsyncGenericRepository<Product, int>, IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly IUserContext _userContext;
        public ProductRepository(AppDbContext context, IUserContext userContext) : base(context, userContext)
        {
            _context = context;
            _userContext = userContext;
        }
    }
}
