using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{

    [Table("Food_Caloric_Type")]
    public class FoodCaloricTypeEntity : BaseEntity
    {
        [Key]
        [Column("Cal_Type_Sk")]
        public int CalTypeSk { get; set; }

        [Column("Code")]
        [MaxLength(100)]
        public string Code { get; set; }

        [Column("Name")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Column("Unit")]
        [MaxLength(5)]
        public string Unit { get; set; }

        [Column("Active_Status")]
        [Required]
        public bool ActiveStatus { get; set; }

        public ICollection<IngredientsMasterCaloric> IngredientsMasterCaloric { get; set; }
    }
}
