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
        public UpdateAssetDTO updateAssetDTO { get; set; }

        public UpdateAssetCommand(UpdateAssetDTO updateAssetDTO)
        {
            this.updateAssetDTO = updateAssetDTO;
        }

    }



}
