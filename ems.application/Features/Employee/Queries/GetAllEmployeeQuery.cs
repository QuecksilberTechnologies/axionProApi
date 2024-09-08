using ems.application.Interfaces.Repositories;
using ems.application.Interfaces.UnitOfWork;
using MediatR;

namespace ems.application.Features.Employee.Queries;

public class GetAllEmployeeQuery :IRequest<string>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public GetAllEmployeeQuery(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}

public class GetAllEmployeeQueryHandler : IRequestHandler<GetAllEmployeeQuery, string>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetAllEmployeeQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork= unitOfWork;
    }

    public async Task<string> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
    {
        _unitOfWork.Employees.GetAll();

        return $"{request.FirstName} {request.LastName}";
    }
}
