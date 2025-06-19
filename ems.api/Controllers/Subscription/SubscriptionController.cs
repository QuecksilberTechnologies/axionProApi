using ems.application.DTOs.Asset;
using ems.application.DTOs.SubscriptionModule;
using ems.application.Features.AssetCmd.Commands;
using ems.application.Features.SubscriptionCmd.Commands;
using ems.application.Interfaces.ILogger;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ems.api.Controllers.Subscription
{
    public class SubscriptionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILoggerService _logger;  // Logger service ka declaration

        public SubscriptionController(IMediator mediator, ILoggerService logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        #region Asset

        [HttpPost("get-all-subscription-plan")]
        public async Task<IActionResult> GetAllSubscriptionPlan([FromBody] SubscriptionPlanRequestDTO? subscriptionPlanRequestDTO)
        {
            if (subscriptionPlanRequestDTO == null)
            {
                // _logger.LogWarning("Received null request for getting Assets.");
                // return BadRequest(new ApiResponse<List<GetAllAssetDTO>>(false, "Invalid request", null));
            }

            // _logger.LogInformation("Received request to get Assets for userId: {LoginId}", AssetRequestDTO.Id);

            var query = new GetSubscriptionPlanCommand(subscriptionPlanRequestDTO);  //  Fix: No parameter needed in GetAllAssetQuery
            var result = await _mediator.Send(query);

            if (!result.IsSucceeded)
            {
                return Unauthorized(result);
            }

            return Ok(result);
        }

        #endregion
    }

}
