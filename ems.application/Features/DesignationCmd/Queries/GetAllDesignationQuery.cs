 
using ems.application.DTOs.Designation;
 
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.DesignationCmd.Queries
{
    public class GetAllDesignationQuery : IRequest<ApiResponse<List<GetAllDesignationDTO>>>
    {
        public DesignationRequestDTO designationRequestDTO { get; set; }

        public GetAllDesignationQuery(DesignationRequestDTO designationRequestDTO)
        {
            this.designationRequestDTO = designationRequestDTO;
        }
    }
}

