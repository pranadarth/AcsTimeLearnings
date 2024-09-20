using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class CopyMenuResponeModel
    {
        public string Location { get; set; }
        public int LocationSk { get; set; }
        public string MenuType { get; set; }

        public bool IsLocationMenuTypeMismatch { get; set; }
        public bool IsCourseMismatch { get; set; }

        public int CourseTypeSk { get; set; }
        public bool IsMenuFormMealCourseMismatch { get; set; }
        public List<DateDetails> DateDetails { get; set; }
    }

    public class DateDetails
    {
        public DateTime Date { get; set; }
        public bool IsSuccess { get; set; }
        public int MFDetailsSk { get; set; }
    }
}
