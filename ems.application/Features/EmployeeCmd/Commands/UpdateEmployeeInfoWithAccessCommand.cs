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
    public class UpdateEmployeeInfoWithAccessCommand : IRequest<ApiResponse<bool>>
    {
        public UpdateEmployeeInfoWithAccessRequestDTO Dto { get; set; }

        public UpdateEmployeeInfoWithAccessCommand(UpdateEmployeeInfoWithAccessRequestDTO dto)
        {
            Dto = dto;
        }

    }

}
