using ems.application.DTOs.Client;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.ClientCmd.Commands
{
    public class UpdateClientTypeCommand : IRequest<ApiResponse<List<GetAllClientTypeDTO>>>
    {

        public UpdateClientTypeDTO updateClientTypeCommand { get; set; }

        public UpdateClientTypeCommand(UpdateClientTypeDTO updateClientTypeCommand)
        {
            this.updateClientTypeCommand = updateClientTypeCommand;
        }
    }
}
