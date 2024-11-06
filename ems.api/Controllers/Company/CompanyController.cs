using ems.application.Interfaces.ILogger;
using MediatR;
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
    public async Task<IActionResult> Get(string firstname, string lastname)
    {
        _logger.LogInfo("Company is created");
        return Ok();
    }
}
