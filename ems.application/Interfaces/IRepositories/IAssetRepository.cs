using ems.domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Interfaces.IRepositories
{
    public interface IAssetRepository
    {
        // Create a new asset
        Task<List<Asset>> AddAssetAsync(Asset asset);

        // Get a asset by its Id
    //    Task<Asset> GetAssetByIdFromTenantAsync(int TenantId ,);
        Task<bool> IsAssetDuplicate(Asset asset);

        // Get all asset
        Task<List<Asset>> GetAllAssetAsync();
        Task<List<AssetType>> GetAllAssetTypeAsync();
        Task<List<AssetStatus>> GetAssetsStatus();

        // Update an existing asset
        Task<List<Asset>> UpdateAssetAsync(Asset asset);

        // Delete a asset by its Id
        Task<bool> DeleteAssetAsync(int Id);




        /// <summary>
        /// 
        /// </summary>
        /// <param name="assetStatus"></param>
        /// <returns></returns>


        Task<List<AssetStatus>> GetAllAssetStatusByTenantAsync(AssetStatus assetStatus);        
        Task<List<AssetStatus>>AddAssetStatusByTenantAsync(AssetStatus assetStatus);
        Task<AssetStatus>UpdateAssetStatusByTenantAsync(AssetStatus assetStatus);
      //  Task<AssetStatus>UpdateAssetStatusByTenantAsync(AssetStatus assetStatus);
    }
}
