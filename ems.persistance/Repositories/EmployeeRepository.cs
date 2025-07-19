using ems.application.Common.Attributes;
using ems.application.Common.Helpers;
using ems.application.Constants;
using ems.application.DTOs.Common;
using ems.application.Interfaces.IRepositories;
using ems.domain.Entity;
using ems.persistance.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ems.persistance.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private WorkforceDbContext context;
        private ILogger _logger;

        public EmployeeRepository(WorkforceDbContext context)
        {
            this.context = context;
          //  this._logger = logger;  
        }

        public async Task<Employee> AddAsync(Employee entity)
        {
            // Entity ko DbSet mein add karte hain
            await context.AddAsync(entity);

            // Changes ko save karte hain database mein
            await context.SaveChangesAsync();

            // Added entity ko return karte hain
            return entity;
        }

        public Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Employee?> GetEmployeeInfoForLoginByIdAsync(long employeeId)
        {
            try
            {
                _logger?.LogInformation("Fetching employee details for ID: {EmployeeId}", employeeId);

                var employee = await context.Employees
                  .Where(e =>    e.Id == employeeId  && e.IsActive == ConstantValues.IsByDefaultTrue &&
                  e.IsSoftDeleted == ConstantValues.IsByDefaultFalse)
                  .Select(e => new Employee
                  {
                      Id = e.Id,
                      TenantId = e.TenantId ?? 0L,  // 👈 Null fallback to 0L
                      DesignationId = e.DesignationId,
                      DepartmentId = e.DepartmentId,
                      EmployeeTypeId = e.EmployeeTypeId,
                      OfficialEmail = e.OfficialEmail,
                      FirstName = e.FirstName,
                      MiddleName = e.MiddleName,
                      LastName = e.LastName
                  })
                    .FirstOrDefaultAsync();

                return employee; // Sirf required fields return ho rahe hain!
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "An error occurred while fetching employee details for ID: {EmployeeId}", employeeId);
                throw;
            }
        }

        public async Task<EmployeePersonalDetail?> GetEmployeePersonalInfoByIdAsync(long employeeId)
        {
            try
            {
                _logger?.LogInformation("Fetching employee personal details for EmployeeId: {EmployeeId}", employeeId);

                var personalDetail = await context.EmployeePersonalDetails
                    .FirstOrDefaultAsync(e => e.EmployeeId == employeeId && e.IsActive == true && !e.IsSoftDeleted ==true);

                if (personalDetail == null)
                {
                    _logger?.LogWarning("No active personal detail found for EmployeeId: {EmployeeId}", employeeId);
                }

                return personalDetail;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "An error occurred while fetching personal details for EmployeeId: {EmployeeId}", employeeId);
                throw;
            }
        }


        public async Task<EmployeeBankDetail?> GetEmployeeBankInfoByIdAsync(long employeeId)
        {
            try
            {
                _logger?.LogInformation("Fetching employee bank details for ID: {EmployeeId}", employeeId);

                EmployeeBankDetail? bankDetail = await context.EmployeeBankDetails
                    .Where(e => e.EmployeeId == employeeId && e.IsActive == true && e.IsSoftDeleted == false)
                    .FirstOrDefaultAsync();

                return bankDetail;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "An error occurred while fetching employee bank details for ID: {EmployeeId}", employeeId);
                throw;
            }
        }


        public async Task<EmployeeExperience?> GetEmployeeExperienceInfoByIdAsync(long employeeId)
        {
            try
            {
                _logger?.LogInformation("Fetching employee experience details for ID: {EmployeeId}", employeeId);

                EmployeeExperience? experience = await context.EmployeeExperiences
                    .Where(e => e.EmployeeId == employeeId && e.IsActive == true && (e.IsSoftDeleted == null || e.IsSoftDeleted == false))
                    .FirstOrDefaultAsync();

                return experience;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "An error occurred while fetching employee experience details for ID: {EmployeeId}", employeeId);
                throw;
            }
        }

        public async Task<EmployeeEducation?> GetEmployeeEducationByIdAsync(long employeeId)
        {
            try
            {
                return await context.EmployeeEducations
                    .FirstOrDefaultAsync(x =>
                        x.EmployeeId == employeeId &&                     
                        (x.IsSoftDeleted == null || x.IsSoftDeleted == false));
            }
            catch (Exception ex)
            {
                // Optionally log here if you have logger in repository
                // _logger.LogError(ex, "Error fetching employee education");

                throw; // propagate exception to upper layer
            }
        }


        public async Task<Employee?> GetEmployeeByIdAsync(long employeeId)
        {
            try
            {
                _logger?.LogInformation("Fetching employee details for ID: {EmployeeId}", employeeId);

                Employee? employee = await context.Employees
                    .Where(e => e.Id == employeeId && e.IsActive == true && e.IsSoftDeleted==false)
                    .FirstOrDefaultAsync();

                return employee; // पूरी entity return करें
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "An error occurred while fetching employee details for ID: {EmployeeId}", employeeId);
                throw;
            }
        }

        public async Task<long> AddEmployeePersonalDetailByAdminAsync(long empId, long addedId)
        {
            if (empId <= 0)
                throw new ArgumentException("Invalid EmployeeId", nameof(empId));

            if (addedId <= 0)
                throw new ArgumentException("Invalid AddedById", nameof(addedId));

            var personalInfo = new EmployeePersonalDetail
            {
                EmployeeId = empId,
                AddedById = addedId,
                AddedDateTime = DateTime.UtcNow,
                IsActive = true,
                // Add other default fields if needed
            };

            await context.EmployeePersonalDetails.AddAsync(personalInfo);
            await context.SaveChangesAsync();

            return personalInfo.Id; // ✅ Return the created entity, not DbSet
        }


        public async Task<long> AddEmployeeByAdminAsync(Employee entity, long addedId)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Employee entity cannot be null.");

            // Optional: Validate required properties
            if (string.IsNullOrWhiteSpace(entity.FirstName))
                throw new ArgumentException("First name is required.", nameof(entity.FirstName));

            if (string.IsNullOrWhiteSpace(entity.EmployementCode))
                throw new ArgumentException("Employment code is required.", nameof(entity.EmployementCode));

            try
            {
                // Add to DB
                await context.Employees.AddAsync(entity);
                await context.SaveChangesAsync();

                // Return generated EmployeeId
                return entity.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding employee in AddEmployeeByAdminAsync.");
                throw new Exception("An error occurred while adding employee. Please try again later.", ex);
            }
        }

        //public async Task<bool> UpdateEmployeeFieldAsync(UpdateSingleFieldRequestDTO request)
        //{
        //    var tableType = request.TableName.ToLower() switch
        //    {
        //        "employeepersonaldetail" => typeof(EmployeePersonalDetail),
        //        "employeeeducation" => typeof(EmployeeEducation),
        //        "employeeexperience" => typeof(EmployeeExperience),
        //        "employeebankdetail" => typeof(EmployeeBankDetail),
        //        "employee" => typeof(Employee),
        //        _ => null
        //    };

        //    if (tableType == null) return false;

        //    var dbSet = context.Set<Employee>(tableType.Name.ToString());
        //    var record = await dbSet.FindAsync(request.EmployeeId);
        //    if (record == null) return false;

        //    var prop = tableType.GetProperty(request.FieldName);
        //    if (prop == null || !Attribute.IsDefined(prop, typeof(UpdatableAttribute)))
        //        return false;

        //    prop.SetValue(record, Convert.ChangeType(request.NewValue, prop.PropertyType));

        //    var updatedProp = tableType.GetProperty("UpdatedDateTime");
        //    updatedProp?.SetValue(record, DateTime.UtcNow);

        //    var updatedByProp = tableType.GetProperty("UpdatedById");
        //    updatedByProp?.SetValue(record, request.UpdatedById);

        //    await  context.SaveChangesAsync();
        //    return true;
        //}
        public async Task<bool> UpdateEmployeeFieldAsync(long employeeId, string fieldName, long updatedById)
      {
        try
        {
                var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == employeeId && !e.IsSoftDeleted == true && !e.IsActive == false && e.IsEditAllowed ==true);

            

            // ✅ Audit trail
            employee.UpdatedById = updatedById;
            employee.UpdatedDateTime = DateTime.UtcNow;

            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EXCEPTION] UpdateEmployeeFieldAsync: {ex.Message}");
            return false;
        }
    }

        public async Task<long> AddEmployeeBankInfoByAdminAsync(long employeeId, long addedId)
        {
            try
            {
                if (employeeId <= 0)
                    throw new ArgumentNullException(nameof(employeeId), "EmployeeId cannot be null or zero.");

                var bankInfo = new EmployeeBankDetail
                {
                    EmployeeId = employeeId,
                    IsPrimaryAccount = false,
                    IsSoftDeleted = false,
                    IsEditAllowed = true,
                    IsInfoVerified = false,
                    AddedById = addedId,
                    AddedDateTime = DateTime.Now
                 
                    // You can also add TenantId, AddedById if available
                };

                await  context.EmployeeBankDetails.AddAsync(bankInfo);
                await  context.SaveChangesAsync();

                return bankInfo.Id;
            }
            catch (ArgumentNullException ex)
            {
                // Handle specific null input
                // Optionally log here
                throw new Exception("Invalid employee ID provided. Details: " + ex.Message);
            }
            catch (DbUpdateException dbEx)
            {
                // Handle DB issues (FK constraint, connection etc.)
                throw new Exception("Database error occurred while adding employee bank info: " + dbEx.Message);
            }
            catch (Exception ex)
            {
                // General fallback
                throw new Exception("An unexpected error occurred while adding employee bank info: " + ex.Message);
            }
        }


        public async Task<long> AddEmployeeEducationInfoByPermittedUserAsync(long employeeId, long addedId)
        {
            try
            {
                if (employeeId <= 0)
                    throw new ArgumentNullException(nameof(employeeId), "EmployeeId cannot be null or zero.");

                var educationInfo = new EmployeeEducation
                {
                    EmployeeId = employeeId,
                    IsSoftDeleted = false,
                    IsEditAllowed = true,
                    IsInfoVerified = false,
                    AddedDateTime = DateTime.UtcNow,
                    IsActive = true,
                    AddedById= addedId,
                   
                    // Optional: Add AddedById if context/user info available
                };

                await  context.EmployeeEducations.AddAsync(educationInfo);
                await  context.SaveChangesAsync();

                return educationInfo.Id;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception("Invalid employee ID provided. Details: " + ex.Message);
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Database error occurred while adding employee education info: " + dbEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding employee education info: " + ex.Message);
            }
        }

        public async Task<long> AddEmployeeExperienceInfoByPermittedUserAsync(long employeeId, long addedId)
        {
            try
            {
                if (employeeId <= 0)
                    throw new ArgumentNullException(nameof(employeeId), "EmployeeId cannot be null or zero.");

                var experience = new EmployeeExperience
                {
                    EmployeeId = employeeId,
                    IsSoftDeleted = false,
                    IsEditAllowed = true,
                    IsInfoVerified = false,
                    IsExperienceVerified = false,
                    IsExperienceVerifiedByCall = false,
                    IsExperienceVerifiedByMail = false,
                    AddedById = addedId,
                    
                    AddedDateTime = DateTime.UtcNow
                    // Optionally: TenantId, AddedById etc. bhi set karo if context mein hai
                };

                await context.EmployeeExperiences.AddAsync(experience);
                await context.SaveChangesAsync();

                return experience.Id;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception("Invalid employee ID provided. Details: " + ex.Message);
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Database error occurred while adding employee experience info: " + dbEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding employee experience info: " + ex.Message);
            }
        }

        public async Task<bool> UpdateEmployeeFieldAsync(long employeeId, string entity, string fieldName, object? fieldValue, long updatedById)
        {
            try
            {
                object? entityObject = null;
                Type? entityType = null;

                switch (entity)
                {
                    case "Employee":
                        entityObject = await context.Employees
                            .FirstOrDefaultAsync(e => e.Id == employeeId && !e.IsSoftDeleted ==true && e.IsActive == true);
                        entityType = typeof(Employee);
                        break;

                    case "EmployeeBankDetail":
                        entityObject = await context.EmployeeBankDetails
                            .FirstOrDefaultAsync(e => e.EmployeeId == employeeId && !e.IsSoftDeleted == true && e.IsActive == true);
                        entityType = typeof(EmployeeBankDetail);
                        break;

                    case "EmployeePersonalDetail":
                        entityObject = await context.EmployeePersonalDetails
                            .FirstOrDefaultAsync(e => e.EmployeeId == employeeId && !e.IsSoftDeleted == true && e.IsActive == true);
                        entityType = typeof(EmployeePersonalDetail);
                        break;

                    case "EmployeeExperience":
                        entityObject = await context.EmployeeExperiences
                            .FirstOrDefaultAsync(e => e.EmployeeId == employeeId && !e.IsSoftDeleted == true && e.IsActive == true);
                        entityType = typeof(EmployeeExperience);
                        break;
                    case "EmployeeEducation":
                        entityObject = await context.EmployeeEducations
                            .FirstOrDefaultAsync(e => e.EmployeeId == employeeId && !e.IsSoftDeleted == true && e.IsActive == true);
                        entityType = typeof(EmployeeEducation);
                        break;
                    // 👉 Add more cases as needed...
                    default:
                        return false;
                }

                if (entityObject == null || entityType == null)
                    return false;

                // ✅ Get property with case-insensitive search
                var propertyInfo = entityType.GetProperty(fieldName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                if (propertyInfo == null || !propertyInfo.CanWrite)
                    return false;

                // ✅ Safe type conversion
                if (!TryConvertObjectToValue.TryConvertValue(fieldValue, propertyInfo.PropertyType, out var convertedValue))
                {
                    Console.WriteLine($"[Conversion Failed] Field: {fieldName}, Value: {fieldValue}");
                    return false;
                }

                // ✅ Set the converted value
                propertyInfo.SetValue(entityObject, convertedValue);

                // ✅ Set audit fields
                var updatedByIdProp = entityType.GetProperty("UpdatedById", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                var updatedDateTimeProp = entityType.GetProperty("UpdatedDateTime", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                if (updatedByIdProp != null && updatedByIdProp.CanWrite)
                    updatedByIdProp.SetValue(entityObject, updatedById);

                if (updatedDateTimeProp != null && updatedDateTimeProp.CanWrite)
                    updatedDateTimeProp.SetValue(entityObject, DateTime.UtcNow);

                // ✅ Save changes
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION] UpdateEmployeeFieldAsync: {ex.Message}");
                return false;
            }
        }




        // Method to fetch employee type by ID

    }
}




 
 






