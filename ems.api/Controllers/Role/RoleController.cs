 
using ems.application.DTOs.Role;
using ems.application.Features.CategoryCmd.Command;
using ems.application.Features.EmployeeCmd.Commands;
using ems.application.Features.RoleCmd.Commands;
using ems.application.Features.RoleCmd.Queries;
using ems.application.Interfaces.ILogger;
using ems.application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ems.api.Controllers.Role
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILoggerService _logger;  // Logger service ka declaration

        public RoleController(IMediator mediator, ILoggerService logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpPost("update-role-by-tenant-admin")]
        // [Authorize]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleDTO updateRoleDTO)
        {
            _logger.LogInfo("Received request for update a new role" + updateRoleDTO.ToString());
            var command = new UpdateRoleCommand(updateRoleDTO);
            var result = await _mediator.Send(command);
            if (!result.IsSucceeded)
            {
                return Ok(result);
            }
            return Ok(result);
        }

        [HttpPost("add-role-by-tenant-admin")]
         //  [Authorize]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDTO createRoleDTO)
        {
            _logger.LogInfo("Received request for create a new role" + createRoleDTO.ToString());
            var command = new CreateRoleCommand(createRoleDTO);
            var result = await _mediator.Send(command);
            if (!result.IsSucceeded)
            {
                return Ok(result);
            }
            return Ok(result);
        }
     //   [Authorize]
        [HttpGet("get-all-active-role-by-tenant-user")]
        public async Task<IActionResult> GetAllActiveRole([FromQuery] GetActiveRoleRequestDTO? roleRequestDTO)
        {
            if (roleRequestDTO == null)
            {
                // _logger.LogWarning("Received null request for getting roles.");
                // return BadRequest(new ApiResponse<List<GetAllRoleDTO>>(false, "Invalid request", null));
            }

            // _logger.LogInformation("Received request to get roles for userId: {LoginId}", roleRequestDTO.Id);

            var query = new GetAllActiveRoleQuery(roleRequestDTO);  //  Fix: No parameter needed in GetAllRoleQuery
            var result = await _mediator.Send(query);

            if (!result.IsSucceeded)
            {
                return Unauthorized(result);
            }

            return Ok(result);
        }
        [HttpGet("get-all-role-by-tenant-admin")]
        public async Task<IActionResult> GetAllRoles([FromQuery] GetRoleRequestDTO? roleRequestDTO)
        {
            if (roleRequestDTO == null)
            {
                // _logger.LogWarning("Received null request for getting roles.");
                // return BadRequest(new ApiResponse<List<GetAllRoleDTO>>(false, "Invalid request", null));
            }

            // _logger.LogInformation("Received request to get roles for userId: {LoginId}", roleRequestDTO.Id);

            var query = new GetAllRoleQuery(roleRequestDTO);  //  Fix: No parameter needed in GetAllRoleQuery
            var result = await _mediator.Send(query);

            if (!result.IsSucceeded)
            {
                return Unauthorized(result);
            }

            return Ok(result);
        }
        [HttpPost("get-role-by-role-code")]
        public async Task<IActionResult> GetGlobalRole([FromBody] GetRoleIdByRoleCodeRequestDTO? getRoleIdByRoleCodeRequestDTO)
        {
            if (getRoleIdByRoleCodeRequestDTO == null)
            {
                // _logger.LogWarning("Received null request for getting roles.");
                // return BadRequest(new ApiResponse<List<GetAllRoleDTO>>(false, "Invalid request", null));
            }

            // _logger.LogInformation("Received request to get roles for userId: {LoginId}", roleRequestDTO.Id);

            var command = new GetRoleIdByRoleInfoCommand(getRoleIdByRoleCodeRequestDTO);
            var result = await _mediator.Send(command);
            if (!result.IsSucceeded)
            {
                return Ok(result);
            }
            return Ok(result);

       
        }


    }

}
