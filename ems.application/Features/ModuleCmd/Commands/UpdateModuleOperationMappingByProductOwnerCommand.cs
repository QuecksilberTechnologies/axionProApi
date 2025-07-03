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
    public class UpdateModuleOperationMappingByProductOwnerCommand : IRequest<ApiResponse<ModuleOperationMappingByProductOwnerResponseDTO>>
    {

        public UpdateModuleOperationMappingByProductOwnerRequestDTO dto { get; set; }

        public UpdateModuleOperationMappingByProductOwnerCommand(UpdateModuleOperationMappingByProductOwnerRequestDTO dto)
        {
            this.dto = dto;
        }

    }

}
