using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class LoginDetailsModel
    {
        public string Token { get; set; }
        public string UserId { get; set; }
        public int UserSk { get; set; }
        public int ExpiryInHours { get; set; }
        public DateTime GeneratedDateTime { get; set; }
    }
}
