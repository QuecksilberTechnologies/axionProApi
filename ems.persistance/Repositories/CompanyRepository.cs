using ems.application.Interfaces.Context;
using ems.application.Interfaces.Repositories;
using Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.persistance.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IEmsDbContext _emsDbContext;
        public CompanyRepository(IEmsDbContext emsDbContext)
        {
            _emsDbContext = emsDbContext;
        }
        public string GetCompanyName(string companyName)
        {
            throw new NotImplementedException();
        }
    }
}
