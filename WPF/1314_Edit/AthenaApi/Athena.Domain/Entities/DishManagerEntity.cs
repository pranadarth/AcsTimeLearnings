using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Entities
{
    [Table("Dish_Manager")]
    public class DishManagerEntity
    {
        [Key]
        [Column("Dish_Sk")]
        public int DishSk { get; set; }

        [Required]
        [Column("Dish_Name")]
        [MaxLength(200)]
        public string? DishName { get; set; }

        [Column("Display_Name")]
        [MaxLength(200)]
        public string? DisplayName { get; set; }

        [Column("Dish_Description")]
        public string? DishDescription { get; set; }

        [Column("Dish_FoodType_Id")]
        public int? DishFoodTypeId { get; set; }

        [Column("Dish_HeatType_Id")]
        public int? DishHeatTypeId { get; set; }

        [Column("PortionControl_Id")]
        public int? PortionControlId { get; set; }

        [Column("DishTemp_Id")]
        public int? DishTempId { get; set; }

        [Column("DishShelfLife_Id")]
        public int? DishShelfLifeId { get; set; }

        [Column("Portion_Size")]
        public float? PortionSize { get; set; }

        [Column("Utility_Cost")]
        public float? UtilityCost { get; set; }

        [Column("Cost_Per_Portion")]
        public float? CostPerPortion { get; set; }

        [Column("Cost")]
        public float? Cost { get; set; }

        [Column("Sale_Price")]
        public float? SalePrice { get; set; }

        [Column("Cooked_Portion_Weight")]
        public float? CookedPortionWeight { get; set; }

        [Required]
        [Column("CrossContamination")]
        public bool CrossContamination { get; set; }

        [Required]
        [Column("Active_Status")]
        public bool ActiveStatus { get; set; }

        [Column("Dish_Image")]
        [MaxLength(250)]
        public string? DishImage { get; set; }

        [Column("Calrories")]
        public float? Calories { get; set; }

        [Column("Protein")]
        public float? Protein { get; set; }

        [Column("Salt")]
        public float? Salt { get; set; }

        [Column("Fat")]
        public float? Fat { get; set; }

        [Column("Fibre")]
        public float? Fibre { get; set; }

        [Column("Sodium")]
        public float? Sodium { get; set; }

        [Column("Carbohydrates")]
        public float? Carbohydrates { get; set; }

        [Column("Popularity")]
        public float? Popularity { get; set; }

        [Column("Cals_From_Fat")]
        public float? CalsFromFat { get; set; }

        [Column("Calrorie_Source")]
        [MaxLength(50)]
        public string? CalrorieSource { get; set; }

        [Column("Salt_Equivalent")]
        public float? SaltEquivalent { get; set; }

        [Column("Saturated_Fat")]
        public float? SaturatedFat { get; set; }

        [Column("Popularity_Source")]
        [MaxLength(50)]
        public string? PopularitySource { get; set; }

        [Column("Modified_By")]
        [MaxLength(50)]
        public string? ModifiedBy { get; set; }

        [Column("Modified_Date")]
        public DateTime? ModifiedDate { get; set; }

        [Column("SysStartTime")]
        public DateTime SysStartTime { get; set; }

        [Column("SysEndTime")]
        public DateTime SysEndTime { get; set; }
    }
}
