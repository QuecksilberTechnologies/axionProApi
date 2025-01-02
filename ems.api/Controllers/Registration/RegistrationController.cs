using ems.application.DTOs.RegistrationDTO;
using ems.application.DTOs.UserLogin;
using ems.application.Features.UserLoginAndDashboardCmd.Commands;
using ems.application.Interfaces.ILogger;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ems.api.Controllers.Registration
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController :  ControllerBase
    {
        private readonly IMediator _mediator;
    private readonly ILoggerService _logger;  // Logger service ka declaration

    public RegistrationController(IMediator mediator, ILoggerService logger)
    {
        _mediator = mediator;
        _logger = logger;
    }


    [HttpPost("candidate")]
    public async Task<IActionResult> Login([FromBody] CandidateRequestDTO candidateRegistrationDTO)
    {
           // (14, '1234-5678-9012', 'ABCDE1234F', 'P1234567', 'DL1234567890',
           //     'V1234567890', 'O+', 'Married', 'Indian', 'John Doe', '9876543210'),
           
           //(15, '2345-6789-0123', 'ABCDE5678G', 'P2345678', 'DL2345678901',
           // 'V2345678901', 'A-', 'Single', 'Indian', 'Jane Doe', '9876543211'),
           
           //(16, '3456-7890-1234', 'ABCDE6789H', 'P3456789', 'DL3456789012',
           // 'V3456789012', 'B+', 'Divorced', 'Indian', 'Alice Johnson', '9876543212'),
           
           //(17, '4567-8901-2345', 'ABCDE7890I', 'P4567890', 'DL4567890123',
           // 'V4567890123', 'AB-', 'Widowed', 'Indian', 'Bob Smith', '9876543213');
            _logger.LogInfo("Received request for register a new candidate" + candidateRegistrationDTO.ToString());
        var command = new CandidateRegistrationCommand(candidateRegistrationDTO);
        var result = await _mediator.Send(command);
        if (!result.IsSuccecced)
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
            if (!result.IsSuccecced)
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
