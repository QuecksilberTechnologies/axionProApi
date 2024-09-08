using ems.application.Features.Employee.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ems.api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IMediator _mediator;
    public EmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{firstname}/{lastname}")]
    public async Task<IActionResult> Get(string firstname, string lastname)
    {
        var result = await _mediator.Send(new GetAllEmployeeQuery(firstname, lastname));
        return Ok(result);
    }
}   
