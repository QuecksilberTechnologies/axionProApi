using ems.application.Interfaces.IRepositories;
using ems.domain.Entity;
using ems.persistance.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ems.persistance.Repositories
{
    public class EmailTemplateRepository : IEmailTemplateRepository
    {
        private readonly WorkforceDbContext _context;
        private readonly ILogger<EmailTemplateRepository> _logger;

        public EmailTemplateRepository(WorkforceDbContext context, ILogger<EmailTemplateRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task AddTemplateAsync(EmailTemplate template)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTemplateAsync(int templateId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EmailTemplate>> GetAllTemplatesAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateTemplateAsync(EmailTemplate template)
        {
            throw new NotImplementedException();
        }

        // ✅ Get list of templates by template code
        public async Task<List<EmailTemplate>> GetTemplateByCodeAsync(string templateCode)
        {
            try
            {
                return await _context.EmailTemplates
                    .Where(t => t.TemplateCode == templateCode && t.IsActive)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching templates by code: {TemplateCode}", templateCode);
                return new List<EmailTemplate>();
            }
        }

        // ✅ Get a single template (latest active one)
        public async Task<EmailTemplate?> GetTemplateByCodeAsync()
        {
            try
            {
                return await _context.EmailTemplates
                    .Where(t => t.IsActive)
                    .OrderByDescending(t => t.AddedDateTime)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching a single email template.");
                return null;
            }
        }
    }
}
