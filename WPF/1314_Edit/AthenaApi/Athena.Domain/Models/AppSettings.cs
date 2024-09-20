using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class AppSettings
    {
        public int JwtExpiryInHours { get; set; }
        public string JwtSecret { get; set; } = string.Empty;
        public string TripleDESKey { get; set; } = string.Empty;
        public List<string>? AlowedApplications { get; set; }
        public List<string>? AlowedClients { get; set; }
        
    }
}
