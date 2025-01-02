using AutoMapper;
using ems.application.DTOs.RegistrationDTO;
using ems.application.Interfaces.IRepositories;
using ems.application.Interfaces.ITokenService;
using ems.application.Interfaces;
using ems.application.Wrappers;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using ems.application.Features.UserLoginAndDashboardCmd.Commands;
using ems.domain.Entity;

namespace ems.application.Features.UserLoginAndDashboardCmd.Handlers
{
    public class CandidateRegistrationCommandHandler : IRequestHandler<CandidateRegistrationCommand, ApiResponse<CandidateResponseDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CandidateRegistrationCommandHandler> _logger;

        public CandidateRegistrationCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<CandidateRegistrationCommandHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<CandidateResponseDTO>> Handle(CandidateRegistrationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Validate the request
                if (request == null || request.RequestCandidateRegistrationDTO == null)
                {
                    return new ApiResponse<CandidateResponseDTO>
                    {
                        IsSuccecced = false,
                        Message = "Invalid request or missing CandidateRegistrationDTO."
                    };
                }

                 


                CandidateRequestDTO dto = _mapper.Map<CandidateRequestDTO>(request);


                //CandidateResponseDTO candidateResponseDTO = _mapper.Map<CandidateResponseDTO>(request.RequestCandidateRegistrationDTO);

                // Check for duplicate data
                var isDuplicate = await _unitOfWork.CandidatesRegistration.IsEmailPANAdharPhoneExistsAsync(dto);
                 

                if (isDuplicate)
                {
                    return new ApiResponse<CandidateResponseDTO>
                    {
                        IsSuccecced = false,
                        Message = "Duplicate candidate data found. Registration cannot proceed."
                    };
                }


                // Save the candidate data
                var candidate = _mapper.Map<Candidate>(dto);
                candidate.IsActive = true;

             //   await _unitOfWork.CandidatesRegistration.AddAsync(candidate, cancellationToken);
              //  await _unitOfWork.CommitAsync(cancellationToken);

                var responseDTO = _mapper.Map<CandidateResponseDTO>(candidate);

                return new ApiResponse<CandidateResponseDTO>
                {
                    IsSuccecced = true,
                    Message = "Candidate registration successful.",
                    Data = responseDTO
                };
            }
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "An error occurred while processing the registration request.");

                // Return a failure response
                return new ApiResponse<CandidateResponseDTO>
                {
                    IsSuccecced = false,
                    Message = "An error occurred while processing the request. Please try again later."
                };
            }
        }
    }
}
