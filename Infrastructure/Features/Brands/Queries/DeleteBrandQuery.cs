using Application.EntityDtos;
using Domain.Entities.Extend;
using Infrastructure.Features.Brands.Commands;
using Infrastructure.Services.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features.Brands.Queries
{
    public class DeleteBrandQuery : IRequestHandler<DeleteBrandCommand, Brand>
    {
        private readonly IBrandService _service;
        public DeleteBrandQuery(IBrandService service)
        {
            _service = service;
        }
        public async Task<Brand> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeleteData(request.Id);
        }
    }
}

