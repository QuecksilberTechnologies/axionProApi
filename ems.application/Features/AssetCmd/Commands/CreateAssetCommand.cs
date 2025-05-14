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
    public class CreateAssetCommand : IRequest<ApiResponse<List<GetAllAssetWithDependentEntityDTO>>>
    {
        public CreateAssetDTO createAssetDTO { get; set; }

        public CreateAssetCommand(CreateAssetDTO createAssetDTO)
        {
            this.createAssetDTO = createAssetDTO;
        }

    }



}
