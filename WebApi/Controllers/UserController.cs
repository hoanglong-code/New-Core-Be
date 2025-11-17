using Domain.Enums;
using Infrastructure.CustomAuthorize;
using Infrastructure.Features.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private const string FunctionCode = "QLND";
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetByPage")]
        [CustomAuthorize(FunctionCode, ConstantEnums.TypeAction.READ)]
        public async Task<IActionResult> GetByPage([FromBody] GetUserByPageCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("GetById")]
        [CustomAuthorize(FunctionCode, ConstantEnums.TypeAction.VIEW)]
        public async Task<IActionResult> GetById(GetUserByIdCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("CreateData")]
        [CustomAuthorize(FunctionCode, ConstantEnums.TypeAction.CREATE)]
        public async Task<IActionResult> CreateData([FromBody] SaveUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("UpdateData")]
        [CustomAuthorize(FunctionCode, ConstantEnums.TypeAction.UPDATE)]
        public async Task<IActionResult> UpdateData([FromBody] SaveUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("DeleteData")]
        [CustomAuthorize(FunctionCode, ConstantEnums.TypeAction.DELETED)]
        public async Task<IActionResult> DeleteData(DeleteUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("DeleteMultiple")]
        [CustomAuthorize(FunctionCode, ConstantEnums.TypeAction.DELETED)]
        public async Task<IActionResult> DeleteMultipleData(DeleteMultipleUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}

