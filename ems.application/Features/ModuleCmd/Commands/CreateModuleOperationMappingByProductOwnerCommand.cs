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
    public class CreateModuleOperationMappingByProductOwnerCommand : IRequest<ApiResponse<ModuleOperationMappingByProductOwnerResponseDTO>>
    {
        public ModuleOperationMappingByProductOwnerRequestDTO dto { get; set; }

        public CreateModuleOperationMappingByProductOwnerCommand(ModuleOperationMappingByProductOwnerRequestDTO dto)
        {
            this.dto = dto;
        }
    }


}
