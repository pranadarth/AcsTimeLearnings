using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class LoginRequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Application { get; set; }
    }
}
