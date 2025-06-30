using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Interfaces.IEmail
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string toEmail, string subject, string body, string token, long tenantId);
        Task<bool> SendOtpEmailAsync(string toEmail, string subject, string body, long tenantId, string otp);
        Task<bool> SendTemplatedEmailAsync(string templateCode, string toEmail, long tenantId, Dictionary<string, string> placeholders);
        
    }
}
