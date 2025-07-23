using AutoMapper;
using ems.application.DTOs.Designation;
using ems.application.Features.DesignationCmd.Handlers;
using ems.application.Features.DesignationCmd.Queries;
using ems.application.Interfaces;
using ems.application.Wrappers;
using ems.domain.Entity;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.DesignationCmd.Commands
{
    public class GetAllActiveDesignationCommand : IRequest<ApiResponse<List<GetAllDesignationDTO>>>
    {
        public GetAllActiveDesignationRequestDTO dto { get; set; }

        public GetAllActiveDesignationCommand(GetAllActiveDesignationRequestDTO dto)
        {
            this.dto = dto;
        }
    }
}
 
