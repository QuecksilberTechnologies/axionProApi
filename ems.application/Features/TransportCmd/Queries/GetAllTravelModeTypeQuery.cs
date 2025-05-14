 
using ems.application.DTOs.Transport;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.TransportCmd.Queries
{
    public class GetAllTravelModeTypeQuery : IRequest<ApiResponse<List<GetAllTravelModeDTO>>>
    {
        public TravelModeRequestDTO? travelModeRequestDTO { get; set; }

        public GetAllTravelModeTypeQuery(TravelModeRequestDTO clientTypeRequestDTO)
        {
            this.travelModeRequestDTO = travelModeRequestDTO;
        }
    }
    
}
