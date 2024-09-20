using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class UpdateGenericYieldReqModel
    {
        public long IngSk { get; set; }
        public long GenericYield { get; set; }
        public string UserId { get; set; }
    }
}
