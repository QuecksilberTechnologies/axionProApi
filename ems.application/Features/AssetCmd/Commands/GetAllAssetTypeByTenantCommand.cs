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
    public class GetAllAssetTypeByTenantCommand : IRequest<ApiResponse<List<AssetTypeResponseDTO>>>
    {
        public GetAssetTypeRequestDTO getAssetTypeRequest { get; set; }

        public GetAllAssetTypeByTenantCommand(GetAssetTypeRequestDTO getAssetTypeRequest)
        {
            this.getAssetTypeRequest = getAssetTypeRequest;
        }
    }
}
