using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Ingredients_Master_Caloric")]
    public class IngredientsMasterCaloric :BaseEntity
    {
        [Key]
        [Column("Ing_Mas_Caloric_Sk")]
        public int IngredientMasterCaloricSk { get; set; }

        [Required]
        [Column("Ing_SK")]
        public long IngSk { get; set; }

        [Required]
        [Column("Cal_Type_Sk")]
        public int CaloricTypeSk { get; set; }

        public double? Value { get; set; }

        [ForeignKey("IngSk")]
        public IngredientsMaster IngredientMaster { get; set; }


        [ForeignKey("CaloricTypeSk")]
        public FoodCaloricTypeEntity FoodCaloricTypeEntity { get; set; }

    }
}
