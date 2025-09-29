using Application.EntityDtos;
using AutoMapper;
using Domain.Entities.Extend;
using Infrastructure.Features.Products.Commands;
using Infrastructure.Services.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features.Products.Queries
{
    public class SaveProductQuery : IRequestHandler<SaveProductCommand, Product>
    {
        private readonly IProductService _service;
        private readonly IMapper _mapper;
        public SaveProductQuery(IProductService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public async Task<Product> Handle(SaveProductCommand request, CancellationToken cancellationToken)
        {
            Product product = _mapper.Map<Product>(request);
            return await _service.SaveData(product);
        }
    }
}
