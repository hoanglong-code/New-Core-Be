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

        /// <summary>
        /// Lấy danh sách menu của người dùng hiện tại
        /// </summary>
        [HttpGet("GetCurrentUserMenu")]
        public async Task<ActionResult<List<Menu>>> GetCurrentUserMenu()
        {
            var result = await _mediator.Send(new GetMenuCommand());
            return Ok(result);
        }
    }
}


