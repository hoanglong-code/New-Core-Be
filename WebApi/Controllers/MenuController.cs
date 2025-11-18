using Domain.Commons;
using Infrastructure.Features.Menus.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MenuController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MenuController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetMenu")]
        public async Task<IActionResult> GetMenu(GetMenuCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}


