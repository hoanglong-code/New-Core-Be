using Domain.Enums;
using Infrastructure.CustomAuthorize;
using Infrastructure.Features.Products.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private const string FunctionCode = "QLSP";
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetByPage")]
        [CustomAuthorize(FunctionCode, ConstantEnums.TypeAction.VIEW)]
        public async Task<IActionResult> GetByPage([FromBody] GetProductByPageCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("GetById")]
        [CustomAuthorize(FunctionCode, ConstantEnums.TypeAction.VIEW)]
        public async Task<IActionResult> GetById(GetProductByIdCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("CreateData")]
        [CustomAuthorize(FunctionCode, ConstantEnums.TypeAction.CREATE)]
        public async Task<IActionResult> CreateData([FromBody] SaveProductCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("UpdateData")]
        [CustomAuthorize(FunctionCode, ConstantEnums.TypeAction.UPDATE)]
        public async Task<IActionResult> UpdateData([FromBody] SaveProductCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("DeleteData")]
        [CustomAuthorize(FunctionCode, ConstantEnums.TypeAction.DELETED)]
        public async Task<IActionResult> DeleteData(DeleteProductCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("DeleteMultiple")]
        [CustomAuthorize(FunctionCode, ConstantEnums.TypeAction.DELETED)]
        public async Task<IActionResult> DeleteMultipleData(DeleteMultipleProductCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
