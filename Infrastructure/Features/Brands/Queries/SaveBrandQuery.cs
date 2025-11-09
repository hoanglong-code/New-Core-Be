using Application.EntityDtos;
using AutoMapper;
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
    public class SaveBrandQuery : IRequestHandler<SaveBrandCommand, Brand>
    {
        private readonly IBrandService _service;
        private readonly IMapper _mapper;
        public SaveBrandQuery(IBrandService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public async Task<Brand> Handle(SaveBrandCommand request, CancellationToken cancellationToken)
        {
            Brand brand = _mapper.Map<Brand>(request);
            return await _service.SaveData(brand);
        }
    }
}

