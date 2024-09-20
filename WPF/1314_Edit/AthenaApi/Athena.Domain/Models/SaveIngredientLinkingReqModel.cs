using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class SaveIngredientLinkingReqModel
    {
        public long SourceIngSk { get; set; }
        public List<long> DestingationIngSk { get; set; }
        public string? UserId { get; set; }
        public bool ActiveStatus { get; set; }
    }
}
