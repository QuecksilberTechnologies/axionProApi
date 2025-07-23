using AutoMapper;
 
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
using ems.application.Features.DesignationCmd.Commands;
using ems.application.DTOs.Designation;

namespace ems.application.Features.DesignationCmd.Handlers
{
    public class CreateDesignationCommandHandler : IRequestHandler<CreateDesignationCommand, ApiResponse<List<GetAllDesignationDTO>>>
    {
        private readonly IDesignationRepository designationRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateDesignationCommandHandler(IDesignationRepository designationRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.designationRepository = designationRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<List<GetAllDesignationDTO>>> Handle(CreateDesignationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.dto.TenantId <= 0)
                {
                    return new ApiResponse<List<GetAllDesignationDTO>>
                    {
                        IsSucceeded = false,
                        Message = "TenantId is not valid.",
                        Data = null
                    };
                }

                // 🧼 Trim DesignationName & validate
                string? designationName = request.dto.DesignationName?.Trim();

                if (string.IsNullOrWhiteSpace(designationName))
                {
                    return new ApiResponse<List<GetAllDesignationDTO>>
                    {
                        IsSucceeded = false,
                        Message = "Designation name should not be empty or whitespace.",
                        Data = null
                    };
                }

                // 🔄 Use trimmed name for processing
                request.dto.DesignationName = designationName;

                bool isDuplicate = await designationRepository
                    .CheckDuplicateValueAsync(request.dto.TenantId, designationName);

                if (isDuplicate)
                {
                    return new ApiResponse<List<GetAllDesignationDTO>>
                    {
                        IsSucceeded = false,
                        Message = "This designation name already exists.",
                        Data = null
                    };
                }

                Designation designation = _mapper.Map<Designation>(request.dto);

                List<Designation> designations = await designationRepository.CreateDesignationAsync(designation);

                if (designations == null || !designations.Any())
                {
                    return new ApiResponse<List<GetAllDesignationDTO>>
                    {
                        IsSucceeded = false,
                        Message = "No designation was created.",
                        Data = null
                    };
                }

                List<GetAllDesignationDTO> getAllDesignationDTOs = _mapper.Map<List<GetAllDesignationDTO>>(designations);

                return new ApiResponse<List<GetAllDesignationDTO>>
                {
                    IsSucceeded = true,
                    Message = "Designation created successfully.",
                    Data = getAllDesignationDTOs
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<GetAllDesignationDTO>>
                {
                    IsSucceeded = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }


    }


}
