
using ems.application.DTOs.EmployeeDTO;
using ems.application.Features.EmployeeCmd.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ems.api.Controllers.Employee;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IMediator _mediator;
    public EmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // [HttpGet("{firstname}/{lastname}")]
    // public async Task<IActionResult> Get(string firstname, string lastname)
    //
    // var result = await _mediator.Send(new GetAllEmployeeQuery(firstname, lastname));
    //  return Ok($"{result}");
    // }
    [HttpPost("createemployee")]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDTO employeeCreateDto)
    {
        var command = new CreateEmployeeCommand(employeeCreateDto);
         
        var result = await _mediator.Send(command);

        return Ok(result);
    }
}
 
