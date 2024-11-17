using ems.application.DTOs.UserLogin;
using ems.application.Features.EmployeeCmd.Commands;
using ems.application.Features.UserLoginCmd.Commands;
using ems.application.Interfaces.ILogger;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace ems.api.Controllers.Login
{
    // UserLoginController.cs
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILoggerService _logger;  // Logger service ka declaration
      
        public AuthController(IMediator mediator, ILoggerService logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
       

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO logindto)
        {
            _logger.LogInfo("Received login request for user: {LoginId}" + logindto.LoginId.ToString());
            var command = new LoginCommand(logindto);
            var result = await _mediator.Send(command);
            if (!result.IsSuccecced)
            {
                return Unauthorized(result);
            }
           return Ok(result);
        }
      


 
    }

}
