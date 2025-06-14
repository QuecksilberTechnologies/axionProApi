using ems.application.DTOs.Asset;
using ems.application.Features.AssetCmd.Commands;
using ems.application.Features.AssetCmd.Queries;
using ems.application.Interfaces.ILogger;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        #region Asset

        [HttpPost("get-all-asset-by-admin")]
        public async Task<IActionResult> GetAllAssets([FromQuery] AssetRequestDTO? AssetRequestDTO)
        {
            if (AssetRequestDTO == null)
            {
                // _logger.LogWarning("Received null request for getting Assets.");
                // return BadRequest(new ApiResponse<List<GetAllAssetDTO>>(false, "Invalid request", null));
            }

            // _logger.LogInformation("Received request to get Assets for userId: {LoginId}", AssetRequestDTO.Id);

            var query = new GetAllAssetByTenantCommand(AssetRequestDTO);  //  Fix: No parameter needed in GetAllAssetQuery
            var result = await _mediator.Send(query);

            if (!result.IsSucceeded)
            {
                return Unauthorized(result);
            }

            return Ok(result);
        }

        [HttpPost("update-asset-by-admin")]
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

        [HttpPost("delete-asset-by-tenant-admin")]
        public async Task<IActionResult> DeleteAssetByTenant([FromBody] DeleteAssetRequestDTO deleteAssetDTO)
        {
            _logger.LogInfo("Received request for deleted  Asset" + deleteAssetDTO.ToString());
            var command = new DeleteAssetCommand(deleteAssetDTO);
            var result = await _mediator.Send(command);
            if (!result.IsSucceeded)
            {
                return Ok(result);
            }
            return Ok(result);
        }



        [HttpPost("add-asset-by-tenant-admin")]
        //[authorize]
        public async Task<IActionResult> createasset([FromBody] CreateAssetDTO createassetdto)
        {
            _logger.LogInfo("received request for create a new asset" + createassetdto);
            var command = new CreateAssetCommand(createassetdto);
            var result = await _mediator.Send(command);
        
            if (!result.IsSucceeded)
            {
                return Ok(result);
            }
            return Ok(result);
        }
        #endregion
        #region AssetStatus
        
        [HttpPost("add-asset-status-by-tenant-admin")]
        // [Authorize]
        public async Task<IActionResult> CreateAssetStatus([FromBody] AddAssetStatusRequestDTO addAssetStatusRequestDTO)
        {
            _logger.LogInfo("Received request for create a new Asset" + addAssetStatusRequestDTO.ToString());
            var command = new AddStatusByTenantCommand(addAssetStatusRequestDTO);
            var result = await _mediator.Send(command);
            if (!result.IsSucceeded)
            {
                return Ok(result);
            }
            return Ok(result);
        }
        [HttpPost("update-asset-status-by-tenant-admin")]
        // [Authorize]
        public async Task<IActionResult> UpdateAssetStatusByTenant([FromBody] UpdateAssetStatusRequestDTO updateAssetStatusRequestDTO)
        {
            _logger.LogInfo("Received request for update a new Asset" + updateAssetStatusRequestDTO.ToString());
            var command = new UpdateStatusByTenantCommand(updateAssetStatusRequestDTO);
            var result = await _mediator.Send(command);
            if (!result.IsSucceeded)
            {
                return Ok(result);
            }
            return Ok(result);
        }
        [HttpGet("get-all-asset-status-by-tenant")]
        public async Task<IActionResult> GetAllAssets([FromQuery] AddAssetStatusRequestDTO? assetStatusRequestDTO)
        {
            if (assetStatusRequestDTO == null)
            {
                // _logger.LogWarning("Received null request for getting Assets.");
                // return BadRequest(new ApiResponse<List<GetAllAssetDTO>>(false, "Invalid request", null));
            }

            // _logger.LogInformation("Received request to get Assets for userId: {LoginId}", AssetRequestDTO.Id);

            var query = new GetAllAssetStatusByFieldQuery(assetStatusRequestDTO);  //  Fix: No parameter needed in GetAllAssetQuery
            var result = await _mediator.Send(query);

            if (!result.IsSucceeded)
            {
                return Unauthorized(result);
            }

            return Ok(result);
        }

        [HttpPost("delete-asset-status-by-tenant-admin")]
        public async Task<IActionResult> DeleteAssetStatusByTenant([FromBody] DeleteAssetStatusRequestDTO deleteAssetStatusRequestDTO)
        {
            _logger.LogInfo("Received request for deleted  Asset" + deleteAssetStatusRequestDTO.ToString());
            var command = new DeleteStatusByTenantCommand(deleteAssetStatusRequestDTO);
            var result = await _mediator.Send(command);
            if (!result.IsSucceeded)
            {
                return Ok(result);
            }
            return Ok(result);
        }

        #endregion

        #region AssetTypeComplete
        [HttpPost("add-asset-type-by-tenant-admin")]
        // [Authorize]
        public async Task<IActionResult> CreateAssetType([FromBody] AssetTypeRequestDTO assetTypeRequestDTO)
        {
            _logger.LogInfo("Received request for create a new Asset Type" + assetTypeRequestDTO.ToString());
            var command = new AddNewAssetTypeByTenantCommand(assetTypeRequestDTO);
            var result = await _mediator.Send(command);
            if (!result.IsSucceeded)
            {
                return Ok(result);
            }
            return Ok(result);
        }

        [HttpPost("gell-all-asset-type-by-tenant")]
        // [Authorize]
        public async Task<IActionResult> GetAllAssetTypeByTenant([FromBody] GetAssetTypeRequestDTO getAllAssetTypeDTO)
        {
            _logger.LogInfo("Received request for create a new Asset Type" + getAllAssetTypeDTO.ToString());
            var command = new GetAllAssetTypeByTenantCommand(getAllAssetTypeDTO);
            var result = await _mediator.Send(command);
            if (!result.IsSucceeded)
            {
                return Ok(result);
            }
            return Ok(result);
        }

        [HttpPost("update-asset-type-by-tenant-admin")]
        // [Authorize]
        public async Task<IActionResult> UpdateAssetTypeByTenant([FromBody] UpdateAssetTypeRequestDTO updateAssetTypeRequestDTO)
        {
            _logger.LogInfo("Received request for update a  Asset- type" + updateAssetTypeRequestDTO.ToString());
            var command = new UpdateAssetTypeCommand(updateAssetTypeRequestDTO);
            var result = await _mediator.Send(command);
            if (!result.IsSucceeded)
            {
                return Ok(result);
            }
            return Ok(result);
        }

        [HttpPost("delete-asset-type-by-tenant-admin")]
        public async Task<IActionResult> DeleteAssetTypeByTenant([FromBody] DeleteAssetTypeRequestDTO deleteAssetTypeRequestDTO)
        {
            _logger.LogInfo("Received request for deleted  Asset" + deleteAssetTypeRequestDTO.ToString());
            var command = new DeleteAssetTypeCommand(deleteAssetTypeRequestDTO);
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



