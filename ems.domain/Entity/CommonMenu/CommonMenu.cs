using ems.domain.Common;
using System;
using System.Collections.Generic;

namespace ems.domain.Entity.CommonMenu;

public partial class CommonMenu : BaseEntity
{
  
        public int Id { get; set; }

        public string MenuName { get; set; } = null!;

        public string? MenuUrl { get; set; }

        public int? ParentMenuId { get; set; }

        public string Remark { get; set; } = null!;

        public int? ForPlatform { get; set; }

        public bool IsMenuDisplayInUi { get; set; }

        public bool IsSubMenu { get; set; }

        public bool IsDisplayable { get; set; }

        public bool IsActive { get; set; }

        public bool HasAccess { get; set; }
            public virtual ICollection<CommonMenu> InverseParentMenu { get; set; } = new List<CommonMenu>();

        public virtual CommonMenu? ParentMenu { get; set; }
    }

