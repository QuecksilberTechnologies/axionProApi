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
    public class GetModulesDLLCommand : IRequest<ApiResponse<List<GetModuleDDLResponseDTO>>>
    {

        public GetModuleDDLRequestDTO DTO { get; set; }

        public GetModulesDLLCommand(GetModuleDDLRequestDTO dTO)
        {
            this.DTO = dTO;
        }

    }
    }
