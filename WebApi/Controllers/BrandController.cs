using Domain.Enums;
using Infrastructure.CustomAuthorize;
using Infrastructure.Features.Brands.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BrandController : ControllerBase
    {
        private const string FunctionCode = "QLBR";
        private readonly IMediator _mediator;

        public BrandController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetByPage")]
        [CustomAuthorize(FunctionCode, ConstantEnums.TypeAction.VIEW)]
        public async Task<IActionResult> GetByPage([FromBody] GetBrandByPageCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("GetById")]
        [CustomAuthorize(FunctionCode, ConstantEnums.TypeAction.VIEW)]
        public async Task<IActionResult> GetById(GetBrandByIdCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("CreateData")]
        [CustomAuthorize(FunctionCode, ConstantEnums.TypeAction.CREATE)]
        public async Task<IActionResult> CreateData([FromBody] SaveBrandCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("UpdateData")]
        [CustomAuthorize(FunctionCode, ConstantEnums.TypeAction.UPDATE)]
        public async Task<IActionResult> UpdateData([FromBody] SaveBrandCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("DeleteData")]
        [CustomAuthorize(FunctionCode, ConstantEnums.TypeAction.DELETED)]
        public async Task<IActionResult> DeleteData(DeleteBrandCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("DeleteMultiple")]
        [CustomAuthorize(FunctionCode, ConstantEnums.TypeAction.DELETED)]
        public async Task<IActionResult> DeleteMultipleData(DeleteMultipleBrandCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}

