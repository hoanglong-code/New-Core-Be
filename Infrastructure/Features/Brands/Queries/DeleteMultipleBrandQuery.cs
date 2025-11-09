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
    public class DeleteMultipleBrandQuery : IRequestHandler<DeleteMultipleBrandCommand, List<Brand>>
    {
        private readonly IBrandService _service;
        public DeleteMultipleBrandQuery(IBrandService service)
        {
            _service = service;
        }
        public async Task<List<Brand>> Handle(DeleteMultipleBrandCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeleteMultipleData(request.Ids);
        }
    }
}

