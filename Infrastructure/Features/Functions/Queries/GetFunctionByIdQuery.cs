using Application.EntityDtos;
using Application.EntityDtos.Functions;
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
    public class GetFunctionByIdQuery : IRequestHandler<GetFunctionByIdCommand, FunctionDetailDto>
    {
        private readonly IFunctionService _service;
        public GetFunctionByIdQuery(IFunctionService service)
        {
            _service = service;
        }
        public async Task<FunctionDetailDto> Handle(GetFunctionByIdCommand request, CancellationToken cancellationToken)
        {
            return await _service.GetById(request.Id);
        }
    }
}

