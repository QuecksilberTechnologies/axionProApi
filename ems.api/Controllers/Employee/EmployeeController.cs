
using ems.application.DTOs.Employee;
using ems.application.Features.EmployeeCmd.Commands;
using ems.application.Interfaces.ILogger;
using ems.application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ems.api.Controllers.Employee;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILoggerService _logger;  // Logger service ka declaration

    public EmployeeController(IMediator mediator, ILoggerService logger)
    {
        _mediator = mediator;
        _logger = logger;  // Logger service ko inject karna
    }

    [HttpPost("create-employee-by-permitted-user")]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeRequestDTO employeeCreateDto)
    {
        var command = new CreateEmployeeCommand(employeeCreateDto);
        _logger.LogInfo("Creating new employee"); // Log the info message

        var result = await _mediator.Send(command);

        return Ok(result);
    }

    [HttpPost("create-employee-by-tenant-admin-with-minimal-info")]
    public async Task<IActionResult> CreateByAdminEmployee([FromBody] CreateEmployeeByTenantAdminRequestDTO employeeCreateDto)
    {
        var command = new CreateEmployeeByTenantAdminCommand(employeeCreateDto);
         _logger.LogInfo("Creating new employee"); // Log the info message

         var result = await _mediator.Send(command);
        if (!result.IsSucceeded)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
    [HttpPost("get-all-employee-type-by-permitted-user")]
    public async Task<IActionResult> GetAllEmployeeType([FromBody] GetEmployeeTypeRequestDTO requestDto)
    {
        try
        {
            // Dummy data list
            var employeeTypes = new List<GetAllEmployeeTypeResponseDTO>
        {
            new GetAllEmployeeTypeResponseDTO
            {
                Id = 1,
                TypeName = "Full-Time",
                Description = "Permanent employee with all benefits",
                IsActive = true
            },
            new GetAllEmployeeTypeResponseDTO
            {
                Id = 2,
                TypeName = "Contract",
                Description = "Contract-based employee for 6/12 months",
                IsActive = true
            },
            new GetAllEmployeeTypeResponseDTO
            {
                Id = 3,
                TypeName = "Intern",
                Description = "Temporary internship employee",
                IsActive = true
            },
            new GetAllEmployeeTypeResponseDTO
            {
                Id = 4,
                TypeName = "Freelancer",
                Description = "Independent external resource",
                IsActive = false
            }
        };

            // Wrap in ApiResponse
            var response = new ApiResponse<List<GetAllEmployeeTypeResponseDTO>>(
                employeeTypes,
                "Employee types fetched successfully.",
                true
            );

            return Ok(response);
        }
        catch (Exception ex)
        {
            var errorResponse = new ApiResponse<List<GetAllEmployeeTypeResponseDTO>>(
                null!,
                "Failed to fetch employee types.",
                false
            );
            errorResponse.Errors.Add(ex.Message);
            return StatusCode(500, errorResponse);
        }
    }

}

