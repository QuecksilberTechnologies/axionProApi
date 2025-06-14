using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Asset
{
    public class GetAllAssetWithDependentEntityDTO
    {
        public List<AssetResponseDTO> GetAllAssetDTOs { get; set; }
        public List<GetAllAssetTypeDTO> GetAllAssetTypeDTOs { get; set; }
        public List<AllAssetStatusResponseDTO> GetAllAssetStatusDTOs { get; set; }

    }
}
