using Infrastructure.Features.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TokenController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Auth")]
        [AllowAnonymous]
        public async Task<IActionResult> Auth(LoginCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
