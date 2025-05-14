﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Leave
{
    public class GetAllLeaveTypeDTO
    {

        public int Id { get; set; }
        public string LeaveName { get; set; } = null!;

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public long? AddedById { get; set; }
        public long? UpdatedById { get; set; }
    }
}
