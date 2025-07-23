using ems.application.DTOs.Employee;
using ems.application.Wrappers;
using MediatR;

namespace ems.application.Features.EmployeeCmd.Commands
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
