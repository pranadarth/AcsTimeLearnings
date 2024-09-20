using Athena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class MenuTypeDetailsModel
    {
        public int DishMenuTypeId { get; set; }
        public string DishMenuTypeCode { get; set; }
        public string DishMenuTypeDesc { get; set; }
        public int? DisplayOrder { get; set; }
        public LocationEntity? Location { get; set; }
    }
}
