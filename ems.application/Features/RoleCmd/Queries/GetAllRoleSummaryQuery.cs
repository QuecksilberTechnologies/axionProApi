using ems.application.DTOs.Role;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.RoleCmd.Queries
{
    public class GetAllRoleSummaryQuery : IRequest<ApiResponse<List<GetRoleSummaryResponseDTO>>>
    {
        public  GetRoleSummaryRequestDTO? Dto { get; set; }

        public GetAllRoleSummaryQuery(GetRoleSummaryRequestDTO? dTO)
        {
            Dto = dTO;
        }
    }
}

