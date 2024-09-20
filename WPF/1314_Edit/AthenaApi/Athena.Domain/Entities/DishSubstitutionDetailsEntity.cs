using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Dish_Substitution_Details")]
    public class DishSubstitutionDetailsEntity
    {
        [Key]
        [Column("DishSubstitute_Sk")]
        public int DishSubstituteSk { get; set; }

        [Required]
        [Column("Dish_Sk")]
        public int DishSk { get; set; }

        [Required]
        [Column("Pre_Ing_Sk")]
        public long PreIngSk { get; set; }

        [Required]
        [Column("Post_Ing_Sk")]
        public long PostIngSk { get; set; }

        [Column("Pre_Sale_Cost")]
        public float? PreSaleCost { get; set; }

        [Column("Post_Sale_Cost")]
        public float? PostSaleCost { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("Created_By")]
        public string CreatedBy { get; set; } = "SYSTEM";

        [Required]
        [Column("Created_Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [ForeignKey("DishSk")]
        public DishManagerEntity Dish { get; set; }

        [ForeignKey("PreIngSk")]
        public IngredientsMaster PreIngredient { get; set; }

        [ForeignKey("PostIngSk")]
        public IngredientsMaster PostIngredient { get; set; }
    }
}
