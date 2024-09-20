using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Sub_Allergens")]
    public class SubAllergensEntity : BaseEntity
    {
        [Key]
        [Column("Sub_Allergen_Id")]
        public int SubAllergenId { get; set; }

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

        [Column("Active_Status")]
        [Required]
        public bool ActiveStatus { get; set; }


        [ForeignKey("AllergenId")]
        public AllergenEntity AllergenEntity { get; set; }
    }
}
