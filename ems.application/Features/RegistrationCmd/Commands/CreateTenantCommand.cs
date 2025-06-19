using ems.application.DTOs.Employee;
using ems.application.DTOs.Registration;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.RegistrationCmd.Commands
{
    public class CreateTenantCommand : IRequest<ApiResponse<TenantCreateResponseDTO>>
    {
        public TenantRequestDTO TenantCreateRequestDTO { get; set; }

        public CreateTenantCommand(TenantRequestDTO createRequestDTO)
        {
            TenantCreateRequestDTO = createRequestDTO;
        }

    }
}
