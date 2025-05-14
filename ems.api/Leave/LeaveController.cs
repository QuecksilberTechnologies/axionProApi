using ems.application.DTOs.Leave;
 
using ems.application.Features.LeaveCmd.Commands;
using ems.application.Features.LeaveCmd.Queries;
using ems.application.Features.RoleCmd.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ems.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<LeaveController> _logger;  // 🔹 Microsoft ILogger उपयोग करें

        public LeaveController(IMediator mediator, ILogger<LeaveController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("addleavetype")]
        public async Task<IActionResult> CreateLeaveType([FromBody] CreateLeaveTypeDTO createLeaveTypeDTO)
        {
            if (createLeaveTypeDTO == null)
            {
                _logger.LogWarning("Received null request for creating leave type.");  // ✅ अब सही है
                return BadRequest(new { success = false, message = "Invalid request" });
            }

            _logger.LogInformation($"Received request to create a new leave type: {createLeaveTypeDTO.LeaveName}");

            var command = new CreateLeaveTypeCommand(createLeaveTypeDTO);
            var result = await _mediator.Send(command);

            if (!result.IsSuccecced)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        [HttpGet("getallleaves")]
        public async Task<IActionResult> GetAllLeaves([FromQuery] LeaveTypeRequestDTO leaveRequestDTO)
        {
            if (leaveRequestDTO == null)
            {
                _logger.LogWarning("Received null request for getting leaves.");
                return BadRequest(new { success = false, message = "Invalid request" });
            }

            _logger.LogInformation("Fetching all leave types...");

            var query = new GetAllLeaveTypeQuery(leaveRequestDTO);
            var result = await _mediator.Send(query);

            if (!result.IsSuccecced)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        [HttpPost("updateleavetype")]
        // [Authorize]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateLeaveTypeDTO updateLeaveTypeDTO)
        {
            _logger.LogInformation("Received request for update a leave" + updateLeaveTypeDTO.ToString());
            var command = new UpdateLeaveTypeCommand(updateLeaveTypeDTO);
            var result = await _mediator.Send(command);
            if (!result.IsSuccecced)
            {
                return Ok(result);
            }
            return Ok(result);
        }
    }
}

