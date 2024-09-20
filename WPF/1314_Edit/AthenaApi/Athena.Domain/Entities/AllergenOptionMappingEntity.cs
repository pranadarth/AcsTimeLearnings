using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Allergen_Option_Mapping")]
    public class AllergenOptionMappingEntity : BaseEntity
    {
        [Key]
        [Column("AllOptMap_Id")]
        public int AllOptMapId { get; set; }

        [Column("Allergen_Option_Id")]
        [Required]
        public int AllergenOptionId { get; set; }

        [Column("Allergen_Id")]
        [Required]
        public int AllergenId { get; set; }

        [Column("Display_Order")]
        [Required]
        public int DisplayOrder { get; set; }

        [Column("Active_Status")]
        [Required]
        public bool ActiveStatus { get; set; }
    }
}
