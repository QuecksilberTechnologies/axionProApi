using ems.application.DTOs.Employee;
using ems.application.DTOs.Employee.AccessResponse;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.EmployeeCmd.Commands
{
    public class GetEmployeeBankDetailCommand : IRequest<ApiResponse<GetEmployeeBankResponseDTO>>
    {
        public EmployeeInfoRequestDTO DTO { get; set; }

        public GetEmployeeBankDetailCommand(EmployeeInfoRequestDTO dTO)
        {
            DTO = dTO;
        }
    }
}
