using ems.application.DTOs.AttendanceDTO;
using ems.application.DTOs.CategoryDTO;
//using ems.application.Features.AttendanceCmd.Command;
using ems.application.Features.CategoryCmd.Command;
using ems.application.Interfaces.ILogger;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ems.api.Controllers.Category
{
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILoggerService _logger;  // Logger service ka declaration

        public CategoryController(IMediator mediator, ILoggerService logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        [HttpPost("getallmaincategory")]
        public async Task<IActionResult> GetAllMainCategories([FromBody] CategoryRequestDTO? categoryRequestDTO)
        {
            _logger.LogInfo("Received  request to get categories from userId: {LoginId}" + categoryRequestDTO.Id.ToString());
            var command = new GetMainCategoryCommand(categoryRequestDTO);
            var result = await _mediator.Send(command);
            if (!result.IsSuccecced)
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }


        [HttpPost("getalltendermaincategory")]
        //public async Task<IActionResult> GetAllTenderMainCategories([FromBody] TenderCategoryRequestDTO? tenderCategoryRequestDTO)
        //{
        //    _logger.LogInfo("Received  request to get categories from userId: {LoginId}" + tenderCategoryRequestDTO.Id.ToString());
        //    var command = new GetTenderMainCategoryRequestCommand(tenderCategoryRequestDTO);
        //    var result = await _mediator.Send(command);
        //    if (!result.IsSuccecced)
        //    {
        //        return Unauthorized(result);
        //    }
        //    return Ok(result);
        //}


        [HttpPost("getallmainchildcategory")]
        public async Task<IActionResult> GetAllMainChildCategories([FromBody] CategoryRequestDTO? categoryRequestDTO)
        {
            _logger.LogInfo("Received  request to get sub-categories from userId: {LoginId}" + categoryRequestDTO.Id.ToString());
            var command = new GetMainChildCategoryCommand(categoryRequestDTO);
            var result = await _mediator.Send(command);
            if (!result.IsSuccecced)
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }


    }
}
