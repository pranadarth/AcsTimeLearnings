using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Menu_Form_Types")]
    public class MenuFormTypeEntity
    {
        [Key]
        [Column("Menu_Form_Type_Sk")]
        public int MenuFormTypeSk { get; set; }

        [Column("Menu_Form_Code")]
        [MaxLength(30)]
        public string? MenuFormCode { get; set; }

        [Column("Menu_Form_Desc")]
        [MaxLength(200)]
        public string? MenuFormDesc { get; set; }

        [Column("Display_Order")]
        public int? DisplayOrder { get; set; }

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
