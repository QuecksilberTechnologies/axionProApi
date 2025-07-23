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
    public class UpdateAssetTypeCommand : IRequest<ApiResponse<AssetTypeResponseDTO>>
    {
        public UpdateAssetTypeRequestDTO updateAssetTypeRequest { get; set; }

        public UpdateAssetTypeCommand(UpdateAssetTypeRequestDTO updateAssetTypeRequest)
        {
            this.updateAssetTypeRequest = updateAssetTypeRequest;
        }

    }
}
