using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class DishPreparationsModel
    {
        public int DishPrepSk { get; set; }

        public int DishSk { get; set; }

        public int? DishPrepStepSequence { get; set; }

        public string? DishPrepMethod { get; set; }

        public int? DishProcessesSk { get; set; }
        public string? DishProcesses { get; set; }

        public int? DishProcStepSk { get; set; }
        public string? DishProcStep { get; set; }

        public int? DishProcSectionSk { get; set; }
        public string? DishProcSection { get; set; }

        public int? DishTimeSk { get; set; }
        public string? DishTime { get; set; }

        public float? DishPrepTime { get; set; }

        public int? DishTempSk { get; set; }
        public string? DishTemp { get; set; }

        public float? DishLowTemp { get; set; }

        public float? DishHighTemp { get; set; }

        public bool? DishHaccpFlag { get; set; }
    }
}
