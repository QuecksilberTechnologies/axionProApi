using AutoMapper;
using ems.application.DTOs.Transport;
using ems.application.DTOs.Verify;
using ems.application.Features.TransportCmd.Commands;
using ems.application.Interfaces.IRepositories;
using ems.application.Interfaces;
using ems.application.Wrappers;
using ems.domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ems.application.Features.VerifyEmailCmd.Commands;
using Microsoft.Extensions.Configuration;
using FluentValidation;
using System.Collections;
using System.Xml.Linq;
using ems.application.Interfaces.ITokenService;
using ems.application.Constants;
using ems.application.DTOs.Employee;
using ems.application.DTOs.UserLogin;
using ems.application.DTOs.UserRole;
using ems.application.Features.UserLoginAndDashboardCmd.Commands;
using ems.application.Features.UserLoginAndDashboardCmd.Handlers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ems.application.DTOs.Registration;

namespace ems.application.Features.VerifyEmailCmd.Handlers
{
    public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, ApiResponse<VerifyEmailResponseDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewTokenRepository _tokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ILogger<VerifyEmailCommandHandler> _logger;

        public VerifyEmailCommandHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            INewTokenRepository tokenService,
            IRefreshTokenRepository refreshTokenRepository,
            ILogger<VerifyEmailCommandHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _refreshTokenRepository = refreshTokenRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<VerifyEmailResponseDTO>> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.verifyEmailRequestDTO.Token))
                {
                    return new ApiResponse<VerifyEmailResponseDTO>
                    {
                        IsSucceeded = false,
                        Message = "Token is required.",
                        Data = null
                    };
                }

                string tokenPayload = await _tokenService.GetUserInfoFromToken(request.verifyEmailRequestDTO.Token);

                if (string.IsNullOrEmpty(tokenPayload))
                {
                    return new ApiResponse<VerifyEmailResponseDTO>
                    {
                        IsSucceeded = false,
                        Message = "Invalid or expired token.",
                        Data = null
                    };
                }

                var userInfo = JsonConvert.DeserializeObject<VerifyEmailResponseDTO>(tokenPayload);

                if (userInfo == null || string.IsNullOrEmpty(userInfo.UserId))
                {
                    return new ApiResponse<VerifyEmailResponseDTO>
                    {
                        IsSucceeded = false,
                        Message = "Token does not contain valid user info.",
                        Data = null
                    };
                }

                // ✅ Directly try to get EmployeeId. If not found, email doesn't exist.
                var employeeRecord = await _unitOfWork.UserLoginRepository.GetEmployeeIdByUserLogin(userInfo.UserId);

                if (employeeRecord.EmployeeId == 0)
                {
                    return new ApiResponse<VerifyEmailResponseDTO>
                    {
                        IsSucceeded = false,
                        Message = "Email not found in the system.",
                        Data = null
                    };
                }

                // Set empId in response DTO
               

                Tenant tenant = new Tenant();
                tenant.Id   = employeeRecord.TenantId;
                                

                var tenantResult = await _unitOfWork.TenantRepository.UpdateTenantAsync(tenant);

                if (tenantResult == null)
                {
                    return new ApiResponse<VerifyEmailResponseDTO>
                    {
                        IsSucceeded = false,
                        Message = "Tenant update failed.",
                        Data = null
                    };
                }

                userInfo.EmployeeId = employeeRecord.EmployeeId;
                userInfo.TenantId = employeeRecord.TenantId;

                return new ApiResponse<VerifyEmailResponseDTO>
                {
                    IsSucceeded = true,
                    Message = "Email verified successfully.",
                    Data = userInfo
                };


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in VerifyEmailCommandHandler.Handle");

                await _unitOfWork.RollbackTransactionAsync();

                return new ApiResponse<VerifyEmailResponseDTO>
                {
                    IsSucceeded = false,
                    Message = "An unexpected error occurred during email verification.",
                    Data = null
                };
            }
        }


    }

}
