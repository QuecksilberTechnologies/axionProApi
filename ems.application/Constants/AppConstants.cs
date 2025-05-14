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


    }
}
