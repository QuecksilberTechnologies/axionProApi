using AutoMapper;
using ems.application.DTOs.Region;
using ems.application.Features.RegionCmd.Queries;
using ems.application.Interfaces;
using ems.application.Wrappers;
using ems.domain.Entity;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.RegionCmd.Handlers
{
    public class GetAllCountryHandler : IRequestHandler<GetAllCountryQuery, ApiResponse<List<GetAllCountryDTO>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllCountryHandler> _logger;

        public GetAllCountryHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<GetAllCountryHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<ApiResponse<List<GetAllCountryDTO>>> Handle(GetAllCountryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // ✅ Correcting the method call

                List<Country> countries = await _unitOfWork.CountryRepository.GetAllAsync();

                var getAllOperationDTOs = _mapper.Map<List<GetAllCountryDTO>>(countries);

                _logger.LogInformation("Successfully retrieved {Count} Operations.", getAllOperationDTOs.Count);
                return new ApiResponse<List<GetAllCountryDTO>>
                {
                    IsSucceeded = true,
                    Message = "Country fetched successfully.",
                    Data = getAllOperationDTOs
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Operations.");
                return new ApiResponse<List<GetAllCountryDTO>>
                {
                    IsSucceeded = false,
                    Message = "Country not fetched .",
                    Data = null
                };
            }
        }



    }
}
