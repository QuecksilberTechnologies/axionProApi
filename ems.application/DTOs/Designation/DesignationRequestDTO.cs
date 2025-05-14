using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Designation
{
    public class DesignationRequestDTO
    {
        public long Id { get; set; }
        public int SelfRoleId { get; set; }
        public int DesignationId { get; set; }
        public int DesignationtId { get; set; }
    }
}
