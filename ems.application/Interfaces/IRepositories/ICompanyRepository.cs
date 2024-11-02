using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Interfaces.IRepositories
{
    public interface ICompanyRepository
    {
        string GetCompanyName(string companyName);
    }
}
