﻿using ems.application.DTOs.Asset;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.AssetCmd.Commands
{
    public class AddStatusByTenantCommand : IRequest<ApiResponse<AssetStatusResponseDTO>>
    {
        public AssetStatusRequestDTO assetStatusRequestDTO { get; set; }

        public AddStatusByTenantCommand(AssetStatusRequestDTO assetStatusRequestDTO)
        {
            this.assetStatusRequestDTO = assetStatusRequestDTO;
        }

    }
}
