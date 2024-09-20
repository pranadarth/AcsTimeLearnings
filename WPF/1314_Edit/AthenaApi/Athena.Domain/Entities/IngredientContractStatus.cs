using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Ingredient_Contract_Status")]
    public class IngredientContractStatus : BaseEntity
    {
        [Key]
        [Column("Contract_Status_Sk")]
        public int ContractStatusSk { get; set; }
        [Column("Contract_Status_Code")]
        [MaxLength(50)]
        [Required]
        public string ContractStatusCode { get; set; }
        [Column("Contract_Status_Name")]
        [MaxLength(200)]
        [Required]
        public string ContractStatusName { get; set; }
        [Column("Active_Status")]
        [Required]
        public bool ActiveStatus { get; set; }

        public ICollection<IngredientsMaster> IngredientMaster { get; set; }

    }
}
