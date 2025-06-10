using ems.application.DTOs.EmailTemplate;
using ems.application.Wrappers;
using MediatR;

namespace ems.application.Features.EmailTemplateCmd.Queries
{
    public class GetEmailTemplateByCodeQuery : IRequest<ApiResponse<EmailTemplateDTO>>
    {
        public string Code { get; set; }

        public GetEmailTemplateByCodeQuery(string code)
        {
            Code = code;
        }
    }
}
