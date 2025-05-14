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

        public async Task<List<Asset>> AddAssetAsync(Asset asset)
        {
            try
            {

                asset.AddedDate = DateTime.Now; // or DateTime.UtcNow                
                await _context.Assets.AddAsync(asset);
                // Changes ko save karenge
                await _context.SaveChangesAsync();

               
                return (await GetAllAssetAsync())
                 .OrderByDescending(r => r.Id) // Latest asset पहले आएगा
                 .ToList();
            }
            catch (Exception ex)
            {
                // Exception ko log karenge
                _logger.LogError(ex, "Error occurred while creating asset.");
                throw;  // Rethrow the exception for further handling
            }

        }

        

        public Task<bool> DeleteAssetAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteClientTypeAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Asset>> GetAllAssetAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all Asset from the database...");

                var asset = await _context.Assets.ToListAsync(); // ✅ Corrected EF Core syntax

                if (asset == null || !asset.Any())
                {
                    _logger.LogWarning("No Asset found in the database.");
                    return new List<Asset>(); // Empty list return karein instead of null
                }

                _logger.LogInformation("Successfully retrieved {Count} Asset.", asset.Count);
                return asset;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching Asset.");
                return new List<Asset>(); // Exception ke case me empty list return karein
            }
        }

        public async Task<List<AssetType>> GetAllAssetTypeAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all Asset Types from the database...");

                var assetTypes = await _context.AssetTypes.ToListAsync();

                if (assetTypes == null || !assetTypes.Any())
                {
                    _logger.LogWarning("No Asset Types found in the database.");
                    return new List<AssetType>();
                }

                _logger.LogInformation("Successfully retrieved {Count} Asset Types.", assetTypes.Count);
                return assetTypes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching Asset Types.");
                return new List<AssetType>();
            }
        }

        public async Task<Asset> GetAssetByIdAsync(int id)
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

        public async Task<List<AssetStatus>> GetAssetsStatus()
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
        async Task<List<Asset>> IAssetRepository.UpdateAssetAsync(Asset asset)
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
                existingAsset.UpdatedBy = 1; // ट्रैकिंग के लिए UpdatedBy
                existingAsset.UpdatedDate = DateTime.UtcNow;

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


    }
}
