using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Measure_Options")]
    public class MeasureOptions : BaseEntity
    {
        [Key]
        [Column("Measure_Option_Id")]
        public int MeasureOptionId { get; set; }
        [Column("Measure_Option")]
        [Required]
        [MaxLength(15)]
        public string MeasureOption { get; set; }
        [Column("Measure_Option_Symbol")]
        [Required]
        [MaxLength(5)]
        public string MeasureOptionSymbol { get; set; }
        [Column("Active_Status")]
        [Required]
        public bool ActiveStatus { get; set; }
        public ICollection<IngredientsMaster> IngredientMaster { get; set; }

    }
}
