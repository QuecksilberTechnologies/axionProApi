using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Interfaces.IRepositories
{
    public interface IEntitySecurityValidator
    {
        Task<bool> HasPermissionAsync(long userId, string permissionCode);
        Task<bool> IsTenantValidAsync(long userId, long tenantId);
        Task<bool> IsDuplicateAssetTypeAsync(long tenantId, string typeName);
    }

}
