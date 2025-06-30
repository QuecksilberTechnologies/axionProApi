using ems.application.DTOs.Operation;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.OperationCmd.Commands
{
    public class GetAllOperationCommand : IRequest<ApiResponse<List<GetAllOperationDTO>>>
    {
        public GetAllOperationRequestByProductAdminDTO? operationRequestDTO { get; set; }

        public GetAllOperationCommand(GetAllOperationRequestByProductAdminDTO operationRequestDTO)
        {
            this.operationRequestDTO = operationRequestDTO;
        }
    }
}