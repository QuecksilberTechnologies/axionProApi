using ems.application.DTOs.Module;
using ems.application.DTOs.Tenant;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.ModuleCmd.Commands
{
    public class LoginGetAllModuleOperationConfigurationCommand : IRequest<ApiResponse<List<TenantEnableModuleDTO>>>
    {
        public ModuleOperationConfigurationRequestDTO ModuleOperationConfigurationRequestDTO { get; set; }

        public LoginGetAllModuleOperationConfigurationCommand(ModuleOperationConfigurationRequestDTO dto)
        {
            ModuleOperationConfigurationRequestDTO = dto;
        }
    }
}
