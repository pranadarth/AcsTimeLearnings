using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Dish_Category")]
    public class DishCategoryEntity : BaseEntity
    {
        [Key]
        [Column("DishCategory_Id")]
        public int DishCategoryId { get; set; }

        [Required]
        [Column("DishCategory_Code")]
        [MaxLength(20)]
        public string? DishCategoryCode { get; set; }

        [Required]
        [Column("DishCategory_Desc")]
        [MaxLength(250)]
        public string? DishCategoryDesc { get; set; }

        [Required]
        [Column("Display_Order")]
        public int DisplayOrder { get; set; }

        [Required]
        [Column("Active_Status")]
        public bool ActiveStatus { get; set; }

    }
}
