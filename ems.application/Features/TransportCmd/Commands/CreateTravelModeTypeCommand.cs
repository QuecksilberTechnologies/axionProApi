 
using ems.application.DTOs.Transport;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.TransportCmd.Commands
{
    public class CreateTravelModeTypeCommand : IRequest<ApiResponse<List<GetAllTravelModeDTO>>>
    {

        public CreateTravelModeDTO createTravelModeDTO { get; set; }

        public CreateTravelModeTypeCommand(CreateTravelModeDTO createClientTypeDTO)
        {
            this.createTravelModeDTO = createClientTypeDTO;
        }
   
    }
}
