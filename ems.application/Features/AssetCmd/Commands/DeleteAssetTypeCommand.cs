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
    public class DeleteAssetTypeCommand : IRequest<ApiResponse<bool>>
    {
        public DeleteAssetTypeRequestDTO deleteAssetTypeRequestDTO { get; set; }

        public DeleteAssetTypeCommand(DeleteAssetTypeRequestDTO deleteAssetTypeRequestDTO)
        {
            this.deleteAssetTypeRequestDTO = deleteAssetTypeRequestDTO;
        }
    }

}