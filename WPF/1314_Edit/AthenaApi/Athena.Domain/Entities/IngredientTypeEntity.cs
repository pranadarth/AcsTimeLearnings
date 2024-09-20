using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Ingredient_Type")]
    public class IngredientTypeEntity
    {
        [Key]
        [Column("Ingredient_Type_Id")]
        public int IngredientTypeId { get; set; }

        [Column("Ingredient_Type")]
        [MaxLength(30)]
        [Required]
        public string IngredientType { get; set; }

        [Column("Ingredient_Type_Desc")]
        [MaxLength(60)]
        public string IngredientTypeDesc { get; set; }

        [Column("Active_Status")]
        [Required]
        public bool ActiveStatus { get; set; }
    }
}
