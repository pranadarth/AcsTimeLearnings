using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Dish_Process_Section")]
    public class DishProcessSectionEntity : BaseEntity
    {
        [Key]
        [Column("Dish_Proc_Section_Sk")]
        public int DishProcSectionSk { get; set; }

        [Column("Process_Section_Code")]
        [MaxLength(30)]
        public string? ProcessSectionCode { get; set; }

        [Column("Section_Description")]
        [MaxLength(200)]
        public string? SectionDescription { get; set; }

        [Column("Section_Comment")]
        [MaxLength(500)]
        public string? SectionComment { get; set; }

        [Column("Process_Img_Src")]
        [MaxLength(250)]
        public string? ProcessImgSrc { get; set; }

        [Column("Display_Order")]
        public int? DisplayOrder { get; set; }

        [Column("Active_Status")]
        [Required]
        public bool ActiveStatus { get; set; }
    }
}
