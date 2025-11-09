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
    public class GetBrandByIdQuery : IRequestHandler<GetBrandByIdCommand, Brand>
    {
        private readonly IBrandService _service;
        public GetBrandByIdQuery(IBrandService service)
        {
            _service = service;
        }
        public async Task<Brand> Handle(GetBrandByIdCommand request, CancellationToken cancellationToken)
        {
            return await _service.GetById(request.Id);
        }
    }
}

