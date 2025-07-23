using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Region
{
    public class GetAllCountryDTO 
    {
        public int Id { get; set; }

        public string CountryName { get; set; } = null!;

        public string? CountryCode { get; set; }

        public bool? IsActive { get; set; }
    }
}
