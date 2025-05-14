using ems.application.DTOs.Operation;
 
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.OperationCmd.Queries
{
    public class GetAllOperationQuery : IRequest<ApiResponse<List<GetAllOperationDTO>>>
    {
        public OperationRequestDTO? operationRequestDTO { get; set; }

        public GetAllOperationQuery(OperationRequestDTO operationRequestDTO)
        {
            this.operationRequestDTO = operationRequestDTO;
        }
    }
}