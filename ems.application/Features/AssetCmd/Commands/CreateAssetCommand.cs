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
    public class CreateAssetCommand : IRequest<ApiResponse<List<AssetResponseDTO>>>
    {
        public CreateAssetRequestDTO createAssetDTO { get; set; }

        public CreateAssetCommand(CreateAssetRequestDTO createAssetDTO)
        {
            this.createAssetDTO = createAssetDTO;
        }

    }



}
