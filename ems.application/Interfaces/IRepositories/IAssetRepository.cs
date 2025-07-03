using ems.application.DTOs.Asset;
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


        #region asset
        Task<List<Asset>> AddAssetAsync(Asset asset);
         
        Task<List<Asset>> GetAllAssetAsync(Asset? asset);
       // Task<List<Asset>> GetAllAssetAsync(long? TenantId, bool Isactive);
        
        Task<bool> IsAssetDuplicate(Asset asset);
        // Get a asset by its Id
        //    Task<Asset> GetAssetByIdFromTenantAsync(int TenantId ,);
        Task<Asset> UpdateAssetAsync(Asset? asset);
        // Delete a asset by its Id
        Task<int> DeleteAssetAsync(Asset? asset);
        #endregion

       

        #region AssetStatus
        Task<List<AssetStatus>> GetAllAssetsStatus(long? TenantId, bool? Isactive);
        Task<List<AssetStatus>> GetAllAssetStatusByTenantAsync(AssetStatus? assetStatus);        
        Task<AssetStatus>AddAssetStatusByTenantAsync(AssetStatus? assetStatus);
        Task<AssetStatus>UpdateAssetStatusByTenantAsync(AssetStatus assetStatus);
         Task<bool>DeleteAssetStatusByTenantAsync(AssetStatus assetStatus);
        #endregion


        #region AssetTypeCompleted
        Task<AssetType> AddAssetTypeAsync(AssetType assetType);
        Task<List<AssetType>> GetAllAssetTypeByTenantAsync(AssetTypeRequestDTO? assetType);
        Task<AssetType?> UpdateAssetTypeByTenantAsync(AssetType? assetType);
        Task<List<AssetType>> GetAllAssetTypeAsync(long? TenantId, bool? IsActive);
        Task<bool> DeleteAssetTypeByTenantAsync(AssetType assetType);



        #endregion
    }
}
