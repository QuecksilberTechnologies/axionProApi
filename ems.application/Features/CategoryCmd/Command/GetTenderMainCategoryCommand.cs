using ems.application.DTOs.CategoryDTO;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.CategoryCmd.Command
{
    public class GetTenderMainCategoryCommand :IRequest<ApiResponse<List<TenderCategoryResponseDTO>>>
    {
        public TenderCategoryRequestDTO TenderCategoryRequestDTO { get; set; }


        public GetTenderMainCategoryCommand(TenderCategoryRequestDTO tenderCategoryRequestDTO)
        {
            TenderCategoryRequestDTO = tenderCategoryRequestDTO;
        }
    }
}
