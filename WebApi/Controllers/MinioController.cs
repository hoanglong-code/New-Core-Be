using Domain.Enums;
using Infrastructure.CustomAuthorize;
using Infrastructure.Features.MinIO.Commands;
using Infrastructure.Features.Products.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinioController : ControllerBase
    {
        private const string FunctionCode = "QLFTL";
        private readonly IMediator _mediator;

        public MinioController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("GetBucketByPage")]
        [CustomAuthorize(FunctionCode, ConstantEnums.TypeAction.READ)]
        public async Task<IActionResult> GetByPage(GetBucketByPageCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("CreateBucket")]
        [CustomAuthorize(FunctionCode, ConstantEnums.TypeAction.CREATE)]
        public async Task<IActionResult> CreateBucket(CreateBucketCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("DeleteBucket")]
        [CustomAuthorize(FunctionCode, ConstantEnums.TypeAction.DELETED)]
        public async Task<IActionResult> DeleteBucket(DeleteBucketCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("GetObjectByPage")]
        [CustomAuthorize(FunctionCode, ConstantEnums.TypeAction.READ)]
        public async Task<IActionResult> GetObjectByPage(GetObjectsByPageCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("GetObject")]
        [CustomAuthorize(FunctionCode, ConstantEnums.TypeAction.VIEW)]
        public async Task<IActionResult> GetObject(GetObjectCommand command)
        {
            var result = await _mediator.Send(command);
            return File(result.Stream, result.ContentType, result.FileName);
        }

        [HttpPost("GetPresignedObjectUrl")]
        [CustomAuthorize(FunctionCode, ConstantEnums.TypeAction.VIEW)]
        public async Task<IActionResult> GetPresignedObjectUrl(GetPresignedObjectUrlCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("UploadObject")]
        [CustomAuthorize(FunctionCode, ConstantEnums.TypeAction.CREATE)]
        public async Task<IActionResult> UploadObject([FromForm] IFormFileCollection formFiles, [FromForm] string bucketName, [FromForm] string? prefix)
        {
            var command = new UploadObjectCommand
            {
                FormFiles = formFiles,
                BucketName = bucketName,
                Prefix = prefix
            };
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("UploadObjectWithPath")]
        [CustomAuthorize(FunctionCode, ConstantEnums.TypeAction.CREATE)]
        public async Task<IActionResult> UploadObjectWithPath([FromForm] IFormFileCollection formFiles, [FromForm] List<string> relativePaths, [FromForm] string bucketName, [FromForm] string? prefix)
        {
            var command = new UploadObjectWithPathCommand
            {
                FormFiles = formFiles,
                RelativePaths = relativePaths,
                BucketName = bucketName,
                Prefix = prefix
            };
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("DeleteObject")]
        [CustomAuthorize(FunctionCode, ConstantEnums.TypeAction.DELETED)]
        public async Task<IActionResult> DeleteObject(DeleteObjectCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("CopyObject")]
        [CustomAuthorize(FunctionCode, ConstantEnums.TypeAction.CREATE)]
        public async Task<IActionResult> CopyObject(CopyObjectCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
