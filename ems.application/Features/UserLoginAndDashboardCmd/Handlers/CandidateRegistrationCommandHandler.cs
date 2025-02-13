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
  
     //public class CandidateRegistrationCommandHandler : IRequestHandler<CandidateRegistrationCommand, ApiResponse<CandidateResponseDTO>>
    //{
    //    private readonly IMapper _mapper;
    //    private readonly IUnitOfWork _unitOfWork;
    //    private readonly ILogger<CandidateRegistrationCommandHandler> _logger;

    //    public CandidateRegistrationCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<CandidateRegistrationCommandHandler> logger)
    //    {
    //        _mapper = mapper;
    //        _unitOfWork = unitOfWork;
    //        _logger = logger;
    //    }

    //    public async Task<ApiResponse<CandidateResponseDTO>> Handle(CandidateRegistrationCommand request, CancellationToken cancellationToken)
    //    {
    //        try
    //        {

    //            CandidateResponseDTO candidateResponse = new CandidateResponseDTO();
    //            // Validate the request
    //            if (request == null || request.RequestCandidateRegistrationDTO == null)
    //            {
    //                candidateResponse.Success = false;
    //                candidateResponse.CandidateId = 0;
    //                candidateResponse.Message = "Invalid request or missing CandidateRegistrationDTO.";
    //                return new ApiResponse<CandidateResponseDTO>
    //                {
    //                    IsSuccecced = false,
    //                    Message = "fail",
    //                    Data = candidateResponse
    //                };
    //            }

    //            CandidateRequestDTO candidateResponseDTO = _mapper.Map<CandidateRequestDTO>(request.RequestCandidateRegistrationDTO);
              
    //            // Check for duplicate data
    //            var isBlacklistedOrDuplicate = await _unitOfWork.CandidatesRegistrationRepository.IsEmailPANAdharPhoneExistsAsync(candidateResponseDTO);
               
    //            if (isBlacklistedOrDuplicate)
    //            {
    //                candidateResponse.Success = false;
    //                candidateResponse.CandidateId = 0;
    //                candidateResponse.Message = "Data is already exist";
    //                return new ApiResponse<CandidateResponseDTO>
    //                {
    //                    IsSuccecced = false,
    //                    Message = "fail",
    //                     Data = candidateResponse

    //                };
    //            }
    //            else
    //            {
    //                List<int> skillSetList = candidateResponseDTO.SkillSet.Split(',').Select(int.Parse).ToList();
    //                Candidate cc = _mapper.Map<Candidate>(request.RequestCandidateRegistrationDTO);
    //                var id =  await _unitOfWork.CandidatesRegistrationRepository.AddCandidateAsync(cc);
    //                List<CandidateCategorySkill> candidateSkillsList = skillSetList.Select(skill => new CandidateCategorySkill
    //                {
    //                    CandidateId = id,      // Fixed candidate ID
    //                    CategoryId = skill,              // SkillSet ka current item
    //                    AddedDateTime = DateTime.Now,     // Current DateTime
    //                    IsActive = true                   // Default true
    //                }).ToList();
    //               var numberOfRecordInserted= await _unitOfWork.CandidateCategorySkillRepository.AddSkillsAsync(candidateSkillsList);
    //                 // var status= _unitOfWork.CommitAsync();
    //                  candidateResponse.Success = true;
    //                  candidateResponse.CandidateId = id;
    //                  candidateResponse.Message = "Candidate registration successful.";
                    
    //                return new ApiResponse<CandidateResponseDTO>
    //                {
    //                    IsSuccecced = true,
    //                    Message = "success",
    //                    Data = candidateResponse
    //                };
    //            }

    //            //  await _unitOfWork.CommitAsync(cancellationToken);



               
    //        }
    //        catch (Exception ex)
    //        {
    //            // Log the error
    //            _logger.LogError(ex, "An error occurred while processing the registration request.");

    //            // Return a failure response
    //            return new ApiResponse<CandidateResponseDTO>
    //            {

    //                IsSuccecced = false,
    //                Message = "An error occurred while processing the request. Please try again later."
    //            };
    //        }
    //    }
    //}
}
