using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Employee
{
    /// <summary>
    /// post-request get self profile info
    /// </summary>

    public class GetProfileInfoRequestDTO
    {
        /// <summary> self user login id Required</summary>

        [Required]
        public string SelfLoginId { get; set; }
    }
}
