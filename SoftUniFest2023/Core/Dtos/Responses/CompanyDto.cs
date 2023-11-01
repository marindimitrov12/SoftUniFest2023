using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Responses
{
    public class CompanyDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string AccessToken { get; set; } = string.Empty;
    }
}
