using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Allergen_Options")]
    public class AllergenOptionEntity : BaseEntity
    {
        [Key]
        [Column("Allergen_Option_Id")]
        public int AllergenOptionId { get; set; }

        [Column("Allergen_Option")]
        [Required]
        [MaxLength(15)]
        public string Allergen_Option { get; set; }

        [Column("Allergen_Option_Value")]
        [Required]
        public int AllergenOptionValue { get; set; }

        [Column("Active_Status")]
        [Required]
        public bool ActiveStatus { get; set; }

    }
}
