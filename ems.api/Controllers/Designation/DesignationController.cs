using ems.application.DTOs.Designation;
 
using ems.application.Features.DesignationCmd.Commands;
using ems.application.Features.DesignationCmd.Queries;
using ems.application.Features.OperationCmd.Commands;
using ems.application.Features.OperationCmd.Queries;
using ems.application.Interfaces.ILogger;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ems.api.Controllers.Designation
{
    [ApiController]
    [Route("api/[controller]")]
    public class DesignationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILoggerService _logger;  // Logger service ka declaration

        public DesignationController(IMediator mediator, ILoggerService logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        [HttpGet("get-all-designation-by-tenant")]
        public async Task<IActionResult> GetAllDesignationAsyc([FromQuery] DesignationRequestDTO designationRequestDTO)
        {
            _logger.LogInfo($"Received request to get designation from userId: {designationRequestDTO.Id}");

            var command = new GetAllDesignationQuery(designationRequestDTO);
            var result = await _mediator.Send(command);

            if (!result.IsSucceeded)
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }
        [HttpPost("adddesignation")]
        public async Task<IActionResult> CreateDesignation([FromBody] CreateDesignationDTO createDesignationDTO)
        {
            if (createDesignationDTO == null)
            {
                _logger.LogInfo("Received null request for creating designation .");  // ✅ अब सही है
                return BadRequest(new { success = false, message = "Invalid request" });
            }

            _logger.LogInfo($"Received request to create a new designation: {createDesignationDTO.DesignationName}");

            var command = new CreateDesignationCommand(createDesignationDTO);
            var result = await _mediator.Send(command);

            if (!result.IsSucceeded)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("updatedesignation")]
        public async Task<IActionResult> UpdateDesignation([FromBody] UpdateDesignationDTO updateDesignationDTO)
        {
            _logger.LogInfo("Received request for update sedignation" + updateDesignationDTO.ToString());
            var command = new UpdateDesignationCommand(updateDesignationDTO);
            var result = await _mediator.Send(command);
            if (!result.IsSucceeded)
            {
                return Ok(result);
            }
            return Ok(result);
        }

    }
}
