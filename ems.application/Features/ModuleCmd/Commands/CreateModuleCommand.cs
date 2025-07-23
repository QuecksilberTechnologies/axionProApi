using ems.application.DTOs.Leave;
using ems.application.DTOs.Module;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
 
using System.Text;
using System.Threading.Tasks;
 

using ems.domain.Entity;
using ems.application.DTOs.Module.NewFolder;
namespace ems.application.Features.ModuleCmd.Commands
{
    public class CreateModuleCommand : IRequest<ApiResponse<MainModuleResponseDTO>>
    {

        public CreateMainModuleRequestDTO DTO { get; set; }

        public CreateModuleCommand(CreateMainModuleRequestDTO dTO)
        {
            this.DTO = dTO;
        }

    }
     
}
