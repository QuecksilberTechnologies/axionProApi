using ems.application.DTOs.RegistrationDTO;
using ems.application.Features.UserLoginAndDashboardCmd.Commands;
using ems.domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Interfaces.IRepositories
{
    public interface ICandidateRegistrationRepository
    {
        // Create: Add a new candidate
        Task<long> AddCandidateAsync(Candidate candidate);

        // Read: Get a candidate by ID
        Task<CandidateResponseDTO> GetCandidateByIdAsync(int candidateId);

        // Read: Get all candidates
        Task<IEnumerable<CandidateResponseDTO>> GetAllCandidatesAsync();

        // Update: Update candidate details
        Task<bool> UpdateCandidateAsync(CandidateResponseDTO candidate);

        // Delete: Delete a candidate by ID
        Task<bool> DeleteCandidateAsync(int candidateId);
        Task<bool> IsEmailPANAdharPhoneExistsAsync(CandidateRequestDTO request);
        Task<bool> IsCandidateBlackListedAsync(CandidateRequestDTO request);
        Task<long> GetCandidateIdAsync(CandidateRequestDTO request);
    }

}
