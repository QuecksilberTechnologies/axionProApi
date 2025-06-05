using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Common.Helpers
{
    public static class EmailTemplateRenderer
    {
        public static string RenderBody(string template, Dictionary<string, string> values)
        {
            foreach (var item in values)
            {
                template = template.Replace($"{{{{{item.Key}}}}}", item.Value);
            }
            return template;
        }
    }

}
