using AutoMapper;
using ems.application.DTOs.EmailTemplate;
using ems.application.Features.EmailTemplateCmd.Queries;
using ems.application.Interfaces;
using ems.application.Wrappers;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ems.application.Features.EmailTemplateCmd.Handlers
{
    public class GetEmailTemplateByCodeQueryHandler : IRequestHandler<GetEmailTemplateByCodeQuery, ApiResponse<List<EmailTemplateDTO>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetEmailTemplateByCodeQueryHandler> _logger;

        public GetEmailTemplateByCodeQueryHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<GetEmailTemplateByCodeQueryHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<List<EmailTemplateDTO>>> Handle(GetEmailTemplateByCodeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var templates = await _unitOfWork.EmailTemplateRepository.GetTemplateByCodeAsync(request.Code);

                if (templates == null || templates.Count == 0)
                {
                    _logger.LogWarning("No email templates found for code: {Code}", request.Code);

                    return new ApiResponse<List<EmailTemplateDTO>>
                    {
                        IsSucceeded = false,
                        Message = $"No templates found for code: {request.Code}",
                        Data = null
                    };
                }

                var templateDTOs = _mapper.Map<List<EmailTemplateDTO>>(templates);

                _logger.LogInformation("Successfully retrieved {Count} email templates for code: {Code}", templateDTOs.Count, request.Code);

                return new ApiResponse<List<EmailTemplateDTO>>
                {
                    IsSucceeded = true,
                    Message = "Email templates fetched successfully.",
                    Data = templateDTOs
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching email templates for code: {Code}", request.Code);

                return new ApiResponse<List<EmailTemplateDTO>>
                {
                    IsSucceeded = false,
                    Message = "An error occurred while fetching email templates.",
                    Data = null
                };
            }
        }
    }
}
 