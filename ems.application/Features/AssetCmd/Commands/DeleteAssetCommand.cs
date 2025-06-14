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
    public class DeleteAssetCommand : IRequest<ApiResponse<bool>>
    {
        public DeleteAssetRequestDTO deleteAssetDTO { get; set; }

        public DeleteAssetCommand(DeleteAssetRequestDTO deleteAssetDTO)
        {
            this.deleteAssetDTO = deleteAssetDTO;
        }

    }
     
}
