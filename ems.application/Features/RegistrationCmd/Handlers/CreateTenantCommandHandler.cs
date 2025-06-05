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

namespace ems.application.Features.RegistrationCmd.Handlers
{
    public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, ApiResponse<TenantCreateResponseDTO>>
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateTenantCommandHandler> _logger;

        public CreateTenantCommandHandler(
            ITenantRepository tenantRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<CreateTenantCommandHandler> logger)
        {
            _tenantRepository = tenantRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<ApiResponse<TenantCreateResponseDTO>> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {
            try
            {
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

                var tenantEntity = _mapper.Map<Tenant>(request.TenantCreateRequestDTO);

                // 👇 Add Employee into tenantEntity directly
                tenantEntity.Employees.Add(new Employee
                {  
                    TenantId = tenantEntity.Id,
                    OfficialEmail = tenantEntity.TenantEmail,
                    HasPermanent = true,
                    IsActive = true,
                    AddedById = tenantEntity.Id,
                    AddedDateTime = DateTime.Now,
                });

                tenantEntity.TenantProfiles.Add(new TenantProfile
                {
                    TenantId = tenantEntity.Id,

                });



                await _unitOfWork.BeginTransactionAsync();

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



                var createdEmployee = tenantEntity.Employees.FirstOrDefault(); // 👈
                //    var loginInfo = _mapper.Map<LoginCredential>(createdEmployee);
                var loginCredential = new LoginCredential
                {
                    TenantId = tenantEntity.Id,
                    LoginId = tenantEntity.TenantEmail,
                    EmployeeId = createdEmployee?.Id ?? 0,
                    IsActive = true,
                    Password = "123"
                };

                long newLoginId = await _unitOfWork.UserLoginRepository.CreateUser(loginCredential);

                var tenantProfile = tenantEntity.TenantProfiles.FirstOrDefault(); // 👈

                long newTenantProfileId = await _unitOfWork.TenantRepository.AddTenantProfileAsync(tenantProfile);


                UserRole userRole = new UserRole
                {
                    EmployeeId = createdEmployee?.Id ?? 0,
                    RoleId = 49,
                    IsPrimaryRole = true,
                    IsActive = true,
                    AddedById = createdEmployee?.Id ?? 0,
                    AddedDateTime = DateTime.Now,
                    AssignedDateTime = DateTime.Now

                };

                var roleId=  await _unitOfWork.UserRoleRepository.AddUserRoleAsync(userRole);



                await _unitOfWork.CommitTransactionAsync();

                return new ApiResponse<TenantCreateResponseDTO>
                {
                    IsSucceeded = true,
                    Message = "employee registration successful.",
                    Data = new TenantCreateResponseDTO
                    {
                        Success = true,
                        EmployeeId = createdEmployee?.Id ?? 0,
                        TenantId = newTenantId,
                        TenantProfileId =newTenantProfileId,
                        LoginId = tenantEntity.TenantEmail,
                        EmailSent = true,
                        Password = "123",
                        RoleId= roleId,

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
