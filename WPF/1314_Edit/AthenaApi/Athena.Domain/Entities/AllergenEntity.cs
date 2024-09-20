using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Allergens")]
    public class AllergenEntity
    {
        [Key]
        [Column("Allergen_Id")]
        public int AllergenId { get; set; }

        [Column("Code")]
        [Required]
        [MaxLength(100)]
        public string Code { get; set; }

        [Column("Name")]
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Column("Primary")]
        public bool Primary { get; set; }


        [Column("Active_Status")]
        [Required]
        public bool ActiveStatus { get; set; }

        public ICollection<IngredientsMasterAllergensEntity> IngredientsMasterAllergensEntity { get; set; }
        public ICollection<SubAllergensEntity> SubAllergensEntity { get; set; }
    }
}
