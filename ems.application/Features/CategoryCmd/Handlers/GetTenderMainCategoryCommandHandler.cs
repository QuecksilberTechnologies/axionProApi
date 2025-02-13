using AutoMapper;
using ems.application.DTOs.CategoryDTO;
using ems.application.Features.CategoryCmd.Command;
using ems.application.Interfaces;
using ems.application.Wrappers;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.CategoryCmd.Handlers
{
    
      public class GetTenderMainCategoryRequestHandler : IRequestHandler<GetTenderMainCategoryCommand, ApiResponse<List<TenderCategoryResponseDTO>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetTenderMainCategoryRequestHandler> _logger;

        public GetTenderMainCategoryRequestHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<GetTenderMainCategoryRequestHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        //public GetTenderMainCategoryRequestHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<GetTenderMainCategoryRequestHandler> logger)
        //{
        //    _mapper = mapper;
        //    _unitOfWork = unitOfWork;
        //    _logger = logger;
        //}

        public Task<ApiResponse<List<TenderCategoryResponseDTO>>> Handle(GetTenderMainCategoryCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
