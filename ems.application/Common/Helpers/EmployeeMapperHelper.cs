using ems.application.DTOs.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Common.Helpers
{
    public static class EmployeeMapperHelper
    {
        public static GetEmployeeInfoWithAccessResponseDTO ConvertToAccessResponseDTO(CreateEmployeeByTenantAdminRequestDTO source)
        {
            var result = new GetEmployeeInfoWithAccessResponseDTO();

            var sourceProps = typeof(CreateEmployeeByTenantAdminRequestDTO).GetProperties();
            var targetProps = typeof(GetEmployeeInfoWithAccessResponseDTO).GetProperties();

            foreach (var sourceProp in sourceProps)
            {
                var targetProp = targetProps.FirstOrDefault(p => p.Name.Equals(sourceProp.Name, StringComparison.OrdinalIgnoreCase));
                if (targetProp == null)
                    continue;

                var value = sourceProp.GetValue(source);

                var fieldType = typeof(FieldWithAccess<>).MakeGenericType(sourceProp.PropertyType);
                var instance = Activator.CreateInstance(fieldType, value, true); // true = readonly

                targetProp.SetValue(result, instance);
            }

            return result;
        }
    }
}
 
