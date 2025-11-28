using Application.EntityDtos.Users;
using AutoMapper;
using Domain.Commons;
using Infrastructure.Commons;
using Infrastructure.Features.Users.Commands;
using Infrastructure.Services.Abstractions;
using MediatR;

namespace Infrastructure.Features.Users.Queries
{
    public class GetUserByPageQuery : IRequestHandler<GetUserByPageCommand, BaseSearchResponse<UserGridDto>>
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;
        public GetUserByPageQuery(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public async Task<BaseSearchResponse<UserGridDto>> Handle(GetUserByPageCommand request, CancellationToken cancellationToken)
        {
            BaseCriteria baseCriteria = _mapper.Map<BaseCriteria>(request);
            return await _service.GetByPage(baseCriteria);
        }
    }
}

