using ems.application.DTOs.Module;
using ems.application.DTOs.Module.NewFolder;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.ModuleCmd.Commands
{
    public class CreateSubModuleCommand : IRequest<ApiResponse<MainModuleResponseDTO>>
    {

        public CreateSubModuleRequestDTO DTO { get; set; }
        
        public CreateSubModuleCommand(CreateSubModuleRequestDTO dTO)
        {
            this.DTO = dTO;
        }

    }

}