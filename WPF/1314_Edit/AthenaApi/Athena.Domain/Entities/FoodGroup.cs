using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Food_Group")]
    public class FoodGroup : BaseEntity
    {
        [Key]
        [Column("FoodGroup_Sk")]
        public int FoodGroupSk { get; set; }
        [Column("Code")]
        [MaxLength(50)]
        [Required]
        public string Code { get; set; }
        [Column("Name")]
        [MaxLength(200)]
        [Required]
        public string Name { get; set; }
        [Column("Dish_Food_Group_Flag")]
        public bool DishFoodGroupFlag { get; set; }
        [Column("Active_Status")]
        [Required]
        public bool ActiveStatus { get; set; }

        public ICollection<IngredientsMaster> IngredientMaster { get; set; }
    }
}
