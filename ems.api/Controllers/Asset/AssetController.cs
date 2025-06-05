using ems.application.DTOs.Asset;
using ems.application.Features.AssetCmd.Commands;
using ems.application.Features.AssetCmd.Queries;
using ems.application.Interfaces.ILogger;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ems.api.Controllers.Asset
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILoggerService _logger;  // Logger service ka declaration

        public AssetController(IMediator mediator, ILoggerService logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpPost("updateasset")]
        // [Authorize]
        public async Task<IActionResult> UpdateAsset([FromBody] UpdateAssetDTO updateAssetDTO)
        {
            _logger.LogInfo("Received request for update a new Asset" + updateAssetDTO.ToString());
            var command = new UpdateAssetCommand(updateAssetDTO);
            var result = await _mediator.Send(command);
            if (!result.IsSucceeded)
            {
                return Ok(result);
            }
            return Ok(result);
        }

        [HttpPost("addasset")]
        // [Authorize]
        public async Task<IActionResult> CreateAsset([FromBody] CreateAssetDTO createAssetDTO)
        {
            _logger.LogInfo("Received request for create a new Asset" + createAssetDTO.ToString());
            var command = new CreateAssetCommand(createAssetDTO);
            var result = await _mediator.Send(command);
            if (!result.IsSucceeded)
            {
                return Ok(result);
            }
            return Ok(result);
        }
        [HttpGet("getallasset")]
        public async Task<IActionResult> GetAllAssets([FromQuery] AssetRequestDTO? AssetRequestDTO)
        {
            if (AssetRequestDTO == null)
            {
                // _logger.LogWarning("Received null request for getting Assets.");
                // return BadRequest(new ApiResponse<List<GetAllAssetDTO>>(false, "Invalid request", null));
            }

            // _logger.LogInformation("Received request to get Assets for userId: {LoginId}", AssetRequestDTO.Id);

            var query = new GetAllAssetQuery(AssetRequestDTO);  //  Fix: No parameter needed in GetAllAssetQuery
            var result = await _mediator.Send(query);

            if (!result.IsSucceeded)
            {
                return Unauthorized(result);
            }

            return Ok(result);
        }
    }

}

