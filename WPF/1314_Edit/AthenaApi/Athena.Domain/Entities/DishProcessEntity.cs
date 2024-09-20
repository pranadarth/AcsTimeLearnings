using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Dish_Processes")]
    public class DishProcessEntity : BaseEntity
    {
        [Key]
        [Column("Dish_Processes_Sk")]
        public int DishProcessesSk { get; set; }

        [Column("Process_Code")]
        [MaxLength(30)]
        public string? ProcessCode { get; set; }

        [Column("Process_Description")]
        [MaxLength(200)]
        public string? ProcessDescription { get; set; }

        [Column("Process_Comment")]
        [MaxLength(500)]
        public string? ProcessComment { get; set; }

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
