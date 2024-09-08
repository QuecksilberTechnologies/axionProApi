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

    public GetAllEmployeeQueryHandler()
    {
        
    }
    public async Task<string> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
    {
        return $"{request.FirstName} {request.LastName}";
    }
}
