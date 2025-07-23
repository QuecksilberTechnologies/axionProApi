using ems.application.DTOs.EmployeeDTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.EmployeeCmd.Commands
{
    //public class CreateEmployeeCommand :IRequest <int>
    //{
    //    public string EmployeeName { get; set; }
    //    public string Department { get; set; }
    //    public string EmployeeType { get; set; }
    //    public string Specialist { get; set; }

    //    internal class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
    //    {
    //        public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    //        {
    //            return (5);
    //        }
    //    }
    //}
    public class CreateEmployeeCommand : IRequest<EmployeeDTO>
    {
        public CreateEmployeeDTO EmployeeDTO { get; set; }

        public CreateEmployeeCommand(CreateEmployeeDTO createEmployee)
        {
            EmployeeDTO = createEmployee;
        }

    }




}
