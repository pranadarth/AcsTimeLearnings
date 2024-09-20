using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Dish_Category_Mapping")]
    public class DishCategoryMappingEntity
    {
        [Key]
        [Column("Dish_Category_Mapping_Sk")]
        public int DishCategoryMappingSk { get; set; }

        [Column("Dish_Sk")]
        [Required]
        public int DishSk { get; set; }

        [Column("DishCategory_Id")]
        [Required]
        public int DishCategoryId { get; set; }

        [Column("Active_Status")]
        [Required]
        public bool ActiveStatus { get; set; }

        [Column("SysStartTime")]
        [Required]
        public DateTime SysStartTime { get; set; }

        [Column("SysEndTime")]
        [Required]
        public DateTime SysEndTime { get; set; }
    }
}
