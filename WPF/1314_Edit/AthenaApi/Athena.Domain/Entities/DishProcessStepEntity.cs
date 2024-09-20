using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Dish_Process_Steps")]
    public class DishProcessStepEntity : BaseEntity
    {
        [Key]
        [Column("Dish_Proc_Step_Sk")]
        public int DishProcStepSk { get; set; }

        [Column("Process_Step_Code")]
        [MaxLength(30)]
        public string? ProcessStepCode { get; set; }

        [Column("Step_Description")]
        [MaxLength(200)]
        public string? StepDescription { get; set; }

        [Column("Step_Comment")]
        [MaxLength(500)]
        public string? StepComment { get; set; }

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
