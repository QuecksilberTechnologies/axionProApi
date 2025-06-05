using ems.application.DTOs.Operation;
using ems.application.DTOs.Region;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.RegionCmd.Queries
{
    public class GetAllCountryQuery : IRequest<ApiResponse<List<GetAllCountryDTO>>>
    {
        public CountryRequestDTO? countryRequestDTO { get; set; }

        public GetAllCountryQuery(CountryRequestDTO operationRequestDTO)
        {
            countryRequestDTO = countryRequestDTO;
        }
    }

}

