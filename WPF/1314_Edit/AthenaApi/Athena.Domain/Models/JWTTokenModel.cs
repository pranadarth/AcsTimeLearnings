using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class JWTTokenModel
    {
        public string Token { get; set; }
        public DateTime GeneratedDateTime { get; set; }
        public int ExpiryInHours { get; set; }
    }
}
