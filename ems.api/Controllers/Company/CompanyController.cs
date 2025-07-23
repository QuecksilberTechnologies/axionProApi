using ems.application.DTOs.Region;
using ems.application.DTOs.Registration;
using ems.application.DTOs.Tenant;
using ems.application.DTOs.Verify;
using ems.application.Features.RegionCmd.Queries;
 
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

}
