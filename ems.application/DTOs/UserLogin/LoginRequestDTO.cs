﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.UserLogin
{
    public class LoginRequestDTO
    {
        public string LoginId { get; set; }          // User's login ID
        public string Password { get; set; }         // User's password
        public string MacAddress { get; set; }       // User's Mac Address (optional)
        public string IpAddress { get; set; }        // User's IP Address (optional)
        public double Latitude { get; set; }          // User's latitude for geolocation (optional)
        public double Longitude { get; set; }         // User's longitude for geolocation (optional)
        public int LoginDevice { get; set; }         // The device being used for login (optional)
    }

}
