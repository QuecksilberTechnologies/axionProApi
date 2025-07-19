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
    public class UpdateEmployeeExperienceInfoWithAccessCommand : IRequest<ApiResponse<bool>>
    {
        public UpdateGenricAllEmployeeEntityRequestDTO updateEmployeeInfoWithAccessRequest { get; set; }

        public UpdateEmployeeExperienceInfoWithAccessCommand(UpdateGenricAllEmployeeEntityRequestDTO dto)
        {
            updateEmployeeInfoWithAccessRequest = dto;
        }

    }
}