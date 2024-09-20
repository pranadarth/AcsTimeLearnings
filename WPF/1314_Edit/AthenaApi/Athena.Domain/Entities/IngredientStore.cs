using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Ingredient_Store")]
    public class IngredientStore : BaseEntity
    {
        [Key]
        [Column("Store_Sk")]
        public int StoreSk { get; set; }
        [Column("Store_Code")]
        [MaxLength(50)]
        [Required]
        public string StoreCode { get; set; }
        [Column("Store_Name")]
        [MaxLength(200)]
        [Required]
        public string StoreName { get; set; }
        [Column("Active_Status")]
        [Required]
        public bool ActiveStatus { get; set; }
        public ICollection<IngredientsMaster> IngredientMaster { get; set; }
    }
}
