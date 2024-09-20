using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Dish_Templates")]
    public class DishTemplateEntity : BaseEntity
    {
        [Key]
        [Column("DishTemp_Id")]
        public int DishTempId { get; set; }

        [Column("DishTemplate_Code")]
        [MaxLength(50)]
        public string? DishTemplateCode { get; set; }

        [Column("DishTemplate_Desc")]
        [MaxLength(500)]
        public string? DishTemplateDesc { get; set; }

        [Column("Display_Order")]
        public int? DisplayOrder { get; set; }

        [Column("DishTemp_Img_Src")]
        [MaxLength(250)]
        public string? DishTempImgSrc { get; set; }

        [Required]
        [Column("Active_Status")]
        public bool ActiveStatus { get; set; }
    }
}
