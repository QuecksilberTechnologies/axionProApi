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
    public class AddNewAssetTypeByTenantCommand : IRequest<ApiResponse<AssetTypeResponseDTO>>
    {
        public AssetTypeRequestDTO assetStatusRequestDTO { get; set; }

        public AddNewAssetTypeByTenantCommand(AssetTypeRequestDTO assetStatusRequestDTO)
        {
            this.assetStatusRequestDTO = assetStatusRequestDTO;
        }
    }

}
 