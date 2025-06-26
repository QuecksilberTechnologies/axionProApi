using ems.application.DTOs.Region;
using ems.application.DTOs.Registration;
using ems.application.DTOs.Tenant;
using ems.application.DTOs.Verify;
using ems.application.Features.RegionCmd.Queries;
using ems.application.Features.RegistrationCmd.Commands;
using ems.application.Features.RegistrationCmd.Queries;
using ems.application.Features.UserLoginAndDashboardCmd.Commands;
using ems.application.Features.VerifyEmailCmd.Commands;
using ems.application.Interfaces.ILogger;
using ems.application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

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
    [HttpPost("create-tenant")]
    // [Authorize]
    public async Task<IActionResult> TenantCreation([FromBody] application.DTOs.Registration.TenantRequestDTO tenantCreateRequestDTO)
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

    [HttpGet("get-all-tenant")]
    public async Task<IActionResult> GetAllTenantAsync([FromQuery] application.DTOs.Tenant.TenantRequestDTO code)
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
    [HttpGet("get-tenant-subscription-planId-by-tenant-Id")]
    public async Task<IActionResult> GetAllTenantSubscriptionPlanAsync([FromQuery] TenantSubscriptionPlanRequestDTO  code)
    {
        _logger.LogInfo($"Getting email templates for code: {code}");

        var query = new GetTenantSubscriptionQuery(code);
        var result = await _mediator.Send(query);

        if (!result.IsSucceeded)
        {
            _logger.LogInfo($"No templates found for code: {code}");
            return NotFound(result); // NotFound better than Unauthorized here
        }

        return Ok(result);
    }

    [HttpPost("verify")]
    public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailRequestDTO request)
    {
        try
        {
            var command = new VerifyEmailCommand(request);
            var result = await _mediator.Send(command);

            if (result.IsSucceeded)
                return Ok(result);

            return BadRequest(result);
        }
        catch (Exception ex)
        {
            _logger.LogError( "An error occurred while verifying email.");
            return StatusCode(500, new ApiResponse<VerifyEmailResponseDTO>
            {
                IsSucceeded = false,
                Message = "Internal server error occurred while verifying email.",
                Data = null
            });
        }
    }
}
