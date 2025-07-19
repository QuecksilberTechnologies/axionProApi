
using ems.application.Common.Commands;
using ems.application.DTOs.Employee;
using ems.application.Features.EmployeeCmd.Commands;
using ems.application.Features.UserLoginAndDashboardCmd.Commands;
using ems.application.Interfaces.ILogger;
using ems.application.Wrappers;
using ems.domain.Entity;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Security.AccessControl;

namespace ems.api.Controllers.Employee;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILoggerService _logger;  // Logger service ka declaration

    public EmployeeController(IMediator mediator, ILoggerService logger)
    {
        _mediator = mediator;
        _logger = logger;  // Logger service ko inject karna
    }

    //[HttpPost("create-employee-by-permitted-user")]
    //public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeRequestDTO employeeCreateDto)
    //{
    //    var command = new CreateEmployeeCommand(employeeCreateDto);
    //    _logger.LogInfo("Creating new employee"); // Log the info message

    //    var result = await _mediator.Send(command);

    //    return Ok(result);
    //}

    [HttpPost("create-employee-by-tenant-permitted-user")]
    [Authorize]
    public async Task<IActionResult> CreateByAdminEmployee([FromBody] CreateEmployeeByTenantPermittedUserRequestDTO employeeCreateDto)
    {
        var command = new CreateEmployeeByTenantAdminCommand(employeeCreateDto);
         _logger.LogInfo("Creating new employee"); // Log the info message

         var result = await _mediator.Send(command);
        if (!result.IsSucceeded)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
    [HttpPost("get-all-employee-type-by-permitted-user")]
    public async Task<IActionResult> GetAllEmployeeType([FromBody] GetEmployeeTypeRequestDTO requestDto)
    {
        try
        {
            // Dummy data list
            var employeeTypes = new List<GetAllEmployeeTypeResponseDTO>
        {
            new GetAllEmployeeTypeResponseDTO
            {
                Id = 1,
                TypeName = "Full-Time",
                Description = "Permanent employee with all benefits",
                IsActive = true
            },
            new GetAllEmployeeTypeResponseDTO
            {
                Id = 2,
                TypeName = "Contract",
                Description = "Contract-based employee for 6/12 months",
                IsActive = true
            },
            new GetAllEmployeeTypeResponseDTO
            {
                Id = 3,
                TypeName = "Intern",
                Description = "Temporary internship employee",
                IsActive = true
            },
            new GetAllEmployeeTypeResponseDTO
            {
                Id = 4,
                TypeName = "Freelancer",
                Description = "Independent external resource",
                IsActive = false
            }
        };

            // Wrap in ApiResponse
            var response = new ApiResponse<List<GetAllEmployeeTypeResponseDTO>>(
                employeeTypes,
                "Employee types fetched successfully.",
                true
            );

            return Ok(response);
        }
        catch (Exception ex)
        {
            var errorResponse = new ApiResponse<List<GetAllEmployeeTypeResponseDTO>>(
                null!,
                "Failed to fetch employee types.",
                false
            );
            errorResponse.Errors.Add(ex.Message);
            return StatusCode(500, errorResponse);
        }
    }

    
    [HttpPost("get-any-employee-info-by-tenant-user")]
    public async Task<IActionResult> GetAnyEmployeeInfo([FromBody] GetEmployeeInfoRequestDTO commandDto)
    {     

        try
        {
            // ✅ Wrap DTO in the command class
          //  var command = new GetEmployeeInfoCommand(commandDto);
       
            var command = new GetBasicEmployeeInfoCommand(commandDto);

            // ✅ Send command instead of DTO
            ApiResponse<GetEmployeeInfoResponseDTO> result = await _mediator.Send(command);

            if (result.IsSucceeded)
                return Ok(result);
            else
                return BadRequest(result);
        }
        catch (Exception ex)
        {
            var errorResponse = ApiResponse<bool>.Fail("An unexpected error occurred while updating employee info.",
                new List<string> { ex.Message });
            return StatusCode(500, errorResponse);
        }
    }
    
         [HttpPost("get-employee-education-info-by-any")]
    public async Task<IActionResult> GetEmployeeEducationInfo([FromBody] GetEmployeeInfoRequestDTO commandDto)
    {
        try
        {

            var command = new GetEmployeeEducationCommand(commandDto);

            var result = await _mediator.Send(command);

            if (result.IsSucceeded)
                return Ok(result);
            return BadRequest(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<string>.Fail("Internal server error", new List<string> { ex.Message }));
        }
    }

    [HttpPost("get-employee-personal-info-by-any")]
    public async Task<IActionResult> GetEmployeePersonalInfo([FromBody] GetEmployeeInfoRequestDTO commandDto)
    {
        try
        {

            var command = new GetEmployeePersonalDetailCommand(commandDto);

            var result = await _mediator.Send(command);

            if (result.IsSucceeded)
                return Ok(result);
            return BadRequest(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<string>.Fail("Internal server error", new List<string> { ex.Message }));
        }
    }


    [HttpPost("get-employee-experience-info-by-any")]
    public async Task<IActionResult> GetEmployeeExperienceInfo([FromBody] GetEmployeeInfoRequestDTO commandDto)
    {
        try
        {

            var command = new GetEmployeeExperienceCommand(commandDto);

            var result = await _mediator.Send(command);

            if (result.IsSucceeded)
                return Ok(result);
            return BadRequest(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<string>.Fail("Internal server error", new List<string> { ex.Message }));
        }
    }
   
    
    [HttpPost("get-employee-bank-info-by-any")]
    public async Task<IActionResult> GetEmployeeInfo([FromBody] GetEmployeeInfoRequestDTO commandDto)
    {
        try
        {
            
            var command = new GetEmployeeBankDetailCommand(commandDto);

            var result = await _mediator.Send(command);

            if (result.IsSucceeded)
                return Ok(result);
            return BadRequest(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<string>.Fail("Internal server error", new List<string> { ex.Message }));
        }
    }


 

    [HttpPost("get-user-self-employement-info")]
    public async Task<IActionResult> GetEmployeeBySelfInfo([FromBody] GetSelfEmployeeInfoRequestDTO commandDto)
    {
        //if (commandDto == null || string.IsNullOrWhiteSpace(commandDto.TenantId))
        //{
        //    return BadRequest(ApiResponse<bool>.Fail("Invalid request. Field name is required."));
        //}

        try
        {
            // ✅ Wrap DTO in the command class
            var command = new GetSelfEmployeementInfoCommand(commandDto);

            // ✅ Send command instead of DTO
            ApiResponse<GetEmployeeInfoResponseDTO> result = await _mediator.Send(command);

            if (result.IsSucceeded)
                return Ok(result);
            else
                return BadRequest(result);
        }
        catch (Exception ex)
        {
            var errorResponse = ApiResponse<bool>.Fail("An unexpected error occurred while updating employee info.",
                new List<string> { ex.Message });
            return StatusCode(500, errorResponse);
        }
    }


    [HttpPut("update-employee-field-by-tenant-user")]
    public async Task<IActionResult> UpdateEmployeeField([FromBody] UpdateGenricAllEmployeeEntityRequestDTO commandDto)
    {
        try
        {
            ApiResponse<bool> result = ApiResponse<bool>.Fail("Invalid entity name.");

            if (commandDto.EntityName == "Employee")
            {
                var command = new UpdateEmployeeInfoWithAccessCommand(commandDto);
                result = await _mediator.Send(command);

                if (result.IsSucceeded)
                    return Ok(result);
            }
            else if (commandDto.EntityName == "EmployeeBankDetail")
            {
                var command = new UpdateEmployeeBankDetailWithAccessCommand(commandDto);
                result = await _mediator.Send(command);

                if (result.IsSucceeded)
                    return Ok(result);
            }
            else if (commandDto.EntityName == "EmployeeExperience")
            {
                var command = new UpdateEmployeeExperienceInfoWithAccessCommand(commandDto);
                result = await _mediator.Send(command);

                if (result.IsSucceeded)
                    return Ok(result);
            }
            else if (commandDto.EntityName == "EmployeeEducation")
            {
                var command = new UpdateEmployeeEducationInfoWithAccessCommand(commandDto);
                result = await _mediator.Send(command);

                if (result.IsSucceeded)
                    return Ok(result);
            }

            return BadRequest(result);
        }
        catch (Exception ex)
        {
            var errorResponse = ApiResponse<bool>.Fail("An unexpected error occurred while updating employee info.",
                new List<string> { ex.Message });
            return StatusCode(500, errorResponse);
        }
    }




}

