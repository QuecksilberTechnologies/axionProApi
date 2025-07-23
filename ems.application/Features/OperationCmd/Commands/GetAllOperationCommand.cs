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
    public class GetAllOperationCommand : IRequest<ApiResponse<List<GetOperationResponseDTO>>>
    {
        public GetOperationRequestDTO? Dto { get; set; }

        public GetAllOperationCommand(GetOperationRequestDTO dto)
        {
            this.Dto = dto;
        }
    }
}