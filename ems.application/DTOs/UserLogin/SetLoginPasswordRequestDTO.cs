﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.UserLogin
{
    public class SetLoginPasswordRequestDTO
    {
        public string LoginId { get; set; }          // User's login ID
        public long EmployeeId { get; set; }          // User's login ID
        public long? TenantId { get; set; }          // User's login ID
        public bool HasFirstLogin { get; set; }          // User's login ID
        public string PasswordOld { get; set; }         // User's password
        public string PasswordNew { get; set; }         // User's password
        public string MacAddress { get; set; }       // User's Mac Address (optional)
        public string IpAddressPublic { get; set; }        // User's IP Address (optional)
        public required string IpAddressLocal { get; set; }        // User's IP Address (optional)
        public double Latitude { get; set; }          // User's latitude for geolocation (optional)
        public double Longitude { get; set; }         // User's longitude for geolocation (optional)
        public int LoginDevice { get; set; }         // The device being used for login (optional)

    }
}
