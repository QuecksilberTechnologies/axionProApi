﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.UserLogin
{
    public class NewLoginPasswordRequestDTO
    {
        public string LoginId {  get; set; }
        public  string Otp { get; set; }
        public string Password { get; set; }

    }
}
