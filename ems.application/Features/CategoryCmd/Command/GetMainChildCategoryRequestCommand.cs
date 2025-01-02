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
    public class GetMainChildCategoryRequestCommand : IRequest<ApiResponse<List<CategoryResponseDTO>>>
    {
        public CategoryRequestDTO CategoryRequestDTO { get; set; }


        public GetMainChildCategoryRequestCommand(CategoryRequestDTO categoryRequestDTO)
        {
            CategoryRequestDTO = categoryRequestDTO;
        }
     

    }
}
