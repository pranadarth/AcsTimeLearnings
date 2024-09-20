using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Ingredients_Cost_History")]
    public class IngredientCostHistory :BaseEntity
    {

        [Key]
        [Column("Ing_His_Sk")]
        public int IngredientHistorySk { get; set; }
        [Required]
        [Column("Ing_Sk")]
        public long IngSk { get; set; } 
        [Required]
        [Column("Effective_Date")]
        public System.DateTime EffectiveDate { get; set; }
        [Required]
        [Column("Cost")]
        public float Cost { get; set; }
        [Required]
        [Column("Supplier_Name")]
        [MaxLength(100)]
        public string SupplierName { get; set; }
        [Required]
        [Column("Supplier_Reference_No")]
        [MaxLength(50)]
        public string SupplierReferenceNo { get; set; }
        [Column("Active_Status")]
        [Required]
        public bool ActiveStatus { get; set; }


        //[ForeignKey("Ing_SK")]
        //public IngredientMaster? IngredientMaster { get; set; }

    }
}
