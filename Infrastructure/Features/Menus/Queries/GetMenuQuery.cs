using Application.Contexts.Abstractions;
using Domain.Commons;
using Domain.Enums;
using Infrastructure.Configurations;
using Infrastructure.Features.Menus.Commands;
using Infrastructure.Services.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Features.Menus.Queries
{
    public class GetMenuQuery : IRequestHandler<GetMenuCommand, List<Menu>>
    {
        private readonly IUserContext _userContext;
        private readonly IMenuService _service;

        public GetMenuQuery(IUserContext userContext, IMenuService service)
        {
            _userContext = userContext;
            _service = service;
        }

        public async Task<List<Menu>> Handle(GetMenuCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContext.userClaims.userId;
            if (userId <= 0)
            {
                return new List<Menu>();
            }
            return await _service.GetMenu(userId);
        }
    }
}

