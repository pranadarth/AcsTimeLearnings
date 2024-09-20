using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.Xml.Linq;
using System.Diagnostics.Metrics;

namespace Athena.Domain.Entities
{

    [Table("Ingredients_master")]
    public class IngredientsMaster : BaseEntity
    {
        [Key]
        [Column("Ing_SK")]
        public long IngSk { get; set; }
        [MaxLength(250)]
        [Column("Ingredients_Name")]
        public string IngredientsName { get; set; }
        [MaxLength(50)]
        [Column("Calculation_Method")]
        public string? CalculationMethod { get; set; }
        [MaxLength(100)]
        [Column("Supplier_Name")]
        public string? SupplierName { get; set; }
        [Column("Minimum_Order_Quantity")]
        public float? MinimumOrderQuantity { get; set; }
        [MaxLength]
        [Column("Specification_Description")]
        public string? SpecificationDescription { get; set; }

        [Column("Supplier_Id")]
        [Required]
        public int SupplierId { get; set; }
        [MaxLength]
        [Column("Supplier_Reference_No")]
        public string? SupplierReferenceNo { get; set; }
        //[MaxLength(100)]
        //[Column("Pack_size")]
        //public string? PackSize { get; set; }
        [Column("Store_Sk")]
        [Required]
        public int StoreSk { get; set; }
        public float? SalePrice { get; set; }
        public bool? IsOnMenu { get; set; }
        [Column("Measure_Option_Id")]
        [Required]
        public int MeasureOptionId { get; set; }
        [Required]
        public float Weight { get; set; }
        public long? McwCode { get; set; }
        [Column("Country_Sk")]
        public int? CountrySk { get; set; }
        public double? PrepYield { get; set; }
        [Column("FoodGroup_Sk")]
        public int? FoodGroupSk { get; set; }
        public double? GenericYield { get; set; }
        [MaxLength(200)]
        public string? Barcode { get; set; }
        [Column("Ingredient_Type_Id")]
        public int? IngredientTypeId { get; set; }
        [Column("Status_Sk")]
        public int StatusSk { get; set; }
        [Column("Contract_Status_Sk")]
        public int? ContractStatusSk { get; set; }
        public System.DateTime? SysStartTime { get; set; }
        public System.DateTime? SysEndTime { get; set; }

        [ForeignKey("CountrySk")]
        public Country Country { get; set; }

        [ForeignKey("ContractStatusSk")]
        public IngredientContractStatus IngredientContractStatus { get; set; }

        //public ICollection<IngredientCostHistory> IngredientCostHistory { get; set; }

        [ForeignKey("FoodGroupSk")]
        public FoodGroup FoodGroup { get; set; }

        [ForeignKey("MeasureOptionId")]
        public MeasureOptions MeasureOptions { get; set; }

        [ForeignKey("StatusSk")]
        public IngredientStatus IngredientStatus { get; set; }


        [ForeignKey("StoreSk")]
        public IngredientStore IngredientStore { get; set; }

        public ICollection<IngredientsCosting> IngredientsCosting { get; set; }

        public ICollection<IngredientsMasterCaloric> IngredientsMasterCaloric { get; set; }

        public ICollection<IngredientsMasterAllergensEntity> IngredientsMasterAllergensEntity { get; set; }
    }
}
