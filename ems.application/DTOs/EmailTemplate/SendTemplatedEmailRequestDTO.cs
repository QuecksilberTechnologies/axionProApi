using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.EmailTemplate
{
    public class SendTemplatedEmailRequestDTO
    {
        public string TemplateCode { get; set; }
        public string ToEmail { get; set; }
        public long TenantId { get; set; }
        public Dictionary<string, string> Placeholders { get; set; }
    }
}
