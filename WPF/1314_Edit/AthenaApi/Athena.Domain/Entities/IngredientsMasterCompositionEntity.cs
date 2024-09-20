using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Ingredients_Master_Composition")]
    public class IngredientsMasterCompositionEntity : BaseEntity
    {
        [Key]
        [Column("Ing_Mas_Com_SK")]
        public int IngMasComSK { get; set; }

        [Required]
        [Column("Ing_Sk")]
        public long IngSk { get; set; }

        [Column("Ingredients")]
        [MaxLength]
        public string? Ingredients { get; set; }

        [Column("Manually_updated")]
        public bool? ManuallyUpdated { get; set; }

        [Required]
        [Column("Active_Status")]
        public bool ActiveStatus { get; set; }


        [Required]
        [Column("SysStartTime")]
        public DateTime SysStartTime { get; set; }

        [Required]
        [Column("SysEndTime")]
        public DateTime SysEndTime { get; set; }
    }
}
