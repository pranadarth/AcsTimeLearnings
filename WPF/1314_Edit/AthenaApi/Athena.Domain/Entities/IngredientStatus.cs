using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Ingredient_Status")]
    public class IngredientStatus : BaseEntity
    {
        [Key]
        [Column("Status_Sk")]
        public int StatusSk { get; set; }
        [Column("Status_Code")]
        [MaxLength(50)]
        [Required]
        public string StatusCode { get; set; }
        [Column("Status_Name")]
        [MaxLength(200)]
        [Required]
        public string StatusName { get; set; }
        [Column("Active_Status")]
        [Required]
        public bool ActiveStatus { get; set; }

        public ICollection<IngredientsMaster> IngredientMaster { get; set; }
    }
}
