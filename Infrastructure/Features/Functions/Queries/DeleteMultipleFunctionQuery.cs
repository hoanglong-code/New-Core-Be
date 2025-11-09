using Application.EntityDtos;
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
    public class DeleteMultipleFunctionQuery : IRequestHandler<DeleteMultipleFunctionCommand, List<Function>>
    {
        private readonly IFunctionService _service;
        public DeleteMultipleFunctionQuery(IFunctionService service)
        {
            _service = service;
        }
        public async Task<List<Function>> Handle(DeleteMultipleFunctionCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeleteMultipleData(request.Ids);
        }
    }
}

