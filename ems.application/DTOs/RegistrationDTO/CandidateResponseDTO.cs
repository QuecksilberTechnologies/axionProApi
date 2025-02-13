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

        public bool Success { get; set; }  // Operation success status (true/false)
        public long? CandidateId { get; set; }  // CandidateId if success, else null
        public string Message { get; set; }  // Success/Failure message
                                             //   public CandidateInfoDTO CandidateInfoDTO { get; set; }  // Employee Information

    }
}
