using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Cutoff_Time")]
    public class CutoffTimeEntity
    {
        [Key]
        [Column("Cutoff_Time_Sk")]
        public int CutoffTimeSk { get; set; }

        [Column("Cutoff_Type")]
        [MaxLength(30)]
        public string? CutoffType { get; set; }

        [Column("Start_Time")]
        public TimeSpan? StartTime { get; set; }

        [Column("End_Time")]
        public TimeSpan? EndTime { get; set; }

        [Column("Num_of_Days")]
        public float? NumOfDays { get; set; }

        [Column("Extendable")]
        public bool? Extendable { get; set; }

        [Column("Active_Status")]
        public bool ActiveStatus { get; set; } = true;

        [Column("Created_By")]
        [MaxLength(50)]
        public string CreatedBy { get; set; } = "SYSTEM";

        [Column("Created_Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Column("Modified_By")]
        [MaxLength(50)]
        public string? ModifiedBy { get; set; }

        [Column("Modified_Date")]
        public DateTime? ModifiedDate { get; set; }
    }
}
