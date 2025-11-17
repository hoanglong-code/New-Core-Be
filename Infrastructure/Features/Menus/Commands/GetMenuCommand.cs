using Domain.Commons;
using MediatR;

namespace Infrastructure.Features.Menus.Commands
{
    public class GetMenuCommand : IRequest<List<Menu>>
    {
    }
}

