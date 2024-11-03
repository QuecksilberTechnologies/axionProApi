using ems.application.Features.UserLoginCmd.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ems.api.Controllers.Login
{
    // UserLoginController.cs
    [ApiController]
    //[Route("api/[controller]")]
    public class UserLoginController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserLoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("api/[controller]")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.Success)
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }
    }

}
