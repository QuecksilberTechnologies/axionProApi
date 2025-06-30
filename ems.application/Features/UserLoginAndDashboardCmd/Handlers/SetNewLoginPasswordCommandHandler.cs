﻿using AutoMapper;
using ems.application.DTOs.UserLogin;
using ems.application.Features.UserLoginAndDashboardCmd.Commands;
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
using ems.application.Interfaces.IEmail;
using System.Diagnostics;
using Microsoft.AspNetCore.SignalR;
using static System.Net.WebRequestMethods;

namespace ems.application.Features.UserLoginAndDashboardCmd.Handlers
{
    public class SetNewLoginPasswordCommandHandler : IRequestHandler<SetNewLoginPasswordCommand, ApiResponse<UpdateLoginPasswordResponseDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly ILogger<SetNewLoginPasswordCommandHandler> _logger;

        public SetNewLoginPasswordCommandHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork,
             
            ILogger<SetNewLoginPasswordCommandHandler> logger,  IEmailService emailService)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
          
        }

        public async Task<ApiResponse<UpdateLoginPasswordResponseDTO>> Handle(SetNewLoginPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var loginId = request.dto.LoginId;

                long empId = await _unitOfWork.CommonRepository.ValidateActiveUserLoginOnlyAsync(loginId);
                _logger.LogInformation("Validation result for LoginId {LoginId}: {EmpId}", loginId, empId);

                if (empId < 1)
                {
                    _logger.LogWarning("User not found or not authorized.");
                    await _unitOfWork.RollbackTransactionAsync();
                    return ApiResponse<UpdateLoginPasswordResponseDTO>.Fail("User is not authenticated or authorized to perform this action.");
                }

                long userId = await _unitOfWork.CommonRepository.ValidateActiveUserCrendentialOnlyAsync(loginId);
                var empInfo = await _unitOfWork.Employees.GetEmployeeByIdAsync(empId);

                // 🔍 Step 2: Get OTP entry (validated and unused)
                var existingOtpEntry = await _unitOfWork.ForgotPasswordOtpRepository
                    .GetOtpValidateTrueAndUsedFalseByEmployeeIdAsync(userId, empInfo.TenantId);

                if (existingOtpEntry == null)
                {
                    return ApiResponse<UpdateLoginPasswordResponseDTO>.Fail("No OTP found or it has expired.");
                }

                // 🔄 Step 3: Match OTP
                if (existingOtpEntry.Otp != request.dto.Otp)
                {
                    return ApiResponse<UpdateLoginPasswordResponseDTO>.Fail("Invalid OTP entered.");
                }

                // 🔄 Step 4: Check expiry
                if (existingOtpEntry.OtpexpireDateTime <= DateTime.Now)
                {
                    return ApiResponse<UpdateLoginPasswordResponseDTO>.Fail("OTP has expired. Please request a new one.");
                }

                // 🔐 Step 5: Confirm OTP is validated and not used
                if (!existingOtpEntry.IsValidate || existingOtpEntry.IsUsed)
                {
                    return ApiResponse<UpdateLoginPasswordResponseDTO>.Fail("OTP is either not validated or already used.");
                }

                // ✅ Step 6: Set new password
                var loginCredential = new LoginCredential
                {
                    EmployeeId = empId,
                    TenantId = empInfo.TenantId,
                    LoginId = request.dto.LoginId,
                    Password = request.dto.Password
                };

                bool isUpdated = await _unitOfWork.UserLoginRepository.SetNewPassword(loginCredential);

                if (!isUpdated)
                {
                    _logger.LogWarning("Password update failed for LoginId: {LoginId}", loginId);
                    await _unitOfWork.RollbackTransactionAsync();

                    return ApiResponse<UpdateLoginPasswordResponseDTO>.Fail("Password could not be updated. Please try again.");
                }

                // 🔄 Step 7: Mark OTP as used
                existingOtpEntry.IsUsed = true;
                existingOtpEntry.UsedDateTime = DateTime.Now;
                await _unitOfWork.ForgotPasswordOtpRepository.UpdateOTPAsync(existingOtpEntry);
              

                var response = new UpdateLoginPasswordResponseDTO
                {
                    Success = true
                };

                return ApiResponse<UpdateLoginPasswordResponseDTO>.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in SetNewLoginPasswordCommandHandler.Handle.");
                await _unitOfWork.RollbackTransactionAsync();

                return ApiResponse<UpdateLoginPasswordResponseDTO>.Fail("An error occurred while setting the password. Please try again later.");
            }
        }

    }

}