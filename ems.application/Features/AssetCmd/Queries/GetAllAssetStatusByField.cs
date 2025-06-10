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
    public class GetAllAssetStatusByField : IRequest<ApiResponse<List<GetAllAssetWithDependentEntityDTO>>>
    {
        public AssetRequestDTO assetRequestDTO { get; set; }

        public GetAllAssetStatusByField(AssetRequestDTO assetRequestDTO)
        {
            this.assetRequestDTO = assetRequestDTO;
        }

    }
}
