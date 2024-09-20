using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class DishPreparationsReqModel
    {
        public int DishPrepSk { get; set; }

        public int DishSk { get; set; }

        public int? DishPrepStepSequence { get; set; }

        public string DishPrepMethod { get; set; }

        public int? DishProcessesSk { get; set; }

        public int? DishProcStepSk { get; set; }

        public int? DishProcSectionSk { get; set; }

        public int? DishTimeSk { get; set; }

        public float? DishPrepTime { get; set; }

        public int? DishTempSk { get; set; }

        public float? DishLowTemp { get; set; }

        public float? DishHighTemp { get; set; }

        public bool? DishHaccpFlag { get; set; }
    }
}
