using ems.application.Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Employee
{
    public class GetDisabledEmployeeProfileInfoRequestDTO
    {
        [AccessControl(ReadOnly = true)]
        public long Id { get; set; }

        [AccessControl(ReadOnly = true)]
        public long  TenantId { get; set; }

        [AccessControl(ReadOnly = true)]
        public int EmployeeDocumentId { get; set; }

        [AccessControl(ReadOnly = true)]
        public string EmployementCode { get; set; }

        [AccessControl(ReadOnly = true)]
        public string LastName { get; set; } = null!;

        [AccessControl(ReadOnly = true)]
        public string MiddleName { get; set; }

        [AccessControl(ReadOnly = true)]
        public string FirstName { get; set; } = null!;

        [AccessControl(ReadOnly = true)]
        public DateTime DateOfBirth { get; set; }

        [AccessControl(ReadOnly = true)]
        public DateTime DateOfOnBoarding { get; set; }

        [AccessControl(ReadOnly = true)]
        public DateTime? DateOfExit { get; set; }

        // [AccessControl(ReadOnly = true)]
        // public int? SpecializationId { get; set; } // commented as in original

        [AccessControl(ReadOnly = true)]
        public int DesignationId { get; set; }

        [AccessControl(ReadOnly = true)]
        public int EmployeeTypeId { get; set; }

        [AccessControl(ReadOnly = true)]
        public int DepartmentId { get; set; }

        [AccessControl(ReadOnly = true)]
        public string OfficialEmail { get; set; }

        [AccessControl(ReadOnly = true)]
        public bool HasPermanent { get; set; }

        [AccessControl(ReadOnly = true)]
        public int FunctionalId { get; set; }

        [AccessControl(ReadOnly = true)]
        public int ReferalId { get; set; }

        [AccessControl(ReadOnly = true)]
        public string Remark { get; set; }

        [AccessControl(ReadOnly = true)]
        public string Description { get; set; }





    }
}
