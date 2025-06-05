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

                Designation designation = _mapper.Map<Designation>(request.createDesignationDTO);
                List<Designation> designations = await designationRepository.CreateDesignationAsync(designation);

                if (designations == null || !designations.Any())
                {
                    return new ApiResponse<List<GetAllDesignationDTO>>
                    {
                        IsSucceeded = false,
                        Message = "No Travel were created.",
                        Data = new List<GetAllDesignationDTO>()
                    };
                }

                List<GetAllDesignationDTO> getAllDesignationDTOs = _mapper.Map<List<GetAllDesignationDTO>>(designations);

                return new ApiResponse<List<GetAllDesignationDTO>>
                {
                    IsSucceeded = true,
                    Message = "designation created successfully",
                    Data = getAllDesignationDTOs
                };
            }
            catch (Exception ex)
            {
                //  _logger.LogError(ex, "Error occurred while creating role.");
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
