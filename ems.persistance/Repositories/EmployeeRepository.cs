using ems.application.Interfaces.Repositories;
using Persistance.Context;

namespace ems.persistance.Repositories
{

    public class EmployeeRepository:IEmployeeRepository
    {
        protected readonly EmsDbContext _context;
        public EmployeeRepository(EmsDbContext context)
        {
            _context = context;
        }

        public void GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
