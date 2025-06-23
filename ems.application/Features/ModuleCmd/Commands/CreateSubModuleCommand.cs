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
    public class CreateSubModuleCommand : IRequest<ApiResponse<ModuleResponseDTO>>
    {

        public CreateSubModuleRequestDTO createModuleRequestDTO { get; set; }
        
        public CreateSubModuleCommand(CreateSubModuleRequestDTO createModuleRequestDTO)
        {
            this.createModuleRequestDTO = createModuleRequestDTO;
        }

    }

}