using ems.application.DTOs.Common;
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
    public class UpdateEmployeeFieldByAnyCommand : IRequest<ApiResponse<bool>>
    {
        public UpdateSingleFieldRequestDTO Dto { get; set; }

        public UpdateEmployeeFieldByAnyCommand(UpdateSingleFieldRequestDTO dto)
        {
            Dto = dto;
        }
    }
}
 