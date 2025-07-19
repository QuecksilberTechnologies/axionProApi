using ems.application.DTOs.Employee;
using ems.application.Wrappers;
using MediatR;

namespace ems.application.Common.Commands
{
    public class GetBasicEmployeeInfoCommand : IRequest<ApiResponse<GetEmployeeInfoResponseDTO>>
    {
        public GetEmployeeInfoRequestDTO DTO { get; }

        public GetBasicEmployeeInfoCommand(GetEmployeeInfoRequestDTO dTO)
        {
            DTO = dTO;
        }
    }
}
