using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Location")]
    public class LocationEntity
    {

        [Key]
        [Column("Location_Sk")]
        public int LocationSk { get; set; }

        [Column("Location_Code")]
        [MaxLength(30)]
        public string LocationCode { get; set; }

        [Column("Location_Desc")]
        [MaxLength(200)]
        public string LocationDesc { get; set; }

        [Column("Display_Order")]
        public int? DisplayOrder { get; set; }

        [Column("Active_Status")]
        public bool ActiveStatus { get; set; } = true;

        [Required]
        [Column("Created_By")]
        [MaxLength(50)]
        public string CreatedBy { get; set; } = "SYSTEM";

        [Required]
        [Column("Created_Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Column("Modified_By")]
        [MaxLength(50)]
        public string? ModifiedBy { get; set; }

        [Column("Modified_Date")]
        public DateTime? ModifiedDate { get; set; }
    }
}
