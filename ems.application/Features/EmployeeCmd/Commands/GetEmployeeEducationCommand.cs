using ems.application.DTOs.Employee;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.EmployeeCmd.Commands
{
    public class GetEmployeeEducationCommand : IRequest<ApiResponse<GetEmployeeEducationResponseDTO>>
    {
        public GetEmployeeInfoRequestDTO DTO { get; set; }

        public GetEmployeeEducationCommand(GetEmployeeInfoRequestDTO dTO)
        {
            DTO = dTO;
        }
    }
}
