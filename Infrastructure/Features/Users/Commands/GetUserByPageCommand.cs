using Application.EntityDtos.Users;
using Infrastructure.Commons;
using MediatR;

namespace Infrastructure.Features.Users.Commands
{
    public class GetUserByPageCommand : IRequest<BaseSearchResponse<UserDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 20;
        public string? Sorts { get; set; } = null;
        public string? Orders { get; set; } = string.Empty;
        public string? QueryString { get; set; } = string.Empty;
    }
}

