﻿using AutoMapper;
using ems.application.Common.Helpers;
using ems.application.DTOs.UserLogin;
using ems.application.Features.UserLoginAndDashboardCmd.Commands;
using ems.application.Interfaces.IEmail;
using ems.application.Interfaces.ITokenService;
using ems.application.Interfaces;
using ems.application.Wrappers;
using ems.domain.Entity;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.UserLoginAndDashboardCmd.Handlers
{
    public class ValidateOtpCommandHandler : IRequestHandler<ValidateOtpCommand, ApiResponse<bool>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
       
      
        private readonly ILogger<ValidateOtpCommandHandler> _logger;
     
        public ValidateOtpCommandHandler(IMapper mapper, IUnitOfWork unitOfWork  , ILogger<ValidateOtpCommandHandler> logger )
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            

        }

        public async Task<ApiResponse<bool>> Handle(ValidateOtpCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // 🔐 Step 1: Validate if user exists
                long empId = await _unitOfWork.CommonRepository.ValidateActiveUserLoginOnlyAsync(request.dTO.LoginId);
                _logger.LogInformation("Validation result for LoginId {LoginId}: EmployeeId = {empId}", request.dTO.LoginId, empId);

                if (empId < 1)
                {
                    _logger.LogWarning("User validation failed for LoginId: {LoginId}", request.dTO.LoginId);
                    await _unitOfWork.RollbackTransactionAsync();
                    return ApiResponse<bool>.Fail("User is not authenticated or authorized to perform this action.");
                }

                long userId = await _unitOfWork.CommonRepository.ValidateActiveUserCrendentialOnlyAsync(request.dTO.LoginId);
                Employee? empInfo = await _unitOfWork.Employees.GetEmployeeByIdAsync(empId);

                // 🔍 Step 2: Check Existing OTP
                var existingOtpEntry = await _unitOfWork.ForgotPasswordOtpRepository.GetValidOtpByEmployeeIdAsync(userId, empInfo.TenantId);

                if (existingOtpEntry == null)
                {
                    return ApiResponse<bool>.Fail("No OTP found or it has expired.");
                }

                // 🔄 Step 3: Validate OTP and Expiry
                if (existingOtpEntry.Otp != request.dTO.OTP)
                {
                    return ApiResponse<bool>.Fail("Invalid OTP entered.");
                }

                if (existingOtpEntry.OtpexpireDateTime <= DateTime.Now)
                {
                    return ApiResponse<bool>.Fail("OTP has expired. Please request a new one.");
                }

                existingOtpEntry.IsUsed = false;
                existingOtpEntry.IsValidate = true;
                
                await _unitOfWork.ForgotPasswordOtpRepository.UpdateOTPAsync(existingOtpEntry);
                var isOtpUpdated=  await _unitOfWork.ForgotPasswordOtpRepository.UpdateOTPAsync(existingOtpEntry);
                if(isOtpUpdated)

                return ApiResponse<bool>.Success(true);
                return ApiResponse<bool>.Fail("Something went wrong while verifying the OTP.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in ValidateOtpCommand Handler.");
                await _unitOfWork.RollbackTransactionAsync();

                return ApiResponse<bool>.Fail("Something went wrong while verifying the OTP.");
            }
        }


    }
}