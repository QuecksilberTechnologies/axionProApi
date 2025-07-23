using ems.application.DTOs.Tenant;
using ems.application.Features.TenantCmd.Queries;
using ems.application.Features.TenantIndustryCmd.Queries;
using ems.application.Interfaces.ILogger;
using MediatR;
using Microsoft.AspNetCore.Mvc;
 

namespace ems.api.Controllers.TenantIndustry
{
    public class TenantIndustryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILoggerService _logger;  // Logger service ka declaration

        public TenantIndustryController(IMediator mediator, ILoggerService logger)
        {
            _mediator = mediator;
            _logger = logger;  // Logger service ko inject karna
        }

 
        [HttpGet("get-all-industry-by-any")]
        public async Task<IActionResult> GetAllTenantBySubscriptionIdAsync([FromQuery] int planId)
        {
            _logger.LogInfo($"Getting email templates for code: {planId}");

            var query = new GetAllTenantIndustryQuery(planId);
            var result = await _mediator.Send(query);
            if (!result.IsSucceeded)
            {
                _logger.LogInfo($"No templates found for code: {planId}");
                return NotFound(result); // NotFound better than Unauthorized here
            }

            return Ok(result);
        }
        [HttpGet("get-tenant-subscription-plan-information-by-tenantId")]
        public async Task<IActionResult> GetTenantSubscriptionPlanInfoAsync([FromQuery] TenantSubscriptionPlanRequestDTO code)
        {
            _logger.LogInfo($"Getting email templates for code: {code}");

            var query = new GetTenantSubscriptionQuery(code);
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
