using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Ingredients_Master_Allergens")]
    public class IngredientsMasterAllergensEntity:BaseEntity
    {
        [Key]
        [Column("Ing_Mas_Allergen_Sk")]
        public int IngMasAllergenSk { get; set; }

        //Foreign Key
        [Column("Ing_SK")]
        [Required]
        public long IngSk { get; set; }

        [MaxLength(20)]
        public string? Weight { get; set; }

        //Foreign key
        [Column("Allergen_Id")]
        [Required]
        public int AllergenId { get; set; }

        [Column("Sub_Allergen_Id")]
        public int? SubAllergenId { get; set; }

        [Column("Allergen_Option_Id")]
        public int? AllergenOptionId { get; set; }

        [ForeignKey("IngSk")]
        public IngredientsMaster IngredientMaster { get; set; }

        [ForeignKey("AllergenId")]
        public AllergenEntity AllergenEntity { get; set; }
    }
}
