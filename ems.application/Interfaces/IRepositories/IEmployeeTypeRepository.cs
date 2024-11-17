using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Interfaces.IRepositories
{
    public interface IEmployeeTypeRepository
    {
      public  Task<EmployeeType> GetEmployeeTypeByIdAsync(int? employeeTypeId);
    }
}
