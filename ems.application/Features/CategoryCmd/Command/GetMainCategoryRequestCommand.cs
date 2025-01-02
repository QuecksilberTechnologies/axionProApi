using ems.application.DTOs.AttendanceDTO;
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
    public class GetMainCategoryRequestCommand : IRequest<ApiResponse<List<CategoryResponseDTO>>>
    {
        public CategoryRequestDTO CategoryRequestDTO { get; set; }


        public GetMainCategoryRequestCommand(CategoryRequestDTO categoryRequestDTO)
        {
            CategoryRequestDTO = categoryRequestDTO;
        }

    }

}