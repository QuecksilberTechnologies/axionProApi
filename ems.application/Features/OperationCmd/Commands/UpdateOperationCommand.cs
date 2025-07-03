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
    public class UpdateOperationCommand : IRequest<ApiResponse<List<GetAllOperationDTO>>>
    {

        public UpdateOperationByProductOwnerRequestDTO updateOperationDTO { get; set; }

        public UpdateOperationCommand(UpdateOperationByProductOwnerRequestDTO updateOperationDTO)
        {
            this.updateOperationDTO = updateOperationDTO;
        }

    }
}
