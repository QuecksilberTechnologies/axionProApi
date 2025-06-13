using ems.application.DTOs.Asset;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.AssetCmd.Commands
{
    public class UpdateStatusByTenantCommand : IRequest<ApiResponse<AllAssetStatusResponseDTO>>
    {
        public UpdateAssetStatusRequestDTO assetStatusRequestDTO { get; set; }

        public UpdateStatusByTenantCommand(UpdateAssetStatusRequestDTO assetStatusRequestDTO)
        {
            this.assetStatusRequestDTO = assetStatusRequestDTO;
        }

    }
}
