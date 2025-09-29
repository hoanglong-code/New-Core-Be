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
    public class DeleteProductQuery : IRequestHandler<DeleteProductCommand, Product>
    {
        private readonly IProductService _service;
        public DeleteProductQuery(IProductService service)
        {
            _service = service;
        }
        public async Task<Product> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeleteData(request.Id);
        }
    }
}
