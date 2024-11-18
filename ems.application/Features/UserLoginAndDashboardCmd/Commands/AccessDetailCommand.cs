using AutoMapper;
using ems.application.DTOs.UserLogin;
using ems.application.Interfaces.ITokenService;
using ems.application.Interfaces;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.UserLoginAndDashboardCmd.Commands
{
    public class AccessDetailCommand : IRequest<ApiResponse<AccessDetailResponseDTO>>
    {
       
        public AccessDetailRequestDTO AccessDetailDTO { get; set; }
        public AccessDetailCommand(AccessDetailRequestDTO accessRequestDTO)
        {
            AccessDetailDTO = accessRequestDTO;
        }

    }
}
