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
    public class GetAllAssetByTenantCommand : IRequest<ApiResponse<List<AssetResponseDTO>>>
    {
        public GetAssetRequestByTenantAdminDTO getAssetTypeRequest { get; set; }

        public GetAllAssetByTenantCommand(GetAssetRequestByTenantAdminDTO getAssetTypeRequest)
        {
            this.getAssetTypeRequest = getAssetTypeRequest;
        }
    }
}
