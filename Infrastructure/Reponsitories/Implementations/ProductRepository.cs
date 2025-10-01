using Application.Contexts.Abstractions;
using Application.IReponsitories.Abstractions;
using Domain.Entities.Extend;
using Elastic.Clients.Elasticsearch;
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
        private readonly ElasticsearchClient _elasticClient;
        public ProductRepository(AppDbContext context, IUserContext userContext, ElasticsearchClient elasticsearchClient) : base(context, userContext, elasticsearchClient)
        {
            _context = context;
            _userContext = userContext;
            _elasticClient = elasticsearchClient;
        }
    }
}
