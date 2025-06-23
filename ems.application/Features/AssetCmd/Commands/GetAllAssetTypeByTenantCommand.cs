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
        public AssetTypeRequestDTO AssetTypeRequest { get; set; }

        public GetAllAssetTypeByTenantCommand(AssetTypeRequestDTO AssetTypeRequest)
        {
            this.AssetTypeRequest = AssetTypeRequest;
        }
    }
}
