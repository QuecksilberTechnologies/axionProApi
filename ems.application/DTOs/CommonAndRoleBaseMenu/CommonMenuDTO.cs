using ems.domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.CommonAndRoleBaseMenu
{
    public class CommonMenuDTO:BaseEntity
    {
        public int Id { get; set; }
        public string MenuName { get; set; }
        public string? MenuUrl { get; set; }
        public int? ParentMenuId { get; set; }
        public int? ForPlatform { get; set; }
        public byte[]? ImageIcon { get; set; }
        public bool IsMenuDisplayInUi { get; set; }
        public bool IsSubMenu { get; set; }
        public bool IsDisplayable { get; set; }
        public bool IsActive { get; set; }
        public bool HasAccess { get; set; }
    }

  

}
