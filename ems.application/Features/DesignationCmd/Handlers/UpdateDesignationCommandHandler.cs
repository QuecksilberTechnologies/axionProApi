using AutoMapper;
using ems.application.Features.OperationCmd.Commands;
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
    public class UpdateDesignationCommandHandler : IRequestHandler<UpdateDesignationCommand, ApiResponse<bool>>
    {
        private readonly IDesignationRepository designationRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateDesignationCommandHandler(IDesignationRepository designationRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.designationRepository = designationRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<bool>> Handle(UpdateDesignationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // ✅ Step 1: TenantId Validation
                if (request.dto.TenantId <= 0)
                {
                    return new ApiResponse<bool>
                    {
                        IsSucceeded = false,
                        Message = "Valid TenantId is required.",
                        Data = false
                    };
                }

                // ✅ Step 2: Trim and Validate DesignationName
                string designationName = request.dto.DesignationName?.Trim();

                if (string.IsNullOrWhiteSpace(designationName))
                {
                    return new ApiResponse<bool>
                    {
                        IsSucceeded = false,
                        Message = "Designation name should not be empty or whitespace.",
                        Data = false
                    };
                }

                // Set trimmed value back
                request.dto.DesignationName = designationName;
                // ✅ Step 3: Check for duplicate name (excluding same ID)
                //bool isDuplicate = await designationRepository.CheckDuplicateValueAsync(
                //    request.dto.TenantId,
                //    designationName
                     
                //);

                //if (isDuplicate)
                //{
                //    return new ApiResponse<bool>
                //    {
                //        IsSucceeded = false,
                //        Message = "This designation name already exists.",
                //        Data = false
                //    };
                //}

                // ✅ Step 4: Map and Update
                Designation designation = _mapper.Map<Designation>(request.dto);
                bool isUpdated = await designationRepository.UpdateDesignationAsync(designation);

                if (!isUpdated)
                {
                    return new ApiResponse<bool>
                    {
                        IsSucceeded = false,
                        Message = "No designation was updated.",
                        Data = false
                    };
                }

                return new ApiResponse<bool>
                {
                    IsSucceeded = true,
                    Message = "Designation updated successfully.",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error occurred while updating designation.");
                return new ApiResponse<bool>
                {
                    IsSucceeded = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = false
                };
            }
        }


    }
}
