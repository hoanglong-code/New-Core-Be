using Application.EntityDtos;
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
    public class GetProductByIdQuery : IRequestHandler<GetProductByIdCommand, Product>
    {
        private readonly IProductService _service;
        public GetProductByIdQuery(IProductService service)
        {
            _service = service;
        }
        public async Task<Product> Handle(GetProductByIdCommand request, CancellationToken cancellationToken)
        {
            return await _service.GetById(request.Id);
        }
    }
}
