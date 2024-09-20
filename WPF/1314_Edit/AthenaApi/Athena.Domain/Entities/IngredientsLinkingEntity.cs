using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Ingredients_Linking")]
    public class IngredientsLinkingEntity
    {
        [Key]
        [Column("Ing_Link_Sk")]
        public int IngLinkSk { get; set; }

        [Required]
        [Column("Source_Ing_SK")]
        public long SourceIngSk { get; set; }

        [Required]
        [Column("Dest_Ing_SK")]
        public long DestIngSk { get; set; }

        [Required]
        [Column("Active_Status")]
        public bool ActiveStatus { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("Created_By")]
        public string CreatedBy { get; set; }

        [Required]
        [Column("Created_Date")]
        public DateTime CreatedDate { get; set; }

        [MaxLength(50)]
        [Column("Modified_By")]
        public string? ModifiedBy { get; set; }

        [Column("Modified_Date")]
        public DateTime? ModifiedDate { get; set; }

        [Required]
        public DateTime SysStartTime { get; set; }

        [Required]
        public DateTime SysEndTime { get; set; }



        [ForeignKey("SourceIngSk")]
        public IngredientsMaster IngredientMaster { get; set; }
    }
}
