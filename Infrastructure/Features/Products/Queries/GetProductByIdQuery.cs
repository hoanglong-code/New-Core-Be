using Application.EntityDtos;
using Application.EntityDtos.Products;
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
    public class GetProductByIdQuery : IRequestHandler<GetProductByIdCommand, ProductDetailDto>
    {
        private readonly IProductService _service;
        public GetProductByIdQuery(IProductService service)
        {
            _service = service;
        }
        public async Task<ProductDetailDto> Handle(GetProductByIdCommand request, CancellationToken cancellationToken)
        {
            return await _service.GetById(request.Id);
        }
    }
}
