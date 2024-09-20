using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class CopyMenuReqModel
    {
        public int MenuFormTypeId { get; set; }
        public string UserId { get; set; }
        public CopyFrom CopyFrom { get; set; }
        public CopyTo CopyTo { get; set; }
    }

    public class CopyFrom
    {
        public int LocationSk { get; set; }
        public int? SubLocationSk { get; set; }
        public int MenuTypeId { get; set; }
        public int MealTypeId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }

    public class CopyTo
    {
        public List<int> LocationSks { get; set; }
        public List<int> SubLocationSks { get; set; }
        public int MenuTypeId { get; set; }
        public int MealTypeId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
