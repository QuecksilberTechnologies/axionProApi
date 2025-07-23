using ems.application.DTOs.EmployeeDTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.EmployeeCmd.Queries
{
    public class GetEmployeeByIdQuery : IRequest<EmployeeDTO>
    {
        public int Id { get; set; }

        public GetEmployeeByIdQuery(int id)
        {
            Id = id;
        }
    }

}
