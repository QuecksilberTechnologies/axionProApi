using ems.application.DTOs.UserLogin;
using ems.application.Features.EmployeeCmd.Commands;
using ems.application.Features.UserLoginAndDashboardCmd.Commands;
 
using ems.application.Interfaces.ILogger;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
       

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO logindto)
        {
            _logger.LogInfo("Received login request for user: {LoginId}" + logindto.LoginId.ToString());
            var command = new LoginCommand(logindto);
            var result = await _mediator.Send(command);
            if (!result.IsSucceeded)
            {
                return Unauthorized(result);
            }
           return Ok(result);
        }
        [HttpPost("AccessDetails")]
        [Authorize] // Ensures the user is authenticated via token
        public async Task<IActionResult> UserAccessDetailsAsync([FromBody] AccessDetailRequestDTO accessDetailsDTO)
        {
            try
            {
                // Log the request
             //  _logger.LogInformation("Accessing AccessDetail for user: {EmployeeId}", accessDetailsDTO.EmployeeId);

                // Validate input
                if (accessDetailsDTO == null || accessDetailsDTO.EmployeeId <= 0)
                {
                  //  _logger.LogWarning("Invalid request data provided for AccessDetail.");
                    return BadRequest(new { Message = "Invalid request data." });
                }

                // Create and send the command
                var command = new EmployeeTypeBasicMenuCommand(accessDetailsDTO);
                var result = await _mediator.Send(command);

                // Check the result of the command
                if (!result.IsSucceeded)
                {
                  //  _logger.LogWarning("AccessDetail retrieval failed for EmployeeId: {EmployeeId}", accessDetailsDTO.EmployeeId);
                    return Unauthorized(result);
                }

                // Success response
              //  _logger.LogInformation("AccessDetail successfully retrieved for EmployeeId: {EmployeeId}", accessDetailsDTO.EmployeeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the error
               // _logger.LogError(ex, "An error occurred while processing AccessDetail for EmployeeId: {EmployeeId}", accessDetailsDTO?.EmployeeId);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while processing the request." });
            }
        }


        [HttpPost("accessrolepermissions")]
        [Authorize] // Ensures the user is authenticated via token
        public async Task<IActionResult> UserAccessRolesAsync([FromBody] AccessDetailRequestDTO accessDetailsDTO)
        {
            try
            {
                // Log the request
                //  _logger.LogInformation("Accessing AccessDetail for user: {EmployeeId}", accessDetailsDTO.EmployeeId);

                // Validate input
                if (accessDetailsDTO == null || accessDetailsDTO.EmployeeId <= 0)
                {
                    //  _logger.LogWarning("Invalid request data provided for AccessDetail.");
                    return BadRequest(new { Message = "Invalid request data." });
                }

                // Create and send the command
                var command = new UserRolesPermissionOnModuleCommand(accessDetailsDTO);
                var result = await _mediator.Send(command);

                // Check the result of the command
                if (!result.IsSucceeded)
                {
                    //  _logger.LogWarning("AccessDetail retrieval failed for EmployeeId: {EmployeeId}", accessDetailsDTO.EmployeeId);
                    return Unauthorized(result);
                }

                // Success response
                //  _logger.LogInformation("AccessDetail successfully retrieved for EmployeeId: {EmployeeId}", accessDetailsDTO.EmployeeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the error
                // _logger.LogError(ex, "An error occurred while processing AccessDetail for EmployeeId: {EmployeeId}", accessDetailsDTO?.EmployeeId);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while processing the request." });
            }
        }




    }












}

 
