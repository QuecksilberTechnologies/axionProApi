using ems.application.DTOs.Employee;
using ems.application.Wrappers;
using MediatR;

namespace ems.application.Common.Commands
{
    public class GetBasicEmployeeInfoCommand : IRequest<ApiResponse<GetEmployeeInfoResponseDTO>>
    {
        public EmployeeInfoRequestDTO DTO { get; }

        public GetBasicEmployeeInfoCommand(EmployeeInfoRequestDTO dTO)
        {
            DTO = dTO;
        }
    }
}
