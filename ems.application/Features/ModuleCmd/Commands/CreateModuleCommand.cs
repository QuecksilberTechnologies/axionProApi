using ems.application.DTOs.Leave;
using ems.application.DTOs.Module;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.ModuleCmd.Commands
{
    public class CreateModuleCommand : IRequest<ApiResponse<ModuleResponseDTO>>
    {

        public CreateModuleRequestDTO createModuleRequestDTO { get; set; }

        public CreateModuleCommand(CreateModuleRequestDTO createModuleRequestDTO)
        {
            this.createModuleRequestDTO = createModuleRequestDTO;
        }

    }
     
}
