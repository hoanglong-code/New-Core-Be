using Domain.Enums;
using Infrastructure.CustomAuthorize;
using Infrastructure.Features.Roles.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleController : ControllerBase
    {
        private const string FunctionCode = "QLVT";
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetByPage")]
        [CustomAuthorize(FunctionCode, ConstantEnums.TypeAction.VIEW)]
        public async Task<IActionResult> GetByPage([FromBody] GetRoleByPageCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("GetById")]
        [CustomAuthorize(FunctionCode, ConstantEnums.TypeAction.VIEW)]
        public async Task<IActionResult> GetById(GetRoleByIdCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("CreateData")]
        [CustomAuthorize(FunctionCode, ConstantEnums.TypeAction.CREATE)]
        public async Task<IActionResult> CreateData([FromBody] SaveRoleCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("UpdateData")]
        [CustomAuthorize(FunctionCode, ConstantEnums.TypeAction.UPDATE)]
        public async Task<IActionResult> UpdateData([FromBody] SaveRoleCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("DeleteData")]
        [CustomAuthorize(FunctionCode, ConstantEnums.TypeAction.DELETED)]
        public async Task<IActionResult> DeleteData(DeleteRoleCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("DeleteMultiple")]
        [CustomAuthorize(FunctionCode, ConstantEnums.TypeAction.DELETED)]
        public async Task<IActionResult> DeleteMultipleData(DeleteMultipleRoleCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}

