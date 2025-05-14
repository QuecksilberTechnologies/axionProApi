
using ems.application.DTOs.Designation;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.DesignationCmd.Commands
{
    public class UpdateDesignationCommand : IRequest<ApiResponse<List<GetAllDesignationDTO>>>
    {

        public UpdateDesignationDTO updateDesignationDTO { get; set; }

        public UpdateDesignationCommand(UpdateDesignationDTO updateDesignationDTO)
        {
            this.updateDesignationDTO = updateDesignationDTO;
        }

    }
   
}
