using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Module
{
     /// <summary>
    ///  post-request to create module 
    /// </summary>
    public class CreateMainModuleRequestDTO
    {
       /// <summary> ProductOwnerId Required</summary>
        public required int ProductOwnerId { get; set; }

        [MaxLength(50)]
            public required string ModuleCode { get; set; }

            /// <summary> Module name Required</summary>

       
            public required string ModuleName { get; set; }

       

        /// <summary> Display Name Required</summary>
        public required bool IsCommonMenu { get; set; } = false;

        public required string DisplayName { get; set; }

        /// <summary> path Required</summary>

        public required string  Path { get; set; }
            
        public string? SubModuleURL { get; set; }

         /// <summary> Display Name Required</summary>

        //   public int? ParentModuleId { get; set; } = null;

            public bool IsModuleDisplayInUI { get; set; } = false;           

            public bool IsActive { get; set; } = true;

            [MaxLength(200)]
            public string? ImageIconWeb { get; set; }

            [MaxLength(200)]
            public string? ImageIconMobile { get; set; }

            public int? ItemPriority { get; set; }

            [MaxLength(500)]
            public string? Remark { get; set; }

     


    }

}
