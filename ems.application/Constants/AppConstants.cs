﻿using ems.application.Interfaces;
using ems.domain.Entity;

namespace ems.application.Constants
{
    public static class AppConstants
    {
        public static readonly int DeviceTypeWeb = 1;
        public static readonly int DeviceTypeMobile = 2;
        public static readonly int DeviceTypeForAll = 3;
        public static  int EmployeeRoll = 14;

        // Add other constants as needed
        public static readonly string DefaultDateFormat = "yyyy-MM-dd";
        // etc.
    }

    public static class ConstantValues
    {

        public static readonly string invalidCredential = "Invalid credentials";
        public static readonly string Duplicate = "Name you inserted is already exist";
        public static readonly string userMissingAttendanceProfile = "Attendance settings not configured or not matched! for this employee.";
        public static readonly string attendanceNotAllowed = "Attendance is not allowed for the employee based on current settings";
        public static readonly string outOfGeoFence = "You are outside the geofence area and cannot mark attendance.";
        public static readonly string invalidId = "Invalid Id";
        public static readonly string invalidPassword= "Invalid credentials";
        public static readonly string successMessage = "Request processed successfully";
        public static readonly string attendanceSucessful = "Attendance successfully marked";
        public static readonly string attendancefail = "Attendance not marked please try again";
        public static readonly bool isSucceeded = true;
        public static readonly bool fail= false;
        public static readonly DateTime ExpireTokenDate = DateTime.UtcNow.AddDays(5);
        public static readonly string IP ="100.100.100.100";
        public static readonly string SuperAdminRoleName = "Super Admin";
        public static readonly string SuperAdminRoleType = "SYSTEM";
        public static readonly string SuperAdminRoleCode = "Auth_0";


        public static readonly string TenantAdminRoleName = "Admin";
        public static readonly string TenantAdminRoleType = "TENANT";
        public static readonly string TenantAdminRoleCode = "TENANT_ADMIN";
       
        
        public static readonly string TenantHRRoleCode = "TENANT_HR";
        public static readonly string TenantHRRoleType = "TENANT_OPERATIONAL";
        public static readonly string TenantHRManagerRoleName = "HR Manager";


        public static readonly string TenantEmployeeRoleCode = "TENANT_EMPLOYEE";
        public static readonly string TenantEmployeeRoleType = "EMPLOYEE";
        public static readonly string TenantEmployeeRoleName = "Employee";




        public static readonly string TenantAllRoleRemark = "This is an auto-generated Admin account by AI for the initial setup of the tenant.";
        public static readonly bool IsByDefaultTrue = true;
        public static readonly bool IsByDefaultFalse = false;         
        public static readonly long SystemUserIdByDefaultZero = 0; // For system-generated entries
        public static readonly string DefaultPassword = "123"; // For system-generated entries
                                                               //   public static readonly DateOnly SystemOnlyTodaysDate= DateOnly.MaxValue;

        //int adminRoleId = await _unitOfWork.RoleRepository.GetRoleIdByRoleInfoAsync(role);
        //public static readonly string RoleCode = "Super-Admin";
        //public static readonly string AdminRoleName = "Admin";
        //public static readonly string AdminRoleRemark = "This is an auto-generated Admin account by AI for the initial setup of the tenant.";

    }
    
}
