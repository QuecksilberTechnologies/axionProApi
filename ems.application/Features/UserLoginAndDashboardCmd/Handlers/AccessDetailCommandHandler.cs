using AutoMapper;
using ems.application.DTOs.UserLogin;
using ems.application.Features.UserLoginAndDashboardCmd.Commands;
using ems.application.Interfaces.IRepositories;
using ems.application.Interfaces.ITokenService;
using ems.application.Interfaces;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ems.application.Constants;
using FluentValidation;

namespace ems.application.Features.UserLoginAndDashboardCmd.Handlers
{
    public class AccessDetailCommandHandler : IRequestHandler<AccessDetailCommand, ApiResponse<AccessDetailResponseDTO>>
    {
        private readonly IAccessDetailRepository accessDetailRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly ITokenService tokenService;

        
        public AccessDetailCommandHandler(IAccessDetailRepository accessDetailRepository, IMapper mapper, IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            this.accessDetailRepository = accessDetailRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.tokenService = tokenService;
        }

       
        public async Task<ApiResponse<AccessDetailResponseDTO>> Handle(AccessDetailCommand? request, CancellationToken cancellationToken)
        {
            var obj = new AccessDetailRequestDTO
            {
             //   EmployeeId = request.RequestLoginDTO.LoginId,
              //  roleInfo = request.RequestLoginDTO.Password
            };

          var ttt=  accessDetailRepository.GetBasicMenuDTO(request.AccessDetailDTO.EmployeeTypeId,2);

            var apiResponse = new ApiResponse<AccessDetailResponseDTO>
            {
                IsSuccecced = true,  // or use your actual success criteria
                Message = "accessDetailResponseDTO"
            };

            // Log successful retrieval
            // _logger?.LogInformation("AccessDetailResponseDTO created successfully");

            // Return the ApiResponse wrapped in a Task
            return apiResponse;


        }
    }
}
