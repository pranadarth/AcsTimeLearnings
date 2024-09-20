using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Portion_Controls")]
    public class PortionControlEntity
    {
        [Key]
        [Column("PortionControl_Id")]
        public int PortionControlId { get; set; }

        [Column("PortionsControl_Code")]
        [MaxLength(50)]
        public string? PortionsControlCode { get; set; }

        [Column("PortionsControl_Desc")]
        [MaxLength(150)]
        public string? PortionsControlDesc { get; set; }

        [Column("Display_Order")]
        public int? DisplayOrder { get; set; }

        [Column("PortionsControl_Img_Src")]
        [MaxLength(250)]
        public string? PortionsControlImgSrc { get; set; }

        [Required]
        [Column("Active_Status")]
        public bool ActiveStatus { get; set; }
    }
}
