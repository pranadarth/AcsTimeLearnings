using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class LocationDetailsModel
    {
        public int LocationSk { get; set; }

        public string? LocationCode { get; set; }

        public string? LocationDesc { get; set; }

        public int? DisplayOrder { get; set; }

        public List<SubLocationDetailsModel>? SubLocationDetails { get; set; }
    }

    public class SubLocationDetailsModel
    {
        public int SubLocationSk { get; set; }
        public int LocationSk { get; set; }
        public string SubLocationCode { get; set; }
        public string SubLocationDesc { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
