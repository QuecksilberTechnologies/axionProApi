using AutoMapper;
using ems.application.DTOs.Employee;
using ems.application.Features.EmployeeCmd.Commands;
using ems.application.Interfaces.IRepositories;
using ems.application.Interfaces;
using ems.domain.Entity;
using MediatR;
using ems.application.Wrappers;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ems.application.Constants;
using ems.application.Interfaces.ITokenService;
using ems.application.Interfaces.IEmail;

namespace ems.application.Features.EmployeeCmd.Handlers
{
    public class CreateEmployeeByTenantAdminCommandHandler : IRequestHandler<CreateEmployeeByTenantAdminCommand, ApiResponse<long>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICommonRepository _iCommonRepository;
        private readonly ILogger<CreateEmployeeByTenantAdminCommandHandler> _logger;
        private readonly INewTokenRepository _tokenService;
        private readonly IEmailService _emailService;

        public CreateEmployeeByTenantAdminCommandHandler(
            IEmployeeRepository employeeRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor,
            ILogger<CreateEmployeeByTenantAdminCommandHandler> logger,
            ICommonRepository iCommonRepository, INewTokenRepository tokenService, IEmailService emailService)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _iCommonRepository = iCommonRepository;
            _tokenService = tokenService;
            _emailService = emailService;
        }

        public async Task<ApiResponse<long>> Handle(CreateEmployeeByTenantAdminCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                // 🔐 Get current logged-in userId from JWT token
                Claim? userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null)
                {
                    return ApiResponse<long>.Fail("Unauthorized: User ID not found in token.");
                }
               var userIdClaimValue = "embedded.deepesh@gmail.com";
                // 🧠 Validate user via repository
                long empId = await _unitOfWork.CommonRepository.ValidateActiveUserLoginOnlyAsync(userIdClaimValue);
                _logger.LogInformation("Validation result for LoginId {LoginId}: EmployeeId = {empId}", userIdClaimValue, empId);

                if (empId < 1)
                {
                    _logger.LogWarning("User validation failed for LoginId: {LoginId}", userIdClaimValue);
                    await _unitOfWork.RollbackTransactionAsync();
                    return ApiResponse<long>.Fail("User is not authenticated or authorized to perform this action.");
                }

                // 🏗️ Map DTO to Entity
                var employeeEntity = _mapper.Map<Employee>(request.Dto);
                employeeEntity.AddedById = empId;

                long employeeId = await _employeeRepository.AddEmployeeByAdminAsync(employeeEntity);

                var loginCredential = new LoginCredential
                {
                    TenantId = request.Dto.TenantId,
                    LoginId = request.Dto.OfficialEmail,
                    EmployeeId = employeeId,
                    IsActive = ConstantValues.IsByDefaultTrue,
                    Password = ConstantValues.DefaultPassword,
                    HasFirstLogin = ConstantValues.IsByDefaultTrue,
                    IsSoftDeleted = ConstantValues.IsByDefaultFalse,
                    //Remark = ConstantValues.TenantAdminRoleRemark,
                    AddedById = empId,
                    AddedDateTime = DateTime.Now,
                    UpdatedById = ConstantValues.SystemUserIdByDefaultZero,
                    UpdatedDateTime = null,
                    DeletedById = ConstantValues.SystemUserIdByDefaultZero,
                    DeletedDateTime = null

                };
                long newLoginId = await _unitOfWork.UserLoginRepository.CreateUser(loginCredential);
                    if(newLoginId <= 0 )               
                      await _unitOfWork.RollbackTransactionAsync();
                // Step 10: Generate Token for Email
                string token = await _tokenService.GenerateToken(loginCredential.LoginId);

                //eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJzdHJpbmciLCJuYmYiOjE3NDkxNTQzMDgsImV4cCI6MTc0OTE1NjEwOCwiaWF0IjoxNzQ5MTU0MzA4LCJpc3MiOiJFTVNBcHAiLCJhdWQiOiJFTVNVc2VycyJ9.Bm_OOPQwqeJx8Qpz2h6lG4G7 - y9a7oWuxJmiLKY5E1Y
                // await _unitOfWork.CommitTransactionAsync();

                // Assume request.TenantCreateRequestDTO ke andar ye fields available hain
                var firstName = request.Dto.FirstName+ " " + request.Dto.MiddleName + " " + request.Dto.LastName;
                 
                var email =  loginCredential.LoginId;
                var loginId = loginCredential.LoginId; // assuming loginId = email

                var placeholderData = new Dictionary<string, string>
                                           {
                                             { "UserName", $"{firstName}" },
                                              { "LoginId", loginId }
                                            };


                // Call the templated email service
                //var sent =   await _emailService.SendTemplatedEmailAsync(
                //     templateCode: "WELCOME_EMAIL",
                //     toEmail: email,
                //     TenantId: tenantEntity.Id,
                //     placeholders: placeholderData
                //  );

                //string toEmail = "mca.deepesh@gmail.com";
                //string subject = "Verification Email";
                //string userName = "Deepesh Gupta";

                //long? TenantId_ = 17;


                string emailBodyTemplate = @"
                           <html>
                             <body style='font-family: Arial, sans-serif; padding: 20px;'>
                                   <h2 style='color: #2E86C1;'>Dear {{UserName}},</h2>
                                        <p style='font-size: 16px;'>
                                         This is a <strong>verification link</strong> sent by <em>Axion-Pro</em>.
                                         The link will expire in <strong>5 minutes</strong>.
                                        </p>
                                     <p>
                                      <a href='http://localhost:4200/registration-verify?token={{Token}}'
                                      style='display:inline-block; background-color:#2E86C1; color:white; padding:10px 15px; text-decoration:none; border-radius:5px;'>
                                      Click here to verify
                                      </a>
                                  </p>
                              <p style='font-size: 14px; color: gray;'>
                               Regards,<br/>
                              <b>Axion-Pro Team</b>
                                 </p>
                                </body>
                          </html>"
                ;

                bool isSent = await _emailService.SendEmailAsync(
                    toEmail: email,
                    subject: "Verification Email",
                    body: emailBodyTemplate,
                    token: token,
                    TenantId: 17
                );
                if ( !isSent )
                    await _unitOfWork.RollbackTransactionAsync();
                    await _unitOfWork.CommitTransactionAsync();

                return ApiResponse<long>.Success(employeeId, "Employee and LoginId successfully.");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error while creating employee.");
                return ApiResponse<long>.Fail("Failed to create employee.", new List<string> { ex.Message });
            }
        }
    }
}
