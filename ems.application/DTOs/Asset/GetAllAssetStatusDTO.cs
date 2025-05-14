using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Asset
{
    public class GetAllAssetStatusDTO
    {
        public int Id { get; set; }
        public string StatusName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }

}
