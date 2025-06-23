using ems.application.DTOs.Asset;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.AssetCmd.Queries
{
    public class GetAllAssetStatusByFieldQuery : IRequest<ApiResponse<List<AssetStatusResponseDTO>>>
    {
        public AssetStatusRequestDTO assetStatusRequestDTO { get; set; }

        public GetAllAssetStatusByFieldQuery(AssetStatusRequestDTO assetStatusRequestDTO)
        {
            this.assetStatusRequestDTO = assetStatusRequestDTO;
        }

    }
}
