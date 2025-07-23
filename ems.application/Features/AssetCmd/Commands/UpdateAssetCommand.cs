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
    public class UpdateAssetCommand : IRequest<ApiResponse<AssetResponseDTO>>
    {
        public UpdateAssetRequestDTO updateAssetDTO { get; set; }

        public UpdateAssetCommand(UpdateAssetRequestDTO updateAssetDTO)
        {
            this.updateAssetDTO = updateAssetDTO;
        }

    }



}
