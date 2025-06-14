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
                    (a.SerialNumber == asset.SerialNumber || a.Barcode == asset.Barcode) && a.Id != asset.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking for duplicate asset with ID {AssetId}.", asset.Id);
                throw;  // Exception को रिथ्रो कर दें ताकि calling code को पता चल सके
            }
        }
        public async Task<Asset> UpdateAssetAsync(Asset asset)
        {
            try
            {
                var existingAsset = await _context.Assets.FirstOrDefaultAsync(a => a.Id == asset.Id && a.IsSoftDeleted !=ConstantValues.IsByDefaultTrue);

                if (existingAsset == null)
                {
                    _logger.LogWarning("Asset with ID {AssetId} not found.", asset.Id);
                    return null;
                }

                // Null/Default check based update
                existingAsset.AssetName = !string.IsNullOrWhiteSpace(asset.AssetName) ? asset.AssetName : existingAsset.AssetName;
                existingAsset.AssetTypeId = asset.AssetTypeId != 0 ? asset.AssetTypeId : existingAsset.AssetTypeId;
                existingAsset.Company = !string.IsNullOrWhiteSpace(asset.Company) ? asset.Company : existingAsset.Company;
                existingAsset.Color = !string.IsNullOrWhiteSpace(asset.Color) ? asset.Color : existingAsset.Color;
                existingAsset.IsRepairable = asset.IsRepairable ?? existingAsset.IsRepairable;
                existingAsset.Price = asset.Price.HasValue && asset.Price > 0 ? asset.Price : existingAsset.Price;
                existingAsset.SerialNumber = !string.IsNullOrWhiteSpace(asset.SerialNumber) ? asset.SerialNumber : existingAsset.SerialNumber;
                existingAsset.Barcode = !string.IsNullOrWhiteSpace(asset.Barcode) ? asset.Barcode : existingAsset.Barcode;
                existingAsset.Qrcode = !string.IsNullOrWhiteSpace(asset.Qrcode) ? asset.Qrcode : existingAsset.Qrcode;
                existingAsset.PurchaseDate = asset.PurchaseDate != default ? asset.PurchaseDate : existingAsset.PurchaseDate;
                existingAsset.WarrantyExpiryDate = asset.WarrantyExpiryDate ?? existingAsset.WarrantyExpiryDate;
                existingAsset.AssetStatusId = asset.AssetStatusId != 0 ? asset.AssetStatusId : existingAsset.AssetStatusId;
                existingAsset.IsAssigned = asset.IsAssigned ?? existingAsset.IsAssigned;
                existingAsset.IsActive = asset.IsActive ?? existingAsset.IsActive;
                // Audit
                existingAsset.UpdatedById = asset.UpdatedById;
                existingAsset.UpdatedDateTime = DateTime.Now;

                await _context.SaveChangesAsync();

                _logger.LogInformation("Asset with ID {AssetId} updated successfully.", asset.Id);
                return existingAsset;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating asset with ID {AssetId}.", asset.Id);
                return null;
            }
        }

        public async Task<List<Asset>> AddAssetAsync(Asset asset)
        {
            try
            {
                _logger.LogInformation("Inserting new Asset for TenantId: {TenantId}, TypeName: {TypeName}", asset.TenantId, asset.AssetName);
                asset.IsSoftDeleted = ConstantValues.IsByDefaultFalse;
                asset.IsActive = asset.IsActive;
                asset.AddedDateTime = DateTime.Now;
                asset.UpdatedById = ConstantValues.SystemUserIdByDefaultZero;
                asset.UpdatedDateTime = null;
                asset.DeletedById = ConstantValues.SystemUserIdByDefaultZero; ;
                asset.DeletedDateTime = null;

                await _context.Assets.AddAsync(asset);
                 await _context.SaveChangesAsync();

                _logger.LogInformation("AssetType added successfully with Id: {Id}", asset.Id);

                //var allAssetTypes = await _context.AssetTypes
                //    .Where(at => at.TenantId == assetType.TenantId && at.IsSoftDeleted == false && at.IsActive == assetType.IsActive)
                //    .OrderByDescending(at => at.Id)
                //    .ToListAsync();

                return (await GetAllAssetAsync(asset.TenantId, asset.IsActive))
              .OrderByDescending(r => r.Id) // Latest asset पहले आएगा
              .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding AssetType for TenantId: {TenantId}", asset.TenantId);
                throw; // bubble up for higher-level handling
            }
           
        }

        public async Task<List<Asset>> GetAllAssetAsync(long tenantId, bool? isActive)
        {
            List<Asset> assets = new List<Asset>();

            try
            {
                IQueryable<Asset> query = _context.Assets
                    .Where(at => at.TenantId == tenantId && at.IsSoftDeleted == ConstantValues.IsByDefaultFalse);

                if (isActive.HasValue)
                {
                    query = query.Where(at => at.IsActive == isActive.Value);
                }

                _logger.LogInformation("Fetching all Assets for TenantId {TenantId}", tenantId);

                assets = await query
                    .OrderByDescending(at => at.Id)
                    .ToListAsync();

                if (assets == null || !assets.Any())
                {
                    _logger.LogWarning("No Assets found for TenantId {TenantId}.", tenantId);
                    return new List<Asset>();
                }

                _logger.LogInformation("Successfully retrieved {Count} Assets for TenantId {TenantId}.", assets.Count, tenantId);
                return assets;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching Assets.");
                return new List<Asset>();
            }
        }


       

        public async Task<int> DeleteAssetAsync(Asset? asset)
        {
            try
            {
                // Corrected query: use x for filtering
                var existingAsset = await _context.Assets
                    .FirstOrDefaultAsync(x => x.Id == asset.Id &&
                                              x.TenantId == asset.TenantId &&
                                              x.IsSoftDeleted == ConstantValues.IsByDefaultFalse);

                if (existingAsset == null)
                {
                    _logger.LogWarning("Asset with Id {Id} not found for TenantId {TenantId}.", asset.Id, asset.TenantId);
                    return 0; // Or throw custom NotFoundException
                }
                if (existingAsset.IsAssigned == ConstantValues.IsByDefaultTrue)
                    return -1;

                // Update only the tracked entity
                existingAsset.IsSoftDeleted = ConstantValues.IsByDefaultTrue;
                existingAsset.IsActive = ConstantValues.IsByDefaultFalse;
                existingAsset.DeletedById = asset.DeletedById;
                existingAsset.DeletedDateTime = DateTime.Now;

                await _context.SaveChangesAsync();
                return 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while soft deleting Asset.");
                return -2;
            }

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

        public async Task<List<AssetType>> GetAllAssetTypeAsync(long? tenantId, bool? isActive)
        {
            try
            {
                _logger.LogInformation("Fetching all AssetTypes from the database for TenantId: {TenantId}, IsActive: {IsActive}", tenantId, isActive);

                var assetTypes = await _context.AssetTypes
                    .Where(at => at.TenantId == tenantId
                                 && at.IsSoftDeleted == ConstantValues.IsByDefaultFalse
                                 && at.IsActive == isActive)
                    .OrderByDescending(at => at.Id)
                    .ToListAsync();

                if (assetTypes == null || !assetTypes.Any())
                {
                    _logger.LogWarning("No AssetTypes found for TenantId: {TenantId}.", tenantId);
                    return new List<AssetType>();
                }

                _logger.LogInformation("Successfully retrieved {Count} AssetTypes for TenantId: {TenantId}", assetTypes.Count, tenantId);
                return assetTypes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching AssetTypes for TenantId: {TenantId}", tenantId);
                return new List<AssetType>(); // Safe return in case of exception
            }
        }

        public async Task<List<AssetStatus>> GetAllAssetsStatus(long? tenantId, bool? isActive)
        {
            List<AssetStatus> assetStatuses = new();

            try
            {
                if (tenantId == null)
                {
                    _logger.LogWarning("TenantId is null while fetching asset statuses.");
                    return new List<AssetStatus>();
                }

                IQueryable<AssetStatus> query = _context.AssetStatuses
                    .Where(a => a.TenantId == tenantId && a.IsSoftDeleted == ConstantValues.IsByDefaultFalse);

                if (isActive != null)
                {
                    query = query.Where(a => a.IsActive == isActive);
                }

                assetStatuses = await query
                    .OrderByDescending(a => a.Id)
                    .ToListAsync();

                if (!assetStatuses.Any())
                {
                    _logger.LogWarning("No AssetStatus records found for TenantId: {TenantId}", tenantId);
                }
                else
                {
                    _logger.LogInformation("Successfully retrieved {Count} AssetStatus records for TenantId: {TenantId}", assetStatuses.Count, tenantId);
                }

                return assetStatuses;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching asset statuses for TenantId: {TenantId}", tenantId);
                return new List<AssetStatus>();
            }
        }


        public async Task<List<Asset>> GetAllAssetAsync(Asset? asset)
        {
            try
            {
                if (asset == null || asset.TenantId <= 0)
                {
                    _logger.LogWarning("Asset is null or TenantId is invalid while fetching Asset.");
                    return new List<Asset>();
                }

                // ✅ Set static filters and sanitize
                asset.IsSoftDeleted = ConstantValues.IsByDefaultFalse;
                asset.AssetName = string.IsNullOrWhiteSpace(asset.AssetName) ? null : asset.AssetName;
                asset.IsActive = asset.IsActive ?? true;
                asset.AddedById = asset.AddedById == 0 ? null : asset.AddedById;
                asset.UpdatedById = asset.UpdatedById == 0 ? null : asset.UpdatedById;
                asset.Color = string.IsNullOrWhiteSpace(asset.Color) ? null : asset.Color;
                asset.SerialNumber = string.IsNullOrWhiteSpace(asset.SerialNumber) ? null : asset.SerialNumber;
                asset.Barcode = string.IsNullOrWhiteSpace(asset.Barcode) ? null : asset.Barcode;
                asset.Qrcode = string.IsNullOrWhiteSpace(asset.Qrcode) ? null : asset.Qrcode;
                // asset.DeletedById = asset.DeletedById == 0 ? null : asset.DeletedById;

                _logger.LogInformation("Dynamically fetching Asset records for TenantId: {TenantId}", asset.TenantId);

                // ✅ Base query
                IQueryable<Asset> query = _context.Assets
                    .Where(x => x.TenantId == asset.TenantId);

                // ✅ Dynamic filters
                if (!string.IsNullOrEmpty(asset.AssetName))
                    query = query.Where(x => x.AssetName.Contains(asset.AssetName));

                if (!string.IsNullOrEmpty(asset.Color))
                    query = query.Where(x => x.Color.Contains(asset.Color));

                if (!string.IsNullOrEmpty(asset.SerialNumber))
                    query = query.Where(x => x.SerialNumber.Contains(asset.SerialNumber));

                if (!string.IsNullOrEmpty(asset.Barcode))
                    query = query.Where(x => x.Barcode.Contains(asset.Barcode));

                if (!string.IsNullOrEmpty(asset.Qrcode))
                    query = query.Where(x => x.Qrcode.Contains(asset.Qrcode));

                if (asset.IsRepairable.HasValue)
                    query = query.Where(x => x.IsRepairable == asset.IsRepairable); 

                if (asset.IsActive.HasValue)
                    query = query.Where(x => x.IsActive == asset.IsActive);

                if (asset.IsSoftDeleted.HasValue)
                    query = query.Where(x => x.IsSoftDeleted == asset.IsSoftDeleted);

                if (asset.AddedById.HasValue && asset.AddedById.Value > 0)
                    query = query.Where(x => x.AddedById == asset.AddedById);

                // Only apply UpdatedById filter if it's a valid non-zero value
                if (asset.UpdatedById.HasValue && asset.UpdatedById.Value > 0)
                    query = query.Where(x => x.UpdatedById == asset.UpdatedById);


                var result = await query
                    .OrderByDescending(x => x.AddedDateTime)
                    .ToListAsync();

                _logger.LogInformation("Fetched {Count} Asset records for TenantId: {TenantId}", result.Count, asset.TenantId);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while dynamically fetching Asset records for TenantId: {TenantId}", asset?.TenantId);
                return new List<Asset>();
            }
        }








        #endregion




    }
}
