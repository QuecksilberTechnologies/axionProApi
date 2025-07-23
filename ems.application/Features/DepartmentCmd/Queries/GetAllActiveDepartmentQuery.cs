using ems.application.DTOs.Department;
using ems.application.DTOs.Designation;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.DepartmentCmd.Queries
{
    public class GetAllActiveDepartmentQuery : IRequest<ApiResponse<List<GetAllDepartmentResponseDTO>>>
    {
        public DepartmentRequestDTO Dto { get; set; }

        public GetAllActiveDepartmentQuery(DepartmentRequestDTO dto)
        {
            this.Dto = dto;
        }
    }
}
