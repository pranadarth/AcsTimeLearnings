using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class AddMenuFormCourseMealMapReqData
    {
        public int? MenuFormTypeSk { get; set; }

        public int? MealTypeId { get; set; }

        public int? CourseTypeSk { get; set; }

        public int? DisplayOrder { get; set; }

        public bool ActiveStatus { get; set; } = true;
        public string? UserId { get; set; }
    }
}
