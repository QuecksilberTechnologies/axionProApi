using ems.application.DTOs.EmployeeDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.RegistrationDTO
{
    public class CandidateResponseDTO
    {
      
            public long Id { get; set; }
            public bool Success { get; set; }
            public string? Token { get; set; }
            public string? ExpireWithin { get; set; }
            public string? RefreshToken { get; set; }
            public CandidateInfoDTO CandidateInfoDTO { get; set; }  // Employee Information


        
    }
}
