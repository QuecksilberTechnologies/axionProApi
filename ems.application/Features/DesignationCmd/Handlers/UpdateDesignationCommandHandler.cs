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
    public class UpdateDesignationCommandHandler : IRequestHandler<UpdateDesignationCommand, ApiResponse<List<GetAllDesignationDTO>>>
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

        public async Task<ApiResponse<List<GetAllDesignationDTO>>> Handle(UpdateDesignationCommand request, CancellationToken cancellationToken)
        {

            try
            {

                Designation designation = _mapper.Map<Designation>(request.updateDesignationDTO);
                List<Designation> designations = await designationRepository.UpdateDesignationAsync(designation);

                if (designations == null || !designations.Any())
                {
                    return new ApiResponse<List<GetAllDesignationDTO>>
                    {
                        IsSucceeded = false,
                        Message = "No operation were updated.",
                        Data = new List<GetAllDesignationDTO>()
                    };
                }

                List<GetAllDesignationDTO> getAllDesignationDTOs = _mapper.Map<List<GetAllDesignationDTO>>(designations);

                return new ApiResponse<List<GetAllDesignationDTO>>
                {
                    IsSucceeded = true,
                    Message = "Travel created successfully",
                    Data = getAllDesignationDTOs
                };
            }
            catch (Exception ex)
            {
                //  _logger.LogError(ex, "Error occurred while Updatiing Operation.");
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
