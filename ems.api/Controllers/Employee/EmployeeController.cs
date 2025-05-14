
using ems.application.DTOs.Employee;
using ems.application.Features.EmployeeCmd.Commands;
using ems.application.Interfaces.ILogger;
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

    [HttpPost("createemployee")]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDTO employeeCreateDto)
    {
        var command = new CreateEmployeeCommand(employeeCreateDto);
        _logger.LogInfo("Creating new employee"); // Log the info message

        var result = await _mediator.Send(command);

        return Ok(result);
    }
}
 
