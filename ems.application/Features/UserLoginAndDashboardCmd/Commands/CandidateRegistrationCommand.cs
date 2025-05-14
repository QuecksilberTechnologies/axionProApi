using ems.application.DTOs.Registration;
using ems.application.DTOs.UserLogin;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.UserLoginAndDashboardCmd.Commands
{

    public class CandidateRegistrationCommand : IRequest<ApiResponse<CandidateResponseDTO>>
    {
        public CandidateRequestDTO RequestCandidateRegistrationDTO { get; set; }


        public CandidateRegistrationCommand(CandidateRequestDTO candidateRegistrationRequestDTO)
        {
            RequestCandidateRegistrationDTO = candidateRegistrationRequestDTO;
        }



    }
}
