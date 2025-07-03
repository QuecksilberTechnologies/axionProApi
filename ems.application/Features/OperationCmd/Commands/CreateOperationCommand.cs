 
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
    public class CreateOperationCommand : IRequest<ApiResponse<List<GetAllOperationDTO>>>
    {

        public CreateOperationByProductOwnerRequestDTO createOperationDTO { get; set; }

        public CreateOperationCommand(CreateOperationByProductOwnerRequestDTO createOperationDTO)
        {
            this.createOperationDTO = createOperationDTO;
        }

    }
}
