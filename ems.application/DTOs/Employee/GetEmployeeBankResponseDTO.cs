using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Employee
{
    public class GetEmployeeBankResponseDTO
    {
        public int EmployeeId { get; set; }

        public string? BankName { get; set; }

        public string? AccountNumber { get; set; }

        public string? IFSCCode { get; set; }

        public string? BranchName { get; set; }

        public string? AccountType { get; set; } // e.g., Savings, Current, etc.

        public string? UPIId { get; set; }

        public bool IsPrimaryAccount { get; set; }

        public long? AddedById { get; set; }

        public DateTime? AddedDateTime { get; set; }

        public long? UpdatedById { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public long? DeletedById { get; set; }

        public DateTime? DeletedDateTime { get; set; }

        public bool IsSoftDeleted { get; set; }

        public long? InfoVerifiedById { get; set; }

        public bool IsInfoVerified { get; set; }

        public DateTime? InfoVerifiedDateTime { get; set; }

        public bool IsEditAllowed { get; set; }

        public bool IsActive { get; set; }
    }

}
