using ems.application.DTOs.Client;
 
using ems.application.Features.ClientCmd.Queries;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.ClientCmd.Commands
{
    public class CreateClientTypeCommand :  IRequest<ApiResponse<List<GetAllClientTypeDTO>>>
    {
        
            public CreateClientTypeDTO createClientTypeDTO { get; set; }

    public CreateClientTypeCommand(CreateClientTypeDTO createClientTypeDTO)
    {
        this.createClientTypeDTO = createClientTypeDTO;
    }
}
}

