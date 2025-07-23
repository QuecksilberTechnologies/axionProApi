using ems.application.DTOs.Asset;
using ems.application.DTOs.Module;
using ems.application.Features.AssetCmd.Commands;
 
using ems.application.Features.ModuleCmd.Commands;
using ems.application.Interfaces.ILogger;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ems.api.Controllers.Module
{
    /// <summary>
    /// handled-module-related-operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ModuleController :ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILoggerService _logger;  // Logger service ka declaration

        public ModuleController(IMediator mediator, ILoggerService logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        #region Create Module

        /// <summary>
        /// create a new module.
        /// </summary>
        [HttpPost("create-module")]
        public async Task<IActionResult> AddModule([FromBody] CreateMainModuleRequestDTO? createModuleRequestDTO)
        {
            if (createModuleRequestDTO == null)
            {
                // _logger.LogWarning("Received null request for getting Assets.");
                // return BadRequest(new ApiResponse<List<GetAllAssetDTO>>(false, "Invalid request", null));
            }

            // _logger.LogInformation("Received request to get Assets for userId: {LoginId}", AssetRequestDTO.Id);

            var query = new CreateModuleCommand(createModuleRequestDTO);  //  Fix: No parameter needed in GetAllAssetQuery
            var result = await _mediator.Send(query);

            if (!result.IsSucceeded)
            {
                return Unauthorized(result);
            }

            return Ok(result);
        }
        [HttpPost("create-sub-module")]
        public async Task<IActionResult> AddSubModule([FromBody] CreateSubModuleRequestDTO? createSubModuleRequestDTO)
        {
            if (createSubModuleRequestDTO == null)
            {
                // _logger.LogWarning("Received null request for getting Assets.");
                // return BadRequest(new ApiResponse<List<GetAllAssetDTO>>(false, "Invalid request", null));
            }

            // _logger.LogInformation("Received request to get Assets for userId: {LoginId}", AssetRequestDTO.Id);

            var query = new CreateSubModuleCommand(createSubModuleRequestDTO);  //  Fix: No parameter needed in GetAllAssetQuery
            var result = await _mediator.Send(query);

            if (!result.IsSucceeded)
            {
                return Unauthorized(result);
            }

            return Ok(result);
        }
        [HttpPost("update-module")]
        // [Authorize]
        public async Task<IActionResult> UpdateModule([FromBody] UpdateAssetRequestDTO updateAssetDTO)
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

        [HttpPost("update-sub-module")]
        // [Authorize]
        public async Task<IActionResult> UpdateSubModule([FromBody] UpdateAssetRequestDTO updateAssetDTO)
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

      

      
        #endregion

        
        

    }
}
