using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Employee
{
    /// <summary>
    /// post-request to update any employe info :edu/basic,personal,bank,exp 
    /// </summary>

    public class UpdateGenricAllEmployeeEntityRequestDTO
    {
       

        [Required]
        public long EmployeeId { get; set; }
        [Required]
        public string EntityName { get; set; } = string.Empty;
        /// <summary>
        /// actual field name
        /// </summary>

        [Required]
        public string FieldName { get; set; } = string.Empty;
        /// <summary>
        ///  new value which is going to update!
        /// </summary>

        [Required]
        public object? FieldValue { get; set; }


        [Required]
        public long UpdatedById { get; set; }
    }
}
