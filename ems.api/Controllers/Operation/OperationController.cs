using ems.application.DTOs.Operation;
 
using ems.application.Features.OperationCmd.Commands;
using ems.application.Features.OperationCmd.Queries;
using ems.application.Features.RoleCmd.Queries;
using ems.application.Features.TransportCmd.Commands;
using ems.application.Features.TransportCmd.Queries;
using ems.application.Interfaces.ILogger;
using ems.application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ems.api.Controllers.Operation
{
    [ApiController]
    [Route("api/[controller]")]
    public class OperationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILoggerService _logger;  // Logger service ka declaration

        public OperationController(IMediator mediator, ILoggerService logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        [HttpGet("get-all-operation-by-product-owner")]
        public async Task<IActionResult> GetAllOperationAsyc([FromQuery] GetAllOperationRequestByProductAdminDTO operationRequestDTO)
        {
            _logger.LogInfo($"Received request to get operationRequestDTO from userId: {operationRequestDTO.ProductOwnerId}");

            var command = new GetAllOperationCommand(operationRequestDTO);
            var result = await _mediator.Send(command);

            if (!result.IsSucceeded)
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }
        [HttpPost("add-operation-by-product-owner")]
        public async Task<IActionResult> CreateTravelModeType([FromBody] CreateOperationDTO createOperationDTO)
        {
            if (createOperationDTO == null)
            {
                _logger.LogInfo("Received null request for creating operationRequestDTO .");  // ✅ अब सही है
                return BadRequest(new { success = false, message = "Invalid request" });
            }

            _logger.LogInfo($"Received request to create a new operationRequestDTO: {createOperationDTO.OperationName}");

            var command = new CreateOperationCommand(createOperationDTO);
            var result = await _mediator.Send(command);

            if (!result.IsSucceeded)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("update-operation-by-product-owner")]
        public async Task<IActionResult> UpdateOperation([FromBody] UpdateOperationDTO updateOperationDTO)
        {
            _logger.LogInfo("Received request for update a leave" + updateOperationDTO.ToString());
            var command = new UpdateOperationCommand(updateOperationDTO);
            var result = await _mediator.Send(command);
            if (!result.IsSucceeded)
            {
                return Ok(result);
            }
            return Ok(result);
        }
         [Authorize]
        [HttpGet("has-page-operation-access")]
        public async Task<IActionResult> HasPageOperationAccess([FromQuery] CheckOperationPermissionRequestDTO? checkOperationPermissionRequest)
        {
            if (checkOperationPermissionRequest == null)
            {
              //  _logger.LogWarning("❌ Received null request for checking operation permission.");
               // return BadRequest(new ApiResponse<HasAccessOperationDTO>(false, "Invalid request", null));
            }

        //    _logger.LogInformation("📢 Checking operation permission for RoleId: {RoleId}, ModuleId: {ModuleId}, OperationId: {OperationId}",
             //   checkOperationPermissionRequest.RoleId, checkOperationPermissionRequest.ProjectChildModuleDetailId, checkOperationPermissionRequest.OperationId);

            var query = new GetPageOperationPermissionQuery(checkOperationPermissionRequest);
            var result = await _mediator.Send(query);

            if (!result.IsSucceeded)
            {
            //    _logger.LogWarning("⛔ Permission denied for RoleId: {RoleId}, ModuleId: {ModuleId}, OperationId: {OperationId}",
                   // checkOperationPermissionRequest.RoleId, checkOperationPermissionRequest.ProjectChildModuleDetailId, checkOperationPermissionRequest.OperationId);
                return Unauthorized(result);
            }

            return Ok(result);
        }
        


        //  [HttpPost("getalltendermaincategory")]
        //public async Task<IActionResult> GetAllTenderMainCategories([FromBody] TenderCategoryRequestDTO? tenderCategoryRequestDTO)
        //{
        //    _logger.LogInfo("Received  request to get categories from userId: {LoginId}" + tenderCategoryRequestDTO.Id.ToString());
        //    var command = new GetTenderMainCategoryRequestCommand(tenderCategoryRequestDTO);
        //    var result = await _mediator.Send(command);
        //    if (!result.IsSuccecced)
        //    {
        //        return Unauthorized(result);
        //    }
        //    return Ok(result);
        //}


        //[HttpPost("getallmainchildcategory")]
        //public async Task<IActionResult> GetAllMainChildCategories([FromBody] CategoryRequestDTO? categoryRequestDTO)
        //{
        //    _logger.LogInfo("Received  request to get sub-categories from userId: {LoginId}" + categoryRequestDTO.Id.ToString());
        //    var command = new GetMainChildCategoryCommand(categoryRequestDTO);
        //    var result = await _mediator.Send(command);
        //    if (!result.IsSuccecced)
        //    {
        //        return Unauthorized(result);
        //    }
        //    return Ok(result);
        //}


    }

}
