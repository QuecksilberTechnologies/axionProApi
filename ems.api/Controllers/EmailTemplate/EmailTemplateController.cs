using ems.application.DTOs.EmailTemplate;
using ems.application.Features.EmailTemplateCmd.Queries;
using ems.application.Interfaces.ILogger;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ems.api.Controllers.EmailTemplate
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailTemplateController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILoggerService _logger;

        public EmailTemplateController(IMediator mediator, ILoggerService logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Get email templates by template code
        /// </summary>
        /// <param name="code">Template code</param>
        /// <returns>List of matching email templates</returns>
        [HttpGet("gettemplatebycode")]
        public async Task<IActionResult> GetTemplateByCodeAsync([FromQuery] string code)
        {
            _logger.LogInfo($"Getting email templates for code: {code}");

            var query = new GetEmailTemplateByCodeQuery(code);
            var result = await _mediator.Send(query);

            if (!result.IsSucceeded)
            {
                _logger.LogInfo($"No templates found for code: {code}");
                return NotFound(result); // NotFound better than Unauthorized here
            }

            return Ok(result);
        }
    }
}
