using ems.application.DTOs.RegistrationDTO;
using ems.application.DTOs.UserLogin;
using ems.application.Features.UserLoginAndDashboardCmd.Commands;
using ems.application.Interfaces;
using ems.application.Interfaces.IRepositories;
using ems.domain.Entity;
using ems.persistance.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ems.persistance.Repositories
{
    public class CandidateRegistrationRepository : ICandidateRegistrationRepository
    {
        private WorkforceDbContext _context;
        private ILogger _logger;

         
        public CandidateRegistrationRepository(WorkforceDbContext? context, ILogger<CandidateRegistrationRepository>? logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task<int> AddCandidateAsync(CandidateRequestDTO candidate)
        {
            //try
            //{
            //}
            //catch (Exception ex)
            //{
            //    // Log error
            //    _logger?.LogError(ex, "");

            //    // Re-throw with user-friendly message
            //    throw new Exception("", ex);
            //}
            return null;
        }

        public Task<bool> DeleteCandidateAsync(int candidateId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CandidateResponseDTO>> GetAllCandidatesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CandidateResponseDTO> GetCandidateByIdAsync(int candidateId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsEmailPANAdharPhoneExistsAsync(CandidateRegistrationCommand request)
        {
            try
            {
                if (request == null || request.RequestCandidateRegistrationDTO == null)
                    throw new ArgumentNullException(nameof(request), "Request or CandidateRegistrationDTO cannot be null.");

                var dto = request.RequestCandidateRegistrationDTO;

                // Check for duplicate candidate data
                var isDuplicate = await _context.Candidates.AnyAsync(c =>
                    c.Email == dto.Email ||
                    c.PhoneNumber == dto.PhoneNumber ||
                    c.Pan == dto.Pan ||
                    c.Aadhaar == dto.Aadhaar);

                return isDuplicate;
            }
            catch (ArgumentNullException ex)
            {
                _logger?.LogError(ex, "Null input encountered in IsEmailPANAdharPhoneExistsAsync.");
                throw; // Rethrow to ensure the caller is aware of the issue
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "An error occurred while checking for duplicate candidate data.");
                throw new Exception("An unexpected error occurred. Please try again later.", ex);
            }
        }

        public Task<bool> IsEmailPANAdharPhoneExistsAsync(CandidateRequestDTO request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCandidateAsync(CandidateResponseDTO candidate)
        {
            throw new NotImplementedException();
        }
    }
}
