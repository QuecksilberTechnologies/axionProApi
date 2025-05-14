using ems.application.DTOs.Operation;
using ems.application.DTOs.Role;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.OperationCmd.Queries
{
    public class GetPageOperationPermissionQuery : IRequest<ApiResponse<HasAccessOperationDTO>>
    {
        public CheckOperationPermissionRequestDTO? CheckOperationPermissionRequest { get; set; }
        
        public GetPageOperationPermissionQuery(CheckOperationPermissionRequestDTO checkOperationPermissionRequest)
        {
            this.CheckOperationPermissionRequest = checkOperationPermissionRequest;
        }
    }

}
