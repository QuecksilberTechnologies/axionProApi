using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ems.api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompanyController : ControllerBase
{
    private readonly IMediator _mediator;
    public CompanyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{firstname}/{lastname}")]
    public async Task<IActionResult> Get(string firstname, string lastname)
    {
       return Ok();
    }
}
