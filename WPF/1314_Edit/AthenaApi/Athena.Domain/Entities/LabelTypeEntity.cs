using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Label_Type")]
    public class LabelTypeEntity : BaseEntity
    {
        [Key]
        [Column("Label_Type_Id")]
        public int LabelTypeId { get; set; }

        [Column("Label_Type")]
        [MaxLength(50)]
        public string? LabelType { get; set; }

        [Column("Label_Size")]
        [MaxLength(25)]
        public string? LabelSize { get; set; }

        [Column("Label_Type_Desc")]
        [MaxLength(150)]
        public string? LabelTypeDesc { get; set; }

        [Column("Label_Model_Img_Src")]
        [MaxLength(250)]
        public string? LabelModelImgSrc { get; set; }

        [Required]
        [Column("Active_Status")]
        public bool ActiveStatus { get; set; }
    }
}
