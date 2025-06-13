using ems.application.DTOs.Operation;
using ems.application.DTOs.ProjectModule;
using ems.application.DTOs.UserLogin;
using ems.application.DTOs.UserRole;
using ems.application.Interfaces.IRepositories;
using ems.domain.Entity;
using ems.persistance.Data.Context;
using FluentValidation;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ems.persistance.Repositories
{
    public class CommonRepository : ICommonRepository
    {
        private readonly WorkforceDbContext _context;
        private readonly ILogger<CommonRepository> _logger;

        public CommonRepository(WorkforceDbContext context, ILogger<CommonRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        //public async Task<List<ProjectSubModuleDetailDTO>> GetDasboardMenuAsync(string Roles)
        //{
        //    try
        //    {
        //       // Roles = "7,5";
        //        // 🔹 Convert List<int> to Comma-Separated String


        //        var sqlQuery = "EXEC AxionPro.GetDashboardMenusForUser @RoleIds, @ErrorMessage OUTPUT";

        //        var roleParam = new SqlParameter("@RoleIds", Roles);
        //        var errorMessageParam = new SqlParameter
        //        {
        //            ParameterName = "@ErrorMessage",
        //            SqlDbType = SqlDbType.NVarChar,
        //            Size = 500,
        //            Direction = ParameterDirection.Output
        //        };

        //        var result = await _context.ProjectSubModuleDetails
        //            .FromSqlRaw(sqlQuery, roleParam, errorMessageParam)
        //            .ToListAsync();

        //        string errorMessage = errorMessageParam.Value?.ToString();
        //        if (!string.IsNullOrEmpty(errorMessage))
        //        {
        //            _logger.LogError("Stored Procedure Error: {ErrorMessage}", errorMessage);
        //            throw new Exception($"Stored Procedure Error: {errorMessage}");
        //        }

        //        // ✅ Parent-Child Relationship Handling
        //        var groupedModules = result
        //            .GroupBy(x => new
        //            {
        //                x.Id,
        //                x.SubModuleName,
        //                x.SubModuleUrl,
        //                x.IsSubModuleDisplayInUi,
        //                x.IsActive,
        //                x.Remark
        //            })
        //            .Select(g => new ProjectSubModuleDetailDTO
        //            {
        //                Id = g.Key.Id,
        //                SubModuleName = g.Key.SubModuleName,
        //                SubModuleURL = g.Key.SubModuleUrl,
        //                IsSubModuleDisplayInUI = g.Key.IsSubModuleDisplayInUi,
        //                IsActive = g.Key.IsActive,
        //                Remark = g.Key.Remark,
        //                ChildPage = g.Select(c => new ProjectChildModuleDetailDTO
        //                {
        //                    Id = c.ProjectChildModuleDetails.FirstOrDefault()?.Id ?? 0,
        //                    ChildModuleName = c.ProjectChildModuleDetails.FirstOrDefault()?.ChildModuleName,
        //                    ChildModuleURL = c.ProjectChildModuleDetails.FirstOrDefault()?.ChildModuleUrl,
        //                    IconImage = c.ProjectChildModuleDetails.FirstOrDefault()?.IconImage,
        //                    IsOperational = c.ProjectChildModuleDetails.FirstOrDefault()?.IsOperational ?? false
        //                }).Where(c => c.Id > 0).ToList()
        //            }).ToList();

        //        return groupedModules;
        //    }
        //    catch (SqlException ex)
        //    {
        //        _logger.LogError(ex, "SQL Error in GetDasboardMenuAsync for RoleIds: {RoleIds}", userRoles);
        //        throw new Exception("Database error occurred while fetching dashboard menu.", ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Unexpected Error in GetDasboardMenuAsync for RoleIds: {RoleIds}", userRoles);
        //        throw new Exception("An unexpected error occurred while fetching dashboard menu.", ex);
        //    } await _context.Database.ExecuteSqlRawAsync
        //} 

        public async Task<List<CommonItem>> GetCommonItemAsync()
        {
            try
            {
                var sqlQuery = "EXEC AxionPro.GetCommonItem";
                var result = await _context.CommonItems
                    .FromSqlRaw(sqlQuery)
                    .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching dashboard menu: {ex.Message}", ex);
            }
        }



        public async Task<List<RoleModulePermission>> GetModulePermissionsAsync(long empId, string roleIds, bool hasAccess, bool isActive)
        {
            try
            {
                string sqlQuery = "EXEC AxionPro.GetRoleModulePermissions @RoleIds,@EmployeeId, @HasAccess, @IsActive";

                // Parameters ko define karein
                var parameters = new[]
                {
                 new SqlParameter("@RoleIds", roleIds) { DbType = System.Data.DbType.String },
                 new SqlParameter("@EmployeeId", empId) ,                 
                 new SqlParameter("@HasAccess", hasAccess),
                 new SqlParameter("@IsActive", isActive)
                  };

                var result = await _context.RoleModulePermissions
                    .FromSqlRaw(sqlQuery, parameters)
                    .ToListAsync();

                // Icons ko Base64 convert karna
                //foreach (var item in result)
                //{
                //    item.ModuleIcon = item.ModuleIcon != null ? Convert.FromBase64String(ConvertToBase64(item.ModuleIcon)) : null;
                //    item.SubModuleIcon = item.SubModuleIcon != null ? Convert.FromBase64String(ConvertToBase64(item.SubModuleIcon)) : null;
                //    item.ChildModuleIcon = item.ChildModuleIcon != null ? Convert.FromBase64String(ConvertToBase64(item.ChildModuleIcon)) : null;
                //}


                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching module permissions for roles: {roleIds}", roleIds);
                throw;
            }
        }

        // Byte Array to Base64 Converter
        public string ConvertToBase64(byte[] iconData)
        {
            if (iconData == null || iconData.Length == 0)
                return string.Empty;

            return Convert.ToBase64String(iconData);
        }


        public async Task<bool> UpdateLoginCredential(LoginRequestDTO loginRequest)
        {
            try
            {
                // var ttt = _context.Database.GetDbConnection().ToString();
                _logger.LogInformation("Updating login credential for LoginId: {LoginId}", loginRequest.LoginId);
                // _logger.LogInformation("DBContext", _context.Database.GetDbConnection().ToString());

                string sqlQuery = @"EXEC AxionPro.UpdateLoginCredential 
                            @LoginId, @Latitude, @Longitude, @LoginDevice, 
                            @IpAddressLocal, @IpAddressPublic, @MacAddress, 
                            @Status OUTPUT, @ErrorMessage OUTPUT";

                var statusParam = new SqlParameter("@Status", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var errorMsgParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 4000) { Direction = ParameterDirection.Output };

                await _context.Database.ExecuteSqlRawAsync(sqlQuery,
                    new SqlParameter("@LoginId", loginRequest.LoginId ?? (object)DBNull.Value),
                    new SqlParameter("@Latitude", loginRequest.Latitude),
                    new SqlParameter("@Longitude", loginRequest.Longitude),
                    new SqlParameter("@LoginDevice", loginRequest.LoginDevice),
                    new SqlParameter("@IpAddressLocal", loginRequest.IpAddressLocal ?? (object)DBNull.Value),
                    new SqlParameter("@IpAddressPublic", loginRequest.IpAddressPublic ?? (object)DBNull.Value),
                    new SqlParameter("@MacAddress", loginRequest.MacAddress ?? (object)DBNull.Value),
                    statusParam,
                    errorMsgParam
                );

                if (errorMsgParam.Value != DBNull.Value && !string.IsNullOrEmpty(errorMsgParam.Value.ToString()))
                {
                    _logger.LogError("Update failed for LoginId: {LoginId}, Error: {ErrorMessage}", loginRequest.LoginId, errorMsgParam.Value);
                    return false;
                }

                return true;
                    //(statusParam.Value != DBNull.Value && (int)statusParam.Value == 1) ? "Success" : "No record updated";
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Exception occurred while updating login credential for LoginId: {LoginId}", loginRequest.LoginId);
              //  return "Database error occurred while updating login credential.";
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating login credential for LoginId: {LoginId}", loginRequest.LoginId);
                return   false; // "An unexpected error occurred.";
            }
        }

        public async Task<long> ValidateUserLoginAsync(string loginId)
        {
            try
            {
                _logger.LogInformation("Validating user login for LoginId: {LoginId}", loginId);

                var loginParam = new SqlParameter("@LoginId", loginId ?? (object)DBNull.Value);
                var resultParam = new SqlParameter("@Result", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                string sqlQuery = "EXEC AxionPro.ValidateUserLogin @LoginId, @Result OUTPUT";

                await _context.Database.ExecuteSqlRawAsync(sqlQuery, loginParam, resultParam);

                return (int)resultParam.Value;  // Output Parameter से Result Return करें
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Exception occurred while validating user login for LoginId: {LoginId}", loginId);
                return -1;  // Error Case
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while validating user login for LoginId: {LoginId}");
                return -1;  // Error Case
            }
        }
        
        
        
        
        
        
        public async Task<bool> GetHasAccessOperation(CheckOperationPermissionRequestDTO checkOperationPermissionRequest)
        {
            try
            {
                // SQL query calling the stored procedure and returning the result
                string sqlQuery = @"EXEC AxionPro.CheckPermission 
                            @RoleId, @ProjectChildModuleDetailId, @OperationId, @HasAccess, @IsActive";

                // Parameters definition
                var parameters = new[]
                {
            new SqlParameter("@RoleId", checkOperationPermissionRequest.RoleIds),
            new SqlParameter("@ProjectChildModuleDetailId", checkOperationPermissionRequest.ProjectChildModuleDetailId),
            new SqlParameter("@OperationId", checkOperationPermissionRequest.OperationId),
            new SqlParameter("@HasAccess", checkOperationPermissionRequest.HasAccess),
            new SqlParameter("@IsActive", checkOperationPermissionRequest.IsActive)
        };

                // Executing the stored procedure and getting the result (BIT value)
                var result = await _context.Database.SqlQueryRaw<bool>(sqlQuery, parameters).ToListAsync();

                // If result is 1 (HasAccess), return true, otherwise false
                if (result != null && result.Count > 0)
                {
                    return result[0];
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle accordingly
                _logger.LogError($"Error in GetHasAccessOperation: {ex.Message}");
                return false;
            }
        }

        public Task<bool> HasPermissionAsync(long userId, string permissionCode)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsTenantValidAsync(long userId, long tenantId)
        {
            throw new NotImplementedException();
        }

        public Task<int> ValidateUserPasswordAsync(string loginId)
        {
            throw new NotImplementedException();
        }

        //public async Task<int> ValidateUserLoginAsync(string loginId)
        //{
        //    try
        //    {
        //        _logger.LogInformation("Validating user login for LoginId: {LoginId}", loginId);
        //        var param = new[] { new SqlParameter("@LoginId", loginId ?? (object)DBNull.Value),
        //              new SqlParameter("@result",SqlDbType.Int) { Direction=ParameterDirection.Output} };


        //        string sqlQuery  = @"DECLARE @Result INT;
        //                    EXEC AxionPro.ValidateUserLogin 
        //                        @LoginId = @loginId, 
        //                        @EmployeeId = @EmployeeId OUTPUT;
        //                    SELECT @EmployeeId;";

        //        var result = await _context.Database.ExecuteSqlRawAsync(sqlQuery, param);

        //        return result;

        //    }
        //    catch (SqlException ex)
        //    {
        //        _logger.LogError(ex, "SQL Exception occurred while validating user login for LoginId: {LoginId}", loginId);
        //        throw new Exception("Database error occurred while validating user login.");
        //    }

        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An error occurred while validating user login for LoginId: {LoginId}");
        //        throw;
        //    }
        //}


    }
}
