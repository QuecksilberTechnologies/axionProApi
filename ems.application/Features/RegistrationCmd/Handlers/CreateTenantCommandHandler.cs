using AutoMapper;
using ems.application.DTOs.Registration;
using ems.application.Features.RegistrationCmd.Commands;
using ems.application.Interfaces.IRepositories;
using ems.application.Interfaces;
using ems.domain.Entity;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using ems.application.DTOs.UserLogin;
using ems.application.Wrappers;
using Microsoft.Extensions.Logging;
using ems.application.Features.UserLoginAndDashboardCmd.Handlers;
using ems.application.Interfaces.ITokenService;
using ems.application.Constants;
using Microsoft.AspNetCore.Identity.Data;
using ems.application.Interfaces.IEmail;

namespace ems.application.Features.RegistrationCmd.Handlers
{
    public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, ApiResponse<TenantCreateResponseDTO>>
    {
        private readonly IEmailService _emailService;
        private readonly ITenantRepository _tenantRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateTenantCommandHandler> _logger;
        private readonly INewTokenRepository _tokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
       
        
        public CreateTenantCommandHandler(
            ITenantRepository tenantRepository, INewTokenRepository tokenService, IRefreshTokenRepository refreshTokenRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork, 
            ILogger<CreateTenantCommandHandler> logger , IEmailService emailService)

           {
             _emailService = emailService;
            _tenantRepository = tenantRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _tokenService = tokenService;
            _refreshTokenRepository = refreshTokenRepository;
           }
        public async Task<ApiResponse<TenantCreateResponseDTO>> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Step 1: Check if Tenant already exists by email
                bool isEmailExists = await _unitOfWork.TenantRepository.CheckTenantByEmail(request.TenantCreateRequestDTO.TenantEmail);
                if (isEmailExists)
                {
                    return new ApiResponse<TenantCreateResponseDTO>
                    {
                        IsSucceeded = false,
                        Message = "Tenant with this email already exists.",
                        Data = null
                    };
                }

                // Step 2: Map DTO to Entity
                var tenantEntity = _mapper.Map<Tenant>(request.TenantCreateRequestDTO);

                // Step 3: Begin Transaction
                await _unitOfWork.BeginTransactionAsync();

                // Step 4: Add Tenant
                long newTenantId = await _unitOfWork.TenantRepository.AddTenantAsync(tenantEntity);
                if (newTenantId <= 0)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return new ApiResponse<TenantCreateResponseDTO>
                    {
                        IsSucceeded = false,
                        Message = "Tenant creation failed.",
                        Data = null
                    };
                }

                // Step 5: Create Employee for Tenant
                var employee = new Employee
                {
                    TenantId = newTenantId,
                    OfficialEmail = tenantEntity.TenantEmail,
                    HasPermanent = ConstantValues.IsByDefaultTrue,
                    IsActive = ConstantValues.IsByDefaultTrue,
                    AddedById = newTenantId,
                    AddedDateTime = DateTime.Now,
                    UpdatedById = ConstantValues.SystemUserIdByDefaultZero,
                    UpdatedDateTime = null,
                    DeletedById = ConstantValues.SystemUserIdByDefaultZero,
                    DeletedDateTime = null,
                    IsSoftDeleted = ConstantValues.IsByDefaultFalse,
                };

                var createdEmployee = await _unitOfWork.Employees.AddAsync(employee);
                long employeeId = createdEmployee?.Id ?? 0;

                if (employeeId == 0)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return new ApiResponse<TenantCreateResponseDTO>
                    {
                        IsSucceeded = false,
                        Message = "Employee creation failed.",
                        Data = null
                    };
                }

                // Step 6: Create TenantProfile
                var tenantProfile = new TenantProfile
                {
                    TenantId = newTenantId
                };
                long newTenantProfileId = await _unitOfWork.TenantRepository.AddTenantProfileAsync(tenantProfile);

                // Step 7: Create Admin Role
                var role = new Role
                {
                    TenantId = newTenantId,
                    RoleName = ConstantValues.AdminRoleName,
                    IsActive = ConstantValues.IsByDefaultTrue,
                    IsSoftDeleted = ConstantValues.IsByDefaultFalse,
                    Remark = ConstantValues.AdminRoleRemark,
                    AddedById = newTenantId,
                    AddedDateTime = DateTime.Now,
                    UpdatedById = ConstantValues.SystemUserIdByDefaultZero,
                    UpdatedDateTime = null,
                    DeletedById = ConstantValues.SystemUserIdByDefaultZero,
                    DeletedDateTime = null
                };
                var createdRole = await _unitOfWork.RoleRepository.AutoCreatedForTenantRoleAsync(role);

                // Step 8: Create LoginCredential
                var loginCredential = new LoginCredential
                {
                    TenantId = newTenantId,
                    LoginId = tenantEntity.TenantEmail,
                    EmployeeId = employeeId,
                    IsActive = ConstantValues.IsByDefaultTrue,
                    Password = ConstantValues.DefaultPassword,
                    HasFirstLogin = ConstantValues.IsByDefaultTrue,
                    IsSoftDeleted = ConstantValues.IsByDefaultFalse,
                    Remark = ConstantValues.AdminRoleRemark,
                    AddedById = newTenantId,
                    AddedDateTime = DateTime.Now,
                    UpdatedById = ConstantValues.SystemUserIdByDefaultZero,
                    UpdatedDateTime = null,
                    DeletedById = ConstantValues.SystemUserIdByDefaultZero,
                    DeletedDateTime = null 

                };
                long newLoginId = await _unitOfWork.UserLoginRepository.CreateUser(loginCredential);

                // Step 9: Assign Role to Employee
                UserRole userRole = new UserRole
                {
                    EmployeeId = employeeId,
                    RoleId = createdRole.Id,
                    IsPrimaryRole = ConstantValues.IsByDefaultTrue,
                    IsActive = ConstantValues.IsByDefaultTrue,
                    AddedById = employeeId,
                    AddedDateTime = DateTime.Now,
                    AssignedDateTime = DateTime.Now,
                    Remark = ConstantValues.AdminRoleRemark,
                    AssignedById = employeeId,
                    RoleStartDate = ConstantValues.SystemOnlyTodaysDate,
                    ApprovalRequired = ConstantValues.IsByDefaultFalse,
                    UpdatedById = ConstantValues.SystemUserIdByDefaultZero,
                    UpdatedDateTime = null,
                    DeletedById = ConstantValues.SystemUserIdByDefaultZero,
                    DeletedDateTime = null
                };
                int roleId = (int)await _unitOfWork.UserRoleRepository.AddUserRoleAsync(userRole);

                // Step 10: Generate Token for Email
                string token = await _tokenService.GenerateToken(tenantEntity.TenantEmail);

                //eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJzdHJpbmciLCJuYmYiOjE3NDkxNTQzMDgsImV4cCI6MTc0OTE1NjEwOCwiaWF0IjoxNzQ5MTU0MzA4LCJpc3MiOiJFTVNBcHAiLCJhdWQiOiJFTVNVc2VycyJ9.Bm_OOPQwqeJx8Qpz2h6lG4G7 - y9a7oWuxJmiLKY5E1Y
                // await _unitOfWork.CommitTransactionAsync();

                // Assume request.TenantCreateRequestDTO ke andar ye fields available hain
                var firstName = request.TenantCreateRequestDTO.ContactPersonName;

                var email = request.TenantCreateRequestDTO.TenantEmail;
                var loginId = request.TenantCreateRequestDTO.TenantEmail; // assuming loginId = email

                var placeholderData = new Dictionary<string, string>
                                           {
                                             { "UserName", $"{firstName}" },
                                              { "LoginId", loginId }
                                            };

                // Call the templated email service
                //var sent =   await _emailService.SendTemplatedEmailAsync(
                //     templateCode: "WELCOME_EMAIL",
                //     toEmail: email,
                //     tenantId: tenantEntity.Id,
                //     placeholders: placeholderData
                //  );

                string toEmail = "mca.deepesh@gmail.com";
                string subject = "Verification Email";
                string userName = "Deepesh Gupta";

                long tenantId_ = 17;


                string emailBodyTemplate = @"
                           <html>
                             <body style='font-family: Arial, sans-serif; padding: 20px;'>
                                   <h2 style='color: #2E86C1;'>Dear {{UserName}},</h2>
                                        <p style='font-size: 16px;'>
                                         This is a <strong>verification link</strong> sent by <em>Axion-Pro</em>.
                                         The link will expire in <strong>5 minutes</strong>.
                                        </p>
                                     <p>
                                      <a href='http://localhost:4200/registration-profile/registration-policies?token={{Token}}'
                                      style='display:inline-block; background-color:#2E86C1; color:white; padding:10px 15px; text-decoration:none; border-radius:5px;'>
                                      Click here to verify
                                      </a>
                                  </p>
                              <p style='font-size: 14px; color: gray;'>
                               Regards,<br/>
                              <b>Axion-Pro Team</b>
                                 </p>
                                </body>
                          </html>";

                bool isSent = await _emailService.SendEmailAsync(
                    toEmail: tenantEntity.TenantEmail,
                    subject: "Verification Email",
                    body: emailBodyTemplate,
                    token: token,
                    tenantId: 17
                );
                // Step 12: Commit Transaction
                await _unitOfWork.CommitTransactionAsync();

                // Step 13: Return Response
                return new ApiResponse<TenantCreateResponseDTO>
                {
                    IsSucceeded = true,
                    Message = "Employee registration successful.",
                    Data = new TenantCreateResponseDTO
                    {
                        Success = true,
                        EmployeeId = employeeId,
                        TenantId = newTenantId,
                        TenantProfileId = newTenantProfileId,
                        LoginId = tenantEntity.TenantEmail,
                        EmailSent = isSent,
                        Password = ConstantValues.DefaultPassword,
                        RoleId = roleId
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating tenant.");
                await _unitOfWork.RollbackTransactionAsync();

                return new ApiResponse<TenantCreateResponseDTO>
                {
                    IsSucceeded = false,
                    Message = $"An error occurred while creating tenant: {ex.Message}",
                    Data = null
                };
            }
        }

    }
}
