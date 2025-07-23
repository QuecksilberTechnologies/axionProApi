using ems.application.DTOs.Department;
using ems.application.DTOs.Designation;
using ems.application.Features.DepartmentCmd.Queries;
using ems.application.Features.DesignationCmd.Commands;
using ems.application.Features.DesignationCmd.Queries;
using ems.application.Interfaces.ILogger;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ems.api.Controllers.Department
{
    [ApiController]
    [Route("api/[controller]")]

    /// <summary>
    /// handled-department-related-operations.
    /// </summary>
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILoggerService _logger;  // Logger service ka declaration

        public DepartmentController(IMediator mediator, ILoggerService logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Get all department that belong to the specified tenant.
        /// </summary>
        [HttpGet("get-department")]
        public async Task<IActionResult> GetAllDesignationAsyc([FromQuery] DepartmentRequestDTO departmentRequestDTO)
        {
            _logger.LogInfo($"Received request to get department from userId: {departmentRequestDTO.TenantId}");

            var command = new GetAllActiveDepartmentQuery(departmentRequestDTO);
            var result = await _mediator.Send(command);

            if (!result.IsSucceeded)
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }
       
    }
}

