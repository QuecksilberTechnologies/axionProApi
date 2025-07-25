﻿
using ems.application.DTOs.Designation;
 
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.DesignationCmd.Commands
{
    public class CreateDesignationCommand : IRequest<ApiResponse<List<GetAllDesignationDTO>>>
    {

        public CreateDesignationDTO dto { get; set; }

        public CreateDesignationCommand(CreateDesignationDTO dto)
        {
            this.dto = dto;
        }

    }

}

 
