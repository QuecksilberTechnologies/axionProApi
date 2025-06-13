using ems.application.Constants;
using ems.application.DTOs.Asset;
using ems.application.Interfaces.IRepositories;
using ems.domain.Entity;
using ems.persistance.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.persistance.Repositories
{
    public class AssetRepository : IAssetRepository
    {
        private WorkforceDbContext _context;
        private ILogger<AssetRepository> _logger;

        public AssetRepository(WorkforceDbContext context, ILogger<AssetRepository> logger)
        {
            this._context = context;
            this._logger = logger;
        }

     

        #region Asset

        /// <summary>
        ///   Asset  Codes
        /// </summary>
        /// <param name="asset"></param>
        /// <returns></returns>
        public async Task<Asset> GetAssetByIdFromTenantAsync(int id)
        {
            try
            {
                var asset = await _context.Assets.FirstOrDefaultAsync(a => a.Id == id);

                if (asset == null)
                {
                    _logger.LogWarning("Asset with ID {AssetId} not found.", id);
                }

                return asset;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching asset with ID {AssetId}.", id);
                throw; // Exception को rethrow कर दें ताकि calling code को पता चल सके
            }
        }

        public async Task<bool> IsAssetDuplicate(Asset asset)
        {
            try
            {
                return await _context.Assets.AnyAsync(a =>
                    (a.SerialNumber == asset.SerialNumber || a.Barcode == asset.Barcode) &&
                    a.Id != asset.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking for duplicate asset with ID {AssetId}.", asset.Id);
                throw;  // Exception को रिथ्रो कर दें ताकि calling code को पता चल सके
            }
        }

        async Task<List<Asset>> UpdateAssetAsync(Asset asset)
        {
            try
            {
                // Database से existing asset ढूंढें
                var existingAsset = await _context.Assets.FirstOrDefaultAsync(a => a.Id == asset.Id);

                if (existingAsset == null)
                {
                    _logger.LogWarning("Asset with ID {AssetId} not found.", asset.Id);
                    return null; // अगर Asset नहीं मिला तो null रिटर्न करें
                }

                // Existing Asset के फ़ील्ड्स को अपडेट करें
                existingAsset.IsRepairable = asset.IsRepairable;
                existingAsset.WarrantyExpiryDate = asset.WarrantyExpiryDate;
                existingAsset.AssetStatusId = asset.AssetStatusId;
                existingAsset.IsAssigned = asset.IsAssigned;
                existingAsset.IsActive = asset.IsActive;
                existingAsset.UpdatedById = 1; // ट्रैकिंग के लिए UpdatedBy
                                               // existingAsset.UpdatedDate = DateTime.UtcNow;

                // बदलावों को सेव करें
                await _context.SaveChangesAsync();

                _logger.LogInformation("Asset with ID {AssetId} updated successfully.", asset.Id);

                // यदि आपकी repository की डिफ़िनिशन List<Asset> रिटर्न करने की मांग करती है, तो आप updated asset के साथ एक list return कर सकते हैं।
                return new List<Asset> { existingAsset };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating asset with ID {AssetId}.", asset.Id);
                throw;  // Rethrow exception for further handling
            }
        }


        public Task<List<Asset>> AddAssetAsync(Asset asset)
        {
            throw new NotImplementedException();
        }

        public Task<List<Asset>> GetAllAssetAsync()
        {
            throw new NotImplementedException();
        }

        Task<List<Asset>> IAssetRepository.UpdateAssetAsync(Asset asset)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAssetAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<AssetType>> GetAllAssetTypeAsync()
        {
            throw new NotImplementedException();
        }

        #endregion


        #region AssetStatus

        /// <summary>
        ///   Asset Status Codes
        /// </summary>
        /// <param name="assetStatus"></param>
        /// <returns></returns>
        public async Task<AssetStatus> UpdateAssetStatusByTenantAsync(AssetStatus assetStatus)
        {
            try
            {
                // Existing record ko DB se fetch karo (Id + TenantId for safety)
                var existingStatus = await _context.AssetStatuses
                    .FirstOrDefaultAsync(x => x.Id == assetStatus.Id && x.TenantId == assetStatus.TenantId && x.IsSoftDeleted== ConstantValues.IsByDefaultFalse);

                if (existingStatus == null)
                {
                    _logger.LogWarning("AssetStatus with Id {Id} not found for TenantId {TenantId}.", assetStatus.Id, assetStatus.TenantId);
                    return null!; // Or throw custom NotFoundException
                }

                // Update fields
                existingStatus.StatusName = assetStatus.StatusName;
                existingStatus.Description = assetStatus.Description;
                existingStatus.IsActive = assetStatus.IsActive;
                existingStatus.UpdatedById = assetStatus.UpdatedById;
                existingStatus.UpdatedDateTime = DateTime.Now;

                // Save to DB
                await _context.SaveChangesAsync();

                return existingStatus;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating AssetStatus.");
                throw;
            }
        }
        public async Task<List<AssetStatus>> GetAllAssetStatusByTenantAsync(AssetStatus? assetStatus)
        {
            try
            {
                if (assetStatus == null || assetStatus.TenantId <= 0)
                {
                    _logger.LogWarning("AssetStatus is null or TenantId is invalid while fetching Asset Status.");
                    return new List<AssetStatus>();
                }

                // ✅ Sanitize input: convert "" to null and 0 to null (for optional fields)
                assetStatus.IsSoftDeleted = ConstantValues.IsByDefaultFalse;
                assetStatus.StatusName = string.IsNullOrWhiteSpace(assetStatus.StatusName) ? null : assetStatus.StatusName;
                assetStatus.IsActive = assetStatus.IsActive == null ? true : assetStatus.IsActive; // false is valid, so keep it
                                                                                                   // assetStatus.IsSoftDeleted = assetStatus.IsSoftDeleted == null ? false : assetStatus.IsSoftDeleted;
                assetStatus.AddedById = assetStatus.AddedById == 0 ? null : assetStatus.AddedById;
                assetStatus.UpdatedById = assetStatus.UpdatedById == 0 ? null : assetStatus.UpdatedById;
                assetStatus.DeletedById = assetStatus.DeletedById == 0 ? null : assetStatus.DeletedById;

                _logger.LogInformation("Dynamically fetching Asset Status records for TenantId: {TenantId}", assetStatus.TenantId);

                IQueryable<AssetStatus> query = _context.AssetStatuses
                    .Where(x => x.TenantId == assetStatus.TenantId);

                query = query
                    .Where(x => assetStatus.StatusName == null || x.StatusName.Contains(assetStatus.StatusName))
                    .Where(x => !assetStatus.IsActive.HasValue || x.IsActive == assetStatus.IsActive)
                    .Where(x => !assetStatus.IsSoftDeleted.HasValue || x.IsSoftDeleted == assetStatus.IsSoftDeleted)
                    .Where(x => !assetStatus.AddedById.HasValue || x.AddedById == assetStatus.AddedById)
                    .Where(x => !assetStatus.UpdatedById.HasValue || x.UpdatedById == assetStatus.UpdatedById)
                    .Where(x => !assetStatus.DeletedById.HasValue || x.DeletedById == assetStatus.DeletedById);

                var result = await query.OrderByDescending(x => x.AddedDateTime).ToListAsync();

                _logger.LogInformation("Fetched {Count} Asset Status records for TenantId: {TenantId}", result.Count, assetStatus.TenantId);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while dynamically fetching Asset Status records for TenantId: {TenantId}", assetStatus?.TenantId);
                return new List<AssetStatus>();
            }
        }

        public async Task<AssetStatus> AddAssetStatusByTenantAsync(AssetStatus assetStatus)
        {
            try
            {
                assetStatus.AddedDateTime = DateTime.Now;

                await _context.AssetStatuses.AddAsync(assetStatus);
                await _context.SaveChangesAsync();

                _logger.LogInformation("AssetStatus added successfully for TenantId: {TenantId}", assetStatus.TenantId);

                // ✅ Return the inserted record (with ID populated)
                return assetStatus;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating asset status.");
                throw;
            }
        }
        public async Task<bool> DeleteAssetStatusByTenantAsync(AssetStatus assetStatus)
        {
            try
            {
                // Corrected query: use x for filtering
                var existingStatus = await _context.AssetStatuses
                    .FirstOrDefaultAsync(x => x.Id == assetStatus.Id &&
                                              x.TenantId == assetStatus.TenantId &&
                                              x.IsSoftDeleted == ConstantValues.IsByDefaultFalse);

                if (existingStatus == null)
                {
                    _logger.LogWarning("AssetStatus with Id {Id} not found for TenantId {TenantId}.", assetStatus.Id, assetStatus.TenantId);
                    return false; // Or throw custom NotFoundException
                }

                // Update only the tracked entity
                existingStatus.IsSoftDeleted = true;
                existingStatus.IsActive = false;
                existingStatus.DeletedById = assetStatus.DeletedById;
                existingStatus.DeletedDateTime = DateTime.Now;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while soft deleting AssetStatus.");
                throw;
            }
        }
        public async Task<List<AssetStatus>> GetAllAssetsStatus()
        {
            try
            {
                _logger.LogInformation("Fetching all Asset Types from the database...");

                var assetsStatus = await _context.AssetStatuses.ToListAsync();

                if (assetsStatus == null || !assetsStatus.Any())
                {
                    _logger.LogWarning("No Asset Status found in the database.");
                    return new List<AssetStatus>();
                }

                _logger.LogInformation("Successfully retrieved {Count} Asset Status.", assetsStatus.Count);
                return assetsStatus;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching Asset status.");
                return new List<AssetStatus>();
            }
        }

        #endregion

        #region Assetype

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assetType"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<AssetType> AddAssetTypeAsync(AssetType assetType)
        {
            try
            {
                _logger.LogInformation("Inserting new AssetType for TenantId: {TenantId}, TypeName: {TypeName}", assetType.TenantId, assetType.TypeName);

                assetType.IsSoftDeleted = ConstantValues.IsByDefaultFalse;
                assetType.IsActive = assetType.IsActive;
                assetType.AddedDateTime = DateTime.Now;
                assetType.UpdatedById = ConstantValues.SystemUserIdByDefaultZero;
                assetType.UpdatedDateTime = null;
                assetType.DeletedById = ConstantValues.SystemUserIdByDefaultZero; ;
                assetType.DeletedDateTime = null;

                await _context.AssetTypes.AddAsync(assetType);
                await _context.SaveChangesAsync();

                _logger.LogInformation("AssetType added successfully with Id: {Id}", assetType.Id);

                //var allAssetTypes = await _context.AssetTypes
                //    .Where(at => at.TenantId == assetType.TenantId && at.IsSoftDeleted == false && at.IsActive == assetType.IsActive)
                //    .OrderByDescending(at => at.Id)
                //    .ToListAsync();

                return assetType;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding AssetType for TenantId: {TenantId}", assetType.TenantId);
                throw; // bubble up for higher-level handling
            }
        }
        public async Task<List<AssetType>> GetAllAssetTypeByTenantAsync(GetAssetTypeRequestDTO? assetType)
        {
            try
            {
                _logger.LogInformation("Fetching Asset Types for TenantId: {TenantId}", assetType.TenantId);

                var allAssetTypes = await _context.AssetTypes
                    .Where(at => at.TenantId == assetType.TenantId && at.IsSoftDeleted == false && at.IsActive == assetType.IsActive)
                    .OrderByDescending(at => at.Id)
                    .ToListAsync();

                _logger.LogInformation("Fetched {Count} Asset Types for TenantId: {TenantId}", allAssetTypes.Count, assetType.TenantId);

                return allAssetTypes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching asset types for TenantId: {TenantId}", assetType.TenantId);
                return new List<AssetType>(); // Empty list return karo jab error aaye
            }
        }

        public async Task<AssetType?> UpdateAssetTypeByTenantAsync(AssetType? assetType)
        {
            try
            {

                // Existing record ko DB se fetch karo (Id + TenantId for safety)
                var existingAssetType = await _context.AssetTypes
                    .FirstOrDefaultAsync(x => x.Id == assetType.Id && x.TenantId == assetType.TenantId && x.IsSoftDeleted == ConstantValues.IsByDefaultFalse);

                if (existingAssetType == null)
                {
                    _logger.LogWarning("AssetStatus with Id {Id} not found for TenantId {TenantId}.", assetType.Id, assetType.TenantId);
                    return null!; // Or throw custom NotFoundException
                }

                // Update fields
                existingAssetType.TypeName = assetType.TypeName;
                existingAssetType.Description = assetType.Description;
                existingAssetType.IsActive = assetType.IsActive;
                existingAssetType.UpdatedById = assetType.UpdatedById;
                existingAssetType.UpdatedDateTime = DateTime.Now;


                // Step 3: Save changes
                await _context.SaveChangesAsync();

                return existingAssetType;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating AssetType with Id {Id}", assetType.Id);
                throw;
            }
        }

        public async Task<bool> DeleteAssetTypeByTenantAsync(AssetType assetType)
        {
            try
            {
                var existingAssetType = await _context.AssetTypes
                    .FirstOrDefaultAsync(x => x.Id == assetType.Id &&
                                              x.TenantId == assetType.TenantId &&
                                              x.IsSoftDeleted == ConstantValues.IsByDefaultFalse);

                if (existingAssetType == null)
                {
                    _logger.LogWarning("AssetStatus with Id {Id} not found for TenantId {TenantId}.", assetType.Id, assetType.TenantId);
                    return false; // Or throw custom NotFoundException
                }

                // Update only the tracked entity
                existingAssetType.IsSoftDeleted = true;
                existingAssetType.IsActive = false;
                existingAssetType.DeletedById = assetType.DeletedById;
                existingAssetType.DeletedDateTime = DateTime.Now;
                // Step 3: Save changes
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while soft deleting AssetType with Id {Id}", assetType.Id);
                throw;
            }
        }



        #endregion




    }
}
