using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Sub_Location")]
    public class SubLocationEntity
    {
        [Key]
        [Column("Sub_Location_Sk")]
        public int SubLocationSk { get; set; }

        [Required]
        [ForeignKey("Location")]
        [Column("Location_Sk")]
        public int LocationSk { get; set; }

        [Column("SubLocation_Code")]
        [MaxLength(30)]
        public string SubLocationCode { get; set; }

        [Column("SubLocation_Desc")]
        [MaxLength(200)]
        public string SubLocationDesc { get; set; }

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

        public virtual LocationEntity Location { get; set; }
    }
}
