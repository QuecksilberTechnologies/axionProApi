using ems.application.DTOs.Region;
using ems.application.DTOs.Registration;
using ems.application.DTOs.Tenant;
using ems.application.Features.RegionCmd.Queries;
using ems.application.Features.RegistrationCmd.Commands;
using ems.application.Features.RegistrationCmd.Queries;
using ems.application.Features.UserLoginAndDashboardCmd.Commands;
using ems.application.Interfaces.ILogger;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ems.api.Controllers.Company;

[Route("api/[controller]")]
[ApiController]
public class CompanyController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILoggerService _logger;  // Logger service ka declaration

    public CompanyController(IMediator mediator, ILoggerService logger)
    {
        _mediator = mediator;
        _logger = logger;  // Logger service ko inject karna
    }
   

    [HttpGet("{firstname}/{lastname}")]
   // [Authorize]
    public async Task<IActionResult> Get(string firstname, string lastname)
    {
        _logger.LogInfo("Company is created");
        return Ok();
    }
    [HttpPost("tenant")]
    // [Authorize]
    public async Task<IActionResult> TenantCreation([FromBody] TenantCreateRequestDTO tenantCreateRequestDTO)
    {
        _logger.LogInfo("Received request for register a new Tenant" + tenantCreateRequestDTO.ToString());
        var command = new CreateTenantCommand(tenantCreateRequestDTO);
        var result = await _mediator.Send(command);
        if (!result.IsSucceeded)
        {
            return Ok(result);
        }
        return Ok(result);
    }

    [HttpGet("getalltenant")]
    public async Task<IActionResult> GetAllTenantAsync([FromQuery] TenantRequestDTO code)
    {
        _logger.LogInfo($"Getting email templates for code: {code}");

        var query = new GetAllTenantQuery(code);
        var result = await _mediator.Send(query);

        if (!result.IsSucceeded)
        {
            _logger.LogInfo($"No templates found for code: {code}");
            return NotFound(result); // NotFound better than Unauthorized here
        }

        return Ok(result);
    }
}
