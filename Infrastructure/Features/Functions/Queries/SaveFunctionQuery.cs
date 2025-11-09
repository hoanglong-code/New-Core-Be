using Application.EntityDtos;
using AutoMapper;
using Domain.Entities.Extend;
using Infrastructure.Features.Functions.Commands;
using Infrastructure.Services.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features.Functions.Queries
{
    public class SaveFunctionQuery : IRequestHandler<SaveFunctionCommand, Function>
    {
        private readonly IFunctionService _service;
        private readonly IMapper _mapper;
        public SaveFunctionQuery(IFunctionService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public async Task<Function> Handle(SaveFunctionCommand request, CancellationToken cancellationToken)
        {
            Function function = _mapper.Map<Function>(request);
            return await _service.SaveData(function);
        }
    }
}

