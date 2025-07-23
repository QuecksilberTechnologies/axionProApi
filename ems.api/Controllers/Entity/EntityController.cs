using ems.application.DTOs.Entity;
using ems.application.Interfaces.ILogger;
using ems.application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ems.api.Controllers.Entity
{

    [ApiController]
    [Route("api/[controller]")]
    public class EntityController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILoggerService _logger;  // Logger service ka declaration

        public EntityController(IMediator mediator, ILoggerService logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpGet("get-entity-names")]
        public IActionResult GetStaticEntityNames([FromQuery] GetEntityNameRequestDTO dTO)
        {
            var entities = new List<GetEntityNameResponseDTO>
    {
        new GetEntityNameResponseDTO { Id = 1, Name = "Employee" },
        new GetEntityNameResponseDTO { Id = 2, Name = "EmployeeBankDetail" },
        new GetEntityNameResponseDTO { Id = 3, Name = "EmployeeExperience" },
        new GetEntityNameResponseDTO { Id = 4, Name = "EmployeeFamily" },
        new GetEntityNameResponseDTO { Id = 5, Name = "EmployeeDocument" },
        new GetEntityNameResponseDTO { Id = 6, Name = "EmployeeEducation" }
    };

            return Ok(ApiResponse<List<GetEntityNameResponseDTO>>.Success(entities));
        }

    }
}